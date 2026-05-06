using BSI.Integra.Aplicacion.DTO.ExperianSentinel;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using System.Net.Http.Json;
using System.Text.Json;

namespace BSI.Integra.Aplicacion.Transversal.ExperianSentinel
{
    /// <summary>
    /// Implementacion REST del cliente Experian Sentinel.
    /// Se activa cuando Sentinel:Provider = "REST" en appsettings.json.
    /// Maneja OAuth2 con cache de token (TTL 540 seg, el token expira en 600 seg).
    /// </summary>
    public class ExperianSentinelRestClient : IExperianSentinelClient
    {
        private readonly HttpClient _httpClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        private const string ClaveCacheToken = "ExperianSentinelToken";
        private const int SegundosCacheToken = 540; // token expira en 600, cacheamos 540 para margen

        public ExperianSentinelRestClient(HttpClient httpClient, IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        /// <summary>
        /// Consulta el historial crediticio via REST.
        /// Paso 1: obtiene/renueva token OAuth2 (cacheado 540 seg).
        /// Paso 2: consulta el endpoint gethistory con Bearer token.
        /// </summary>
        public async Task<ExperianSentinelRespuestaDTO> ConsultarAsync(string dniConsulta, string tipoDocumento)
        {
            var credenciales = _unitOfWork.SentinelRepository.ObtenerCredencialRest();
            var token = await ObtenerTokenAsync(credenciales);
            var respuestaJson = await ConsultarHistorialAsync(credenciales, token, dniConsulta);
            return MapearRespuesta(respuestaJson);
        }

        public async Task<object> ConsultarAsyncCrudo(string dniConsulta, string tipoDocumento)
        {
            var credenciales = _unitOfWork.SentinelRepository.ObtenerCredencialRest();
            var token = await ObtenerTokenAsync(credenciales);
            return await ConsultarHistorialAsync(credenciales, token, dniConsulta);
        }

        // ──────────────────────────────────────────────────────────────────────
        // Metodos privados
        // ──────────────────────────────────────────────────────────────────────

        private async Task<string> ObtenerTokenAsync(SentinelCredencialRestDTO credenciales)
        {
            // Intentar recuperar token del cache
            if (_cache.TryGetValue(ClaveCacheToken, out string? tokenCacheado) && tokenCacheado != null)
            {
                return tokenCacheado;
            }

            // Token no existe o expiro, obtener uno nuevo
            var solicitudToken = new ExperianTokenRequest
            {
                Username = credenciales.Username,
                Password = credenciales.Password
            };

            using var mensajeToken = new HttpRequestMessage(HttpMethod.Post, credenciales.UrlToken);
            mensajeToken.Headers.Add("Client_id", credenciales.ClientId);
            mensajeToken.Headers.Add("Client_secret", credenciales.ClientSecret);
            var jsonBody = JsonSerializer.Serialize(solicitudToken);
            var content = new StringContent(jsonBody);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            mensajeToken.Content = content;

            var respuestaToken = await _httpClient.SendAsync(mensajeToken);
            respuestaToken.EnsureSuccessStatusCode();

            var contenidoToken = await respuestaToken.Content.ReadFromJsonAsync<ExperianTokenResponse>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (contenidoToken?.AccessToken == null)
            {
                throw new InvalidOperationException("Experian REST: no se obtuvo access_token en la respuesta de autenticacion.");
            }

            // Cachear el token con TTL de 540 segundos
            var opcionesCache = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(SegundosCacheToken)
            };
            _cache.Set(ClaveCacheToken, contenidoToken.AccessToken, opcionesCache);

            return contenidoToken.AccessToken;
        }

        private async Task<ExperianRespuestaRaiz> ConsultarHistorialAsync(
            SentinelCredencialRestDTO credenciales,
            string token,
            string dniConsulta)
        {
            var solicitudConsulta = new ExperianConsultaRequest
            {
                TipDoc = credenciales.TipDocConsulta,
                NroDoc = dniConsulta
            };

            using var mensajeConsulta = new HttpRequestMessage(HttpMethod.Post, credenciales.UrlConsulta);
            mensajeConsulta.Headers.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            mensajeConsulta.Headers.Add("Gx_email", credenciales.GxEmail);
            mensajeConsulta.Headers.Add("Gx_Key", credenciales.GxKey);
            mensajeConsulta.Headers.Add("Gx_usuario", credenciales.GxUsuario);
            mensajeConsulta.Headers.Add("Client_id", credenciales.ClientId);
            mensajeConsulta.Headers.Add("Client_secret", credenciales.ClientSecret);
            var jsonConsulta = JsonSerializer.Serialize(solicitudConsulta);
            var contentConsulta = new StringContent(jsonConsulta);
            contentConsulta.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            mensajeConsulta.Content = contentConsulta;

            var respuesta = await _httpClient.SendAsync(mensajeConsulta);
            respuesta.EnsureSuccessStatusCode();

            var contenido = await respuesta.Content.ReadFromJsonAsync<ExperianRespuestaRaiz>(
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (contenido?.Respuesta == null)
            {
                throw new InvalidOperationException("Experian REST: la respuesta no contiene el nodo 'respuesta'.");
            }

            return contenido;
        }

        /// <summary>
        /// Mapea la respuesta JSON de Experian REST al DTO unificado.
        /// Los campos sin equivalente REST se documentan con TODO.
        /// </summary>
        private ExperianSentinelRespuestaDTO MapearRespuesta(
            ExperianRespuestaRaiz respuestaRaiz)
        {
            var resp = respuestaRaiz.Respuesta!;
            var infBas = resp.InfBas;
            var conRap = resp.ConRap;
            var resumen = conRap?.Resumen_ConRap;

            var resultado = new ExperianSentinelRespuestaDTO();

            // ── 1. EstandarItem (1 registro combinando InfBas + Resumen_ConRap) ──
            var estandarItem = new SentinelSdtEstandarItemDTO
            {
                TipoDocumento  = infBas?.TDoc,
                Documento      = infBas?.NDoc,
                RazonSocial    = ObtenerRazonSocial(infBas),
                EstadoDomicilio = infBas?.EstDom,
                Documento2     = infBas?.RelNDoc,
                TipoActividad  = infBas?.TipCon,
                // InfBas.IniAct viene como string "yyyy-MM-dd" o similar
                FechaInicioActividad = ParsearFechaOpcional(infBas?.IniAct),
                // Desde Resumen_ConRap
                FechaProceso    = ParsearFechaOpcional(resumen?.FechaProceso),
                Semaforos       = resumen?.Semaforos,
                Score           = resumen?.Nota,
                NroBancos       = resumen?.NroEntFin,
                SemanaActual    = resumen?.SemaActual,
                SemanaPrevio    = resumen?.SemaPrevio,
                SemanaPeorMejor = resumen?.SemaPeorMejor,
                DeudaTotal      = resumen?.DeudaTotal,
                Calificativo    = resumen?.Calificativo,
                DeudaTributaria = resumen?.DeudaTributaria,
                DeudaLaboral    = resumen?.DeudaLaboral,
                DeudaImpagable  = resumen?.DeudaImpaga,
                DeudaProtestos  = resumen?.DeudaProtestos,
                DeudaSbs        = resumen?.DeudaSBSMicrof,
                // TODO: sin equivalente en REST (Experian no expone estos campos via REST)
                VencidoBanco              = null,
                Veces24m                  = null,
                CondicionDomicilio        = null,
                CuentasTarjetas           = null,
                ReporteNegativo           = null,
                DeudaDirecta              = null,
                DeudaIndirecta            = null,
                DeudaCastigada            = null,
                TotalRiesgo               = null,
                PeorCalificacion          = null,
                PorcentajeCalificacionNormal = null,
                LineaCreditoNoUtilizada   = null
            };
            resultado.Estandar.Add(estandarItem);

            // ── 2. RepSBSItem[] - entidades del período más reciente (mayor Ano, mayor Mes) ──
            var periodoMasRecienteSbs = conRap?.DeuSBSMicro?
                .OrderByDescending(p => p.Ano ?? 0)
                .ThenByDescending(p => p.Mes ?? 0)
                .FirstOrDefault();

            if (periodoMasRecienteSbs?.Entidad != null)
            {
                foreach (var entidad in periodoMasRecienteSbs.Entidad)
                {
                    resultado.RepSBS.Add(new SentinelSdtRepSbsitemDTO
                    {
                        TipoDoc           = infBas?.TDoc,
                        NroDoc            = infBas?.NDoc,
                        NombreRazonSocial = entidad.NomEnt,
                        Calificacion      = entidad.Cal,
                        MontoDeuda        = entidad.SalDeu,
                        DiasVencidos      = entidad.DiaVen,
                        FechaReporte      = ParsearFechaOpcional(entidad.FchRep)
                    });
                }
            }

            // ── 3. LinCreItem[] - UtiLinCre[] ──
            if (conRap?.UtiLinCre != null)
            {
                decimal totalLineaCredito       = 0;
                decimal totalLineaCreditoNoUtil = 0;
                decimal totalLineaUtil          = 0;

                foreach (var linea in conRap.UtiLinCre)
                {
                    totalLineaCredito       += linea.LinApr    ?? 0;
                    totalLineaCreditoNoUtil += linea.LinNoUti  ?? 0;
                    totalLineaUtil          += linea.LinUti    ?? 0;

                    resultado.LinCre.Add(new SentinelSdtLincreItemDTO
                    {
                        TipoDocumento      = infBas?.TDoc,
                        NumeroDocumento    = infBas?.NDoc,
                        TipoCuenta         = linea.TipCred,
                        LineaCredito       = linea.LinApr,
                        LineaCreditoNoUtil = linea.LinNoUti,
                        LineaUtil          = linea.LinUti,
                        CnsEntNomRazLn     = linea.Inst
                    });
                }

                resultado.LinCre.Add(new SentinelSdtLincreItemDTO
                {
                    TipoDocumento      = infBas?.TDoc,
                    NumeroDocumento    = infBas?.NDoc,
                    TipoCuenta         = null,
                    LineaCredito       = totalLineaCredito,
                    LineaCreditoNoUtil = totalLineaCreditoNoUtil,
                    LineaUtil          = totalLineaUtil,
                    CnsEntNomRazLn     = "Total"
                });
            }

            // ── 4. ResVenItem[] - aplanar DetVen[].Fuentes[].Entidad[] ──
            if (conRap?.DetVen != null)
            {
                foreach (var periodoVen in conRap.DetVen)
                {
                    if (periodoVen.Fuentes == null) continue;
                    foreach (var fuente in periodoVen.Fuentes)
                    {
                        if (fuente.Entidad == null) continue;
                        foreach (var entidadVen in fuente.Entidad)
                        {
                            resultado.ResVen.Add(new SentinelSdtResVenItemDTO
                            {
                                TipoDocumento = infBas?.TDoc,
                                NroDocumento  = infBas?.NDoc,
                                Fuente        = fuente.NomFue,
                                Entidad       = entidadVen.NomEnt,
                                Monto         = entidadVen.MontDeu,
                                DiasVencidos  = entidadVen.DiaVen,
                                CantidadDocs  = entidadVen.NumDoc,
                                // TODO: Cantidad (short) no tiene equivalente directo en REST
                                Cantidad      = null
                            });
                        }
                    }
                }
            }

            // ── 5. InfGenItem (1 registro) - InfBas + InfGen.Direcc[0] ──
            var infGenDto = new SentinelSdtInfGenDTO
            {
                Dni                        = infBas?.NDoc,
                EstadoContribuyente        = infBas?.EstCon,
                CodigoEstadoContribuyente  = infBas?.EstConC,
                TipoContribuyente          = infBas?.TipCon,
                ActividadEconomica         = infBas?.ActEco,
                Ciiu                       = infBas?.CIIU,
                Folio                      = infBas?.Fol,
                Asiento                    = infBas?.Asi,
                PartidaReg                 = infBas?.NumParReg,
                FechaActividad             = ParsearFechaOpcional(infBas?.IniAct),
                Direccion                  = resp.InfGen?.Direcc?.FirstOrDefault()?.Direccion,
                // TODO: los siguientes campos no tienen equivalente en la respuesta REST de Experian
                FechaNacimiento            = null,
                Sexo                       = null,
                Digito                     = null,
                DigitoAnterior             = null,
                Ruc                        = null,
                RazonSocial                = ObtenerRazonSocial(infBas),
                NombreComercial            = infBas?.NomCom,
                FechaBaja                  = null,
                CodigoTipoContribuyente    = null,
                CondicionContribuyente     = null,
                CodigoCondicionContribuyente = null,
                ActividadEconomica2        = null,
                Ciiu2                      = null,
                ActividadEconomica3        = null,
                Ciiu3                      = null,
                Referencia                 = null,
                Departamento               = null,
                Provincia                  = null,
                Distrito                   = null,
                Ubigeo                     = null,
                FechaConstitucion          = ParsearFechaOpcional(infBas?.FchInsRRPP),
                ActvidadComercioExterior   = null,
                CodigoActividadComerExt    = null,
                CodigoDependencia          = null,
                Dependencia                = null,
                Tomo                       = null,
                Patron                     = null
            };
            resultado.InfGen = infGenDto;

            // ── 6. RepLegItem[] - InfGen.RepLeg[] ──
            if (resp.InfGen?.RepLeg != null)
            {
                foreach (var repLeg in resp.InfGen.RepLeg)
                {
                    resultado.RepLeg.Add(new SentinelRepLegItemDTO
                    {
                        TipoDocumento   = repLeg.TDoc ?? string.Empty,
                        NumeroDocumento = repLeg.NDoc ?? string.Empty,
                        RazonSocial     = repLeg.Nombre,
                        Cargo           = repLeg.Cargo,
                        SemaforoActual  = repLeg.SemAct ?? string.Empty,
                        // TODO: ApellidoPaterno, ApellidoMaterno y Nombres no vienen separados en REST
                        Nombres         = null,
                        ApellidoPaterno = null,
                        ApellidoMaterno = null
                    });
                }
            }

            // ── 7. PosHisItem[] - DeuSBSMicro[] historico (1 registro por periodo) ──
            if (conRap?.DeuSBSMicro != null)
            {
                foreach (var periodoHis in conRap.DeuSBSMicro)
                {
                    // Fecha de proceso: primer dia del mes del periodo
                    DateTime? fechaPeriodo = null;
                    if (periodoHis.Ano.HasValue && periodoHis.Mes.HasValue)
                    {
                        fechaPeriodo = new DateTime(periodoHis.Ano.Value, periodoHis.Mes.Value, 1);
                    }

                    // Deuda total del periodo: suma de SalDeu de todas las entidades
                    decimal? deudaTotalPeriodo = periodoHis.Entidad?
                        .Where(e => e.SalDeu.HasValue)
                        .Sum(e => e.SalDeu!.Value);

                    // Numero de entidades del periodo
                    int? nroEntidades = periodoHis.NroEnt;

                    resultado.PosHis.Add(new SentinelSdtPoshisItemDTO
                    {
                        TipoDocumento    = infBas?.TDoc,
                        NumeroDocumento  = infBas?.NDoc,
                        FechaProceso     = fechaPeriodo,
                        NumeroEntidades  = nroEntidades,
                        DeudaTotal       = deudaTotalPeriodo,
                        // Calificacion del primer registro de entidades del periodo
                        PeorCalificacion = ObtenerCodigoCalificacion(periodoHis.Entidad?.FirstOrDefault()?.Cal),
                        // TODO: los siguientes campos no tienen equivalente en REST
                        SemanaActual         = null,
                        DescripcionSemaforo  = null,
                        Score                = null,
                        CodigoVariacion      = null,
                        DescripcionVariacion = null,
                        PorcentajeCalificacion = null,
                        PeroCalificacionDescripcion = null,
                        MontoSbs             = null,
                        ProgresoRegistro     = null,
                        DocImpuesto          = null,
                        DeudaTributaria      = null,
                        Afp                  = null,
                        TarjetaCredito       = null,
                        CuentaCorriente      = null,
                        ReporteNegativo      = null,
                        DeudaDirecta         = null,
                        DeudaIndirecta       = null,
                        LineaCreditoNoUtilizada = null,
                        DeudaCastigada       = null
                    });
                }
            }

            return resultado;
        }

        // ──────────────────────────────────────────────────────────────────────
        // Metodos auxiliares de mapeo
        // ──────────────────────────────────────────────────────────────────────

        private static string? ObtenerRazonSocial(ExperianInfBas? infBas)
        {
            if (infBas == null) return null;
            // Persona natural: usar Nombre completo; empresa: usar RazSoc
            if (!string.IsNullOrWhiteSpace(infBas.RazSoc)) return infBas.RazSoc;
            var partes = new[] { infBas.ApePat, infBas.ApeMat, infBas.Nom }
                .Where(p => !string.IsNullOrWhiteSpace(p));
            return string.Join(" ", partes).Trim();
        }

        private static DateTime? ParsearFechaOpcional(string? valorFecha)
        {
            if (string.IsNullOrWhiteSpace(valorFecha)) return null;
            if (DateTime.TryParse(valorFecha, out var fecha)) return fecha;
            return null;
        }

        private static int? ObtenerCodigoCalificacion(string? calificacion)
        {
            if (int.TryParse(calificacion, out int codigo)) return codigo;
            return null;
        }
    }
}
