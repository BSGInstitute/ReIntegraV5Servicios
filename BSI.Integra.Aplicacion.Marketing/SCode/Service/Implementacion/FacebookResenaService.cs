using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion
{
    /// Servicio: FacebookResenaService
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// Versión: 17.0
    /// <summary>
    /// Lógica de negocio para la gestión de reseñas de Facebook de BSG Institute.
    /// CRUD, consultas de administración, sincronización con Graph API v25.0 y notificación por correo.
    /// </summary>
    public class FacebookResenaService : IFacebookResenaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Mapper      _mapper;

        // ── Configuración de la Graph API de Facebook ────────────────────────────
        private const string URL_BASE_FACEBOOK = "https://graph.facebook.com";
        private const string VERSION_API       = "v25.0";
        private const int    LIMITE_POR_LOTE   = 100;
        private const int    UMBRAL_ESTRATEGIA_ANUAL = 450;
        private const int    ANIO_INICIO       = 2017;
        private const int    MAX_REINTENTOS    = 5;
        private const int    ESPERA_BASE_MS    = 1500;
        private const string USUARIO_SISTEMA   = "sincronizacionFB";
        private const string USUARIO_FALLBACK = "system";

        /// <summary>
        /// Campos solicitados a la Graph API. Solo los que se persisten en BD.
        /// </summary>
        private const string CAMPOS_API =
            "created_time," +
            "has_review," +
            "recommendation_type," +
            "review_text," +
            "open_graph_story{id}";

        /// <summary>
        /// Obtiene los identificadores de las páginas de Facebook activas desde T_FacebookConfiguracion.
        /// </summary>
        private List<string> ObtenerIdsPaginasConfiguradas()
        {
            return _unitOfWork.FacebookConfiguracionRepository
                .GetBy(w => w.Estado == true)
                .Select(p => p.IdentificadorPagina)
                .ToList();
        }

        public FacebookResenaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TFacebookResena, FacebookResena>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region CRUD

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta una reseña y persiste los cambios.
        /// </summary>
        /// <param name="entidad">Entidad FacebookResena a insertar.</param>
        /// <returns>Entidad insertada.</returns>
        public FacebookResena Add(FacebookResena entidad)
        {
            var modelo = _unitOfWork.FacebookResenaRepository.Add(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<FacebookResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza una reseña y persiste los cambios.
        /// </summary>
        /// <param name="entidad">Entidad FacebookResena con los datos actualizados.</param>
        /// <returns>Entidad actualizada.</returns>
        public FacebookResena Update(FacebookResena entidad)
        {
            var modelo = _unitOfWork.FacebookResenaRepository.Update(entidad);
            _unitOfWork.Commit();
            return _mapper.Map<FacebookResena>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente una reseña por su Id.
        /// </summary>
        /// <param name="id">Identificador de la reseña a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si la eliminación fue exitosa.</returns>
        public bool Delete(int id, string usuario)
        {
            _unitOfWork.FacebookResenaRepository.Delete(id, usuario);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listadoEntidad">Listado de entidades FacebookResena a insertar.</param>
        /// <returns>Listado de entidades insertadas.</returns>
        public List<FacebookResena> Add(List<FacebookResena> listadoEntidad)
        {
            var modelo = _unitOfWork.FacebookResenaRepository.Add(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<FacebookResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un listado de reseñas en bloque.
        /// </summary>
        /// <param name="listadoEntidad">Listado de entidades FacebookResena con los datos actualizados.</param>
        /// <returns>Listado de entidades actualizadas.</returns>
        public List<FacebookResena> Update(List<FacebookResena> listadoEntidad)
        {
            var modelo = _unitOfWork.FacebookResenaRepository.Update(listadoEntidad);
            _unitOfWork.Commit();
            return _mapper.Map<List<FacebookResena>>(modelo);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Elimina lógicamente un listado de reseñas por sus Ids.
        /// </summary>
        /// <param name="listadoIds">Listado de identificadores de las reseñas a eliminar.</param>
        /// <param name="usuario">Usuario que realiza la eliminación.</param>
        /// <returns>true si la eliminación fue exitosa.</returns>
        public bool Delete(List<int> listadoIds, string usuario)
        {
            _unitOfWork.FacebookResenaRepository.Delete(listadoIds, usuario);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Consultas de Administración

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna la grilla paginada de reseñas con filtros opcionales.
        /// </summary>
        /// <param name="filtro">Filtros y paginación para la grilla.</param>
        /// <returns>Grilla paginada de reseñas.</returns>
        public FacebookResenaGrillaPaginadaDTO ObtenerGrilla(FacebookResenaGrillaFiltroDTO filtro)
        {
            return _unitOfWork.FacebookResenaRepository.ObtenerGrilla(filtro);
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las páginas configuradas con estadísticas agregadas.
        /// </summary>
        /// <returns>Listado de páginas con estadísticas agregadas.</returns>
        public List<FacebookResenaPaginaItemDTO> ObtenerPaginas()
        {
            return _unitOfWork.FacebookResenaRepository.ObtenerPaginas();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Retorna las cuentas de Facebook para el combo de filtros.
        /// </summary>
        /// <returns>Listado de cuentas de Facebook para combo.</returns>
        public List<FacebookResenaCuentaComboDTO> ObtenerCuentasCombo()
        {
            return _unitOfWork.FacebookResenaRepository.ObtenerCuentasCombo();
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca las reseñas indicadas como visibles (Mostrar=true) y persiste.
        /// </summary>
        /// <param name="dto">DTO con los Ids de reseñas y el usuario que realiza la acción.</param>
        /// <returns>true si la operación fue exitosa.</returns>
        public bool MarcarResenaVisible(FacebookResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.FacebookResenaRepository.MarcarResenaVisible(
                dto.IdsFacebookResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Marca las reseñas indicadas como ocultas (Mostrar=false) y persiste.
        /// </summary>
        /// <param name="dto">DTO con los Ids de reseñas y el usuario que realiza la acción.</param>
        /// <returns>true si la operación fue exitosa.</returns>
        public bool MarcarResenaOculta(FacebookResenaMarcarMostrarDTO dto)
        {
            _unitOfWork.FacebookResenaRepository.MarcarResenaOculta(
                dto.IdsFacebookResena, dto.Usuario ?? USUARIO_FALLBACK);
            _unitOfWork.Commit();
            return true;
        }

        #endregion

        #region Sincronización con Facebook

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 17.0
        /// <summary>
        /// Sincroniza las reseñas de todas las páginas de Facebook configuradas.
        /// Proceso secuencial (thread-safe con un solo DbContext).
        /// Al finalizar envía un correo con el resumen del procesamiento.
        /// </summary>
        /// <param name="nombreUsuario">UserName del usuario logueado que ejecuta la sincronización.</param>
        /// <returns>JSON con el resumen de sincronización.</returns>
        public async Task<string> SincronizarFacebookApi(string nombreUsuario)
        {
            var emailDestinatario = ObtenerEmailUsuario(nombreUsuario);
            var inicio              = DateTime.UtcNow;
            var resultadosPorPagina = new List<FacebookResenaPaginaResultadoDTO>();

            using var clienteHttp = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };

            foreach (var idPagina in ObtenerIdsPaginasConfiguradas())
            {
                var resultado = new FacebookResenaPaginaResultadoDTO
                {
                    IdentificadorPagina = idPagina,
                    LimitUtilizado      = LIMITE_POR_LOTE
                };

                try
                {
                    var configuracionPagina = _unitOfWork.FacebookResenaRepository
                        .ObtenerFacebookConfiguracionPagina(idPagina);

                    if (configuracionPagina == null || string.IsNullOrWhiteSpace(configuracionPagina.TokenAccesoPagina))
                    {
                        resultado.Exitoso         = false;
                        resultado.MensajeError    = $"Sin token para página {idPagina}.";
                        resultado.EstrategiaUsada = "SinToken";
                        resultadosPorPagina.Add(resultado);
                        continue;
                    }

                    resultado.NombrePagina = configuracionPagina.NombrePagina;

                    var resenasExistentes = _unitOfWork.FacebookResenaRepository
                        .ObtenerResenasPorPagina(configuracionPagina.Id)
                        .ToDictionary(r => r.IdentificadorHistoria ?? string.Empty, StringComparer.Ordinal);

                    await ProcesarPaginaAsync(clienteHttp, configuracionPagina, resenasExistentes, resultado);
                }
                catch (Exception ex)
                {
                    resultado.Exitoso      = false;
                    resultado.MensajeError = ex.Message;
                    Debug.WriteLine($"[FB] ERROR {idPagina}: {ex.Message}");
                }

                resultadosPorPagina.Add(resultado);
            }

            var resumen = ConstruirResumen(inicio, resultadosPorPagina);
            EnviarCorreoResumen(resumen, resultadosPorPagina, emailDestinatario);

            return JsonConvert.SerializeObject(
                new FacebookResenaSincronizacionDTO
                {
                    Resumen             = resumen,
                    ResultadosPorPagina = resultadosPorPagina
                },
                Formatting.Indented);
        }

        /// <summary>
        /// Procesa una página individual: descarga reseñas, deduplica y persiste.
        /// Elige automáticamente entre cursor directo o ventanas anuales según el volumen.
        /// </summary>
        private async Task ProcesarPaginaAsync(
            HttpClient                         clienteHttp,
            FacebookConfiguracionPaginaDTO                 configuracionPagina,
            Dictionary<string, FacebookResena> resenasExistentes,
            FacebookResenaPaginaResultadoDTO   resultado)
        {
            int totalNuevas       = 0;
            int totalActualizadas = 0;
            int totalSinCambios   = 0;
            int totalRequests     = 0;
            var erroresPorAnio    = new List<string>();

            var (totalOpinionesFb, calificacionFb) =
                await ObtenerEstadisticasPaginaAsync(clienteHttp, configuracionPagina.IdentificadorPagina, configuracionPagina.TokenAccesoPagina);
            resultado.TotalOpinionesFacebook = totalOpinionesFb;
            resultado.CalificacionGlobal     = calificacionFb;

            var urlInicial = ConstruirUrlGraphApi(configuracionPagina.IdentificadorPagina, configuracionPagina.TokenAccesoPagina,
                                         LIMITE_POR_LOTE, incluirResumen: true);
            var respuestaInicial = await ConsultarGraphApiAsync(clienteHttp, urlInicial, configuracionPagina.IdentificadorPagina);
            totalRequests++;

            resultado.TotalOpiniones = respuestaInicial.TotalCount;

            if (respuestaInicial.Lote.Any())
            {
                var (n, a, s) = PersistirLoteInmediato(respuestaInicial.Lote, resenasExistentes, configuracionPagina.Id);
                totalNuevas += n; totalActualizadas += a; totalSinCambios += s;
            }

            bool usarVentanasAnuales = respuestaInicial.TotalCount > UMBRAL_ESTRATEGIA_ANUAL
                          || (respuestaInicial.TotalCount == 0
                              && respuestaInicial.Lote.Count == LIMITE_POR_LOTE
                              && !string.IsNullOrEmpty(respuestaInicial.UrlSiguiente));

            if (!usarVentanasAnuales)
            {
                // ── Estrategia A: cursor directo ──────────────────────────────
                resultado.EstrategiaUsada = $"CursorDirecto (total={respuestaInicial.TotalCount})";
                var urlActual = respuestaInicial.UrlSiguiente;
                while (!string.IsNullOrEmpty(urlActual))
                {
                    var resp = await ConsultarGraphApiAsync(clienteHttp, urlActual, configuracionPagina.IdentificadorPagina);
                    totalRequests++;
                    urlActual = resp.UrlSiguiente;
                    if (resp.Lote.Any())
                    {
                        var (n, a, s) = PersistirLoteInmediato(resp.Lote, resenasExistentes, configuracionPagina.Id);
                        totalNuevas += n; totalActualizadas += a; totalSinCambios += s;
                    }
                }
            }
            else
            {
                // ── Estrategia B: ventanas anuales (desde 2017) ───────────────
                resultado.EstrategiaUsada = $"VentanasAnuales (total={respuestaInicial.TotalCount})";
                var anioActual = DateTime.UtcNow.Year;

                for (int anio = ANIO_INICIO; anio <= anioActual; anio++)
                {
                    try
                    {
                        var desde = new DateTimeOffset(anio, 1, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds();
                        var hasta = anio < anioActual
                            ? new DateTimeOffset(anio + 1, 1, 1, 0, 0, 0, TimeSpan.Zero).ToUnixTimeSeconds()
                            : DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                        var urlActual   = ConstruirUrlGraphApi(configuracionPagina.IdentificadorPagina, configuracionPagina.TokenAccesoPagina,
                                                       LIMITE_POR_LOTE, desde: desde, hasta: hasta);
                        var resenasAnio = 0;

                        while (!string.IsNullOrEmpty(urlActual))
                        {
                            var resp = await ConsultarGraphApiAsync(clienteHttp, urlActual, configuracionPagina.IdentificadorPagina);
                            totalRequests++;
                            urlActual = resp.UrlSiguiente;
                            if (resp.Lote.Any())
                            {
                                var (n, a, s) = PersistirLoteInmediato(resp.Lote, resenasExistentes, configuracionPagina.Id);
                                totalNuevas += n; totalActualizadas += a; totalSinCambios += s;
                                resenasAnio += resp.Lote.Count;
                            }
                        }

                        if (resenasAnio > 0)
                            Debug.WriteLine($"[FB] {configuracionPagina.NombrePagina} | {anio}: {resenasAnio} reseñas");
                    }
                    catch (Exception ex)
                    {
                        var msg = $"Año {anio}: {ex.Message}";
                        erroresPorAnio.Add(msg);
                        Debug.WriteLine($"[FB] ERROR {configuracionPagina.NombrePagina} | {msg}");
                    }
                }
            }

            resultado.ResenasNuevas          = totalNuevas;
            resultado.ResenasActualizadas    = totalActualizadas;
            resultado.ResenasSinCambios      = totalSinCambios;
            resultado.TotalDescargadas       = totalNuevas + totalActualizadas + totalSinCambios;
            resultado.TotalResenasProcesadas = resultado.TotalDescargadas;
            resultado.TotalRequests          = totalRequests;
            resultado.Exitoso                = true;

            if (erroresPorAnio.Any())
                resultado.MensajeError = $"{erroresPorAnio.Count} año(s) con error: " +
                                         string.Join(" | ", erroresPorAnio.Take(5));

            var totalRegistros  = resenasExistentes.Count;
            var totalPositivos = resenasExistentes.Values.Count(r => r.Recomienda == true);
            resultado.PorcentajeRecomendacion = totalRegistros > 0
                ? Math.Round((decimal)totalPositivos / totalRegistros * 100, 1) : 0m;

            if (resultado.TotalOpiniones == 0)
                resultado.TotalOpiniones = totalRegistros;

            Debug.WriteLine($"[FB] {configuracionPagina.NombrePagina} COMPLETADO | nuevas={totalNuevas} " +
                            $"act={totalActualizadas} sin={totalSinCambios} req={totalRequests}");
        }

        /// <summary>
        /// Consulta un endpoint de la Graph API con reintentos y backoff exponencial.
        /// </summary>
        private async Task<RespuestaGraphApi> ConsultarGraphApiAsync(HttpClient clienteHttp, string url, string idPagina)
        {
            int intentos = 0;
            while (true)
            {
                try
                {
                    var httpResp  = await clienteHttp.GetAsync(url);
                    var contenido = await httpResp.Content.ReadAsStringAsync();

                    if (!httpResp.IsSuccessStatusCode)
                    {
                        dynamic errorJson = JsonConvert.DeserializeObject<dynamic>(contenido);
                        int     codigoFb  = (int?)errorJson?.error?.code ?? (int)httpResp.StatusCode;
                        string  mensajeFb = (string)errorJson?.error?.message ?? contenido;

                        bool esRecuperable = codigoFb == 4 || codigoFb == 17 || codigoFb == 341
                                          || (int)httpResp.StatusCode == 429
                                          || (int)httpResp.StatusCode >= 500;

                        if (esRecuperable && intentos < MAX_REINTENTOS - 1)
                        {
                            intentos++;
                            var espera = ESPERA_BASE_MS * (int)Math.Pow(2, intentos - 1)
                                         + new Random().Next(300, 900);
                            await Task.Delay(espera);
                            continue;
                        }
                        throw new Exception($"Error Facebook ({codigoFb}): {mensajeFb}");
                    }

                    dynamic json = JsonConvert.DeserializeObject<dynamic>(contenido);
                    var lote = new List<FacebookResenaGrafApiDTO>();

                    if (json?.data != null)
                        foreach (var item in json.data)
                        {
                            var dto = ConvertirItemGraphApiADto(item);
                            if (dto != null && !string.IsNullOrEmpty(dto.StoryId))
                                lote.Add(dto);
                        }

                    return new RespuestaGraphApi
                    {
                        Lote         = lote,
                        UrlSiguiente = (string)json?.paging?.next,
                        TotalCount   = (int?)json?.summary?.total_count ?? 0
                    };
                }
                catch (Exception ex) when (ex is HttpRequestException || ex is TaskCanceledException)
                {
                    if (intentos >= MAX_REINTENTOS - 1)
                        throw new Exception($"Sin conexión con Facebook tras {MAX_REINTENTOS} intentos: {ex.Message}");
                    intentos++;
                    await Task.Delay(ESPERA_BASE_MS * intentos);
                }
            }
        }

        /// <summary>
        /// Procesa y persiste un lote de reseñas de forma inmediata.
        /// INSERT para nuevas, UPDATE para las que cambiaron. Mostrar nunca se toca.
        /// </summary>
        private (int nuevas, int actualizadas, int sinCambios) PersistirLoteInmediato(
            IEnumerable<FacebookResenaGrafApiDTO>    lote,
            Dictionary<string, FacebookResena>       resenasExistentes,
            int                                      idConfiguracionPagina)
        {
            var porInsertar   = new List<FacebookResena>();
            var porActualizar = new List<FacebookResena>();
            int sinCambios    = 0;

            foreach (var dto in lote.Where(d => !string.IsNullOrEmpty(d.StoryId)))
            {
                if (resenasExistentes.TryGetValue(dto.StoryId, out var existente))
                {
                    if (TieneCambiosRespectoDatos(existente, dto))
                    {
                        AplicarCambiosDesdeGraphApi(existente, dto);
                        porActualizar.Add(existente);
                    }
                    else { sinCambios++; }
                }
                else
                {
                    var nueva = CrearResenaDesdeGraphApi(dto, idConfiguracionPagina);
                    porInsertar.Add(nueva);
                    resenasExistentes[dto.StoryId] = nueva;
                }
            }

            if (porInsertar.Any())   _unitOfWork.FacebookResenaRepository.Add(porInsertar);
            if (porActualizar.Any()) _unitOfWork.FacebookResenaRepository.Update(porActualizar);
            if (porInsertar.Any() || porActualizar.Any()) _unitOfWork.Commit();

            return (porInsertar.Count, porActualizar.Count, sinCambios);
        }

        #endregion

        #region Construcción de URLs y conversión de datos

        /// <summary>Construye la URL para consultar /ratings de la Graph API.</summary>
        private string ConstruirUrlGraphApi(string idPagina, string token, int limite,
            long? desde = null, long? hasta = null, bool incluirResumen = false)
        {
            var sb = new StringBuilder($"{URL_BASE_FACEBOOK}/{VERSION_API}/{idPagina}/ratings");
            sb.Append($"?fields={Uri.EscapeDataString(CAMPOS_API)}");
            sb.Append($"&limit={limite}");
            if (desde.HasValue)  sb.Append($"&since={desde}");
            if (hasta.HasValue)  sb.Append($"&until={hasta}");
            if (incluirResumen)  sb.Append("&summary=true");
            sb.Append($"&access_token={token}");
            return sb.ToString();
        }

        /// <summary>Obtiene rating_count y overall_star_rating de una página.</summary>
        private async Task<(int totalOpinionesFacebook, decimal calificacionGlobal)>
            ObtenerEstadisticasPaginaAsync(HttpClient clienteHttp, string idPagina, string token)
        {
            try
            {
                var url = $"{URL_BASE_FACEBOOK}/{VERSION_API}/{idPagina}" +
                          $"?fields=rating_count,overall_star_rating&access_token={token}";
                var httpResp  = await clienteHttp.GetAsync(url);
                var contenido = await httpResp.Content.ReadAsStringAsync();
                if (!httpResp.IsSuccessStatusCode) return (0, 0m);
                dynamic json  = JsonConvert.DeserializeObject<dynamic>(contenido);
                int     total = (int?)json?.rating_count ?? 0;
                decimal calif = (decimal?)json?.overall_star_rating ?? 0m;
                return (total, Math.Round(calif, 1));
            }
            catch { return (0, 0m); }
        }

        /// <summary>Convierte un item del JSON de la Graph API al DTO interno.</summary>
        private FacebookResenaGrafApiDTO ConvertirItemGraphApiADto(dynamic item)
        {
            try
            {
                var tipoRecomendacion = (string)item?.recommendation_type;
                return new FacebookResenaGrafApiDTO
                {
                    StoryId     = (string)item?.open_graph_story?.id,
                    Recomienda  = string.Equals(tipoRecomendacion, "positive", StringComparison.OrdinalIgnoreCase),
                    TieneTexto  = (bool?)item?.has_review ?? false,
                    TextoResena = (string)item?.review_text,
                    FechaResena = (DateTime?)item?.created_time ?? DateTime.UtcNow
                };
            }
            catch { return null; }
        }

        /// <summary>Compara si la reseña existente difiere de los datos de la API.</summary>
        private bool TieneCambiosRespectoDatos(FacebookResena existente, FacebookResenaGrafApiDTO dto)
        {
            if (existente.Recomienda != dto.Recomienda) return true;
            if (existente.TieneTexto != dto.TieneTexto) return true;
            if ((existente.TextoResena ?? "") != (dto.TextoResena ?? "")) return true;
            return false;
        }

        /// <summary>Aplica los cambios de la API sobre una reseña existente.</summary>
        private void AplicarCambiosDesdeGraphApi(FacebookResena existente, FacebookResenaGrafApiDTO dto)
        {
            existente.Recomienda          = dto.Recomienda;
            existente.TieneTexto          = dto.TieneTexto;
            existente.TextoResena         = dto.TextoResena ?? string.Empty;
            existente.UsuarioModificacion = USUARIO_SISTEMA;
            existente.FechaModificacion   = DateTime.Now;
        }

        /// <summary>Crea una nueva entidad FacebookResena a partir de datos de la API.</summary>
        private FacebookResena CrearResenaDesdeGraphApi(FacebookResenaGrafApiDTO dto, int idConfiguracionPagina)
        {
            var ahora = DateTime.Now;
            return new FacebookResena
            {
                IdFacebookConfiguracion = idConfiguracionPagina,
                IdentificadorHistoria          = dto.StoryId,
                Recomienda                     = dto.Recomienda,
                TieneTexto                     = dto.TieneTexto,
                TextoResena                    = dto.TextoResena ?? string.Empty,
                FechaResena                    = dto.FechaResena,
                Mostrar                        = false,
                Estado                         = true,
                UsuarioCreacion                = USUARIO_SISTEMA,
                UsuarioModificacion            = USUARIO_SISTEMA,
                FechaCreacion                  = ahora,
                FechaModificacion              = ahora
            };
        }

        /// <summary>Construye el DTO resumen global de la sincronización.</summary>
        private FacebookResenaSincronizacionResumenDTO ConstruirResumen(
            DateTime inicio, List<FacebookResenaPaginaResultadoDTO> paginas)
            => new FacebookResenaSincronizacionResumenDTO
            {
                FechaSincronizacion      = inicio,
                TotalPaginasProcesadas   = paginas.Count(r => r.Exitoso),
                TotalPaginasConError     = paginas.Count(r => !r.Exitoso),
                TotalResenasProcesadas   = paginas.Sum(r => r.TotalResenasProcesadas),
                TotalResenasNuevas       = paginas.Sum(r => r.ResenasNuevas),
                TotalResenasActualizadas = paginas.Sum(r => r.ResenasActualizadas),
                TotalResenasSinCambios   = paginas.Sum(r => r.ResenasSinCambios),
                DuracionSegundos         = (int)(DateTime.UtcNow - inicio).TotalSeconds
            };

        #endregion

        #region Notificación por correo

        private const string CORREO_COPIA_OCULTA = "mmantilla@bsginstitute.com";
        private const string CORREO_REMITENTE = "soporte@bsginstitute.com";

        /// Autor: Max Mantilla.
        /// Fecha: 21/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el email del usuario logueado a partir de su UserName.
        /// Fallback al correo de copia oculta si no se encuentra.
        /// </summary>
        /// <param name="nombreUsuario">UserName del usuario logueado.</param>
        /// <returns>Email del usuario o correo de copia oculta como fallback.</returns>
        private string ObtenerEmailUsuario(string nombreUsuario)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombreUsuario)) return CORREO_COPIA_OCULTA;
                var email = _unitOfWork.IntegraAspNetUserRepository.ObtenerEmailPorNombreUsuario(nombreUsuario);
                return string.IsNullOrWhiteSpace(email) ? CORREO_COPIA_OCULTA : email;
            }
            catch { return CORREO_COPIA_OCULTA; }
        }

        /// <summary>
        /// Envía un correo HTML profesional con el resumen de la sincronización.
        /// Destinatario: usuario que ejecutó. Copia oculta: mmantilla@bsginstitute.com.
        /// Los errores de envío no interrumpen el flujo principal.
        /// </summary>
        private void EnviarCorreoResumen(
            FacebookResenaSincronizacionResumenDTO resumen,
            List<FacebookResenaPaginaResultadoDTO> paginas,
            string emailDestinatario)
        {
            try
            {
                var hayErrores    = paginas.Any(p => !p.Exitoso || !string.IsNullOrEmpty(p.MensajeError));
                var estadoGeneral = hayErrores ? "Con advertencias" : "Exitoso";
                var colorEstado   = hayErrores ? "#e67e22" : "#27ae60";
                var iconoEstado   = hayErrores ? "&#9888;" : "&#10004;";

                var filasHtml = new StringBuilder();
                foreach (var p in paginas)
                {
                    var colorFila   = p.Exitoso ? "#27ae60" : "#c0392b";
                    var textoEstado = p.Exitoso ? "OK" : "Error";
                    var bgFila      = p.Exitoso ? "#fff" : "#fef3cd";

                    filasHtml.Append($@"
                    <tr style='background:{bgFila}'>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee'>{p.NombrePagina ?? p.IdentificadorPagina}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center;color:{colorFila};font-weight:600'>{textoEstado}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center'>{p.ResenasNuevas}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center'>{p.ResenasActualizadas}</td>
                      <td style='padding:8px 12px;border-bottom:1px solid #eee;text-align:center;font-weight:600'>{p.PorcentajeRecomendacion}%</td>
                    </tr>");
                }

                var paginasConError = paginas.Where(p => !string.IsNullOrEmpty(p.MensajeError)).ToList();
                var seccionErrores  = "";
                if (paginasConError.Any())
                {
                    var listaErrores = new StringBuilder();
                    foreach (var p in paginasConError)
                        listaErrores.Append($"<li style='margin-bottom:4px'><b>{p.NombrePagina ?? p.IdentificadorPagina}:</b> {p.MensajeError}</li>");

                    seccionErrores = $@"
                    <div style='background:#fef3cd;padding:12px 16px;border-radius:6px;margin-top:16px;border-left:4px solid #e67e22'>
                      <p style='margin:0 0 6px;font-size:13px;font-weight:600;color:#856404'>Observaciones</p>
                      <ul style='margin:0;padding-left:18px;font-size:12px;color:#856404'>{listaErrores}</ul>
                    </div>";
                }

                var mailService = new TMK_MailService();
                var mailData = new TMKMailDataDTO
                {
                    Sender    = CORREO_REMITENTE,
                    Recipient = emailDestinatario,
                    Subject   = $"Sincronizacion Resenas Facebook - {estadoGeneral} - {resumen.TotalResenasNuevas} nueva(s) - {resumen.FechaSincronizacion:dd/MM/yyyy HH:mm}",
                    Cc        = "",
                    Bcc       = CORREO_COPIA_OCULTA,
                    AttachedFiles = null,
                    Message   = $@"
<div style='font-family:Segoe UI,Arial,sans-serif;max-width:640px;margin:0 auto;background:#ffffff'>

  <!-- Header -->
  <div style='background:#094D82;padding:20px 24px;border-radius:8px 8px 0 0'>
    <h1 style='margin:0;font-size:16px;color:#ffffff;font-weight:600'>Sincronizacion de Resenas Facebook</h1>
    <p style='margin:4px 0 0;font-size:12px;color:rgba(255,255,255,0.7)'>{resumen.FechaSincronizacion:dd/MM/yyyy HH:mm} UTC</p>
  </div>

  <div style='padding:20px 24px;border:1px solid #e5e7eb;border-top:none;border-radius:0 0 8px 8px'>

    <!-- Estado -->
    <div style='display:flex;align-items:center;padding:12px 16px;background:#f8f9fa;border-radius:6px;margin-bottom:20px;border-left:4px solid {colorEstado}'>
      <span style='font-size:18px;margin-right:8px'>{iconoEstado}</span>
      <span style='font-size:14px;color:#333'>Estado: <b style='color:{colorEstado}'>{estadoGeneral}</b></span>
    </div>

    <!-- Metricas -->
    <table style='width:100%;margin-bottom:20px' cellpadding='0' cellspacing='0'>
      <tr>
        <td style='width:33%;text-align:center;padding:12px 8px;background:#f0f9ff;border-radius:6px'>
          <div style='font-size:22px;font-weight:700;color:#094D82'>{resumen.TotalResenasNuevas}</div>
          <div style='font-size:11px;color:#6b7280;margin-top:2px'>Nuevas</div>
        </td>
        <td style='width:8px'></td>
        <td style='width:33%;text-align:center;padding:12px 8px;background:#f0f9ff;border-radius:6px'>
          <div style='font-size:22px;font-weight:700;color:#094D82'>{resumen.TotalResenasActualizadas}</div>
          <div style='font-size:11px;color:#6b7280;margin-top:2px'>Actualizadas</div>
        </td>
        <td style='width:8px'></td>
        <td style='width:33%;text-align:center;padding:12px 8px;background:#f0f9ff;border-radius:6px'>
          <div style='font-size:22px;font-weight:700;color:#094D82'>{resumen.DuracionSegundos}s</div>
          <div style='font-size:11px;color:#6b7280;margin-top:2px'>Duracion</div>
        </td>
      </tr>
    </table>

    <!-- Tabla detalle -->
    <table style='width:100%;border-collapse:collapse;font-size:13px'>
      <thead>
        <tr style='background:#094D82'>
          <th style='padding:10px 12px;text-align:left;color:#fff;font-weight:600;border-radius:6px 0 0 0'>Pagina</th>
          <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Estado</th>
          <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Nuevas</th>
          <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600'>Actualizadas</th>
          <th style='padding:10px 12px;text-align:center;color:#fff;font-weight:600;border-radius:0 6px 0 0'>% Recom.</th>
        </tr>
      </thead>
      <tbody>{filasHtml}</tbody>
    </table>

    {seccionErrores}
  </div>

  <!-- Footer -->
  <div style='text-align:center;padding:16px 0'>
    <p style='margin:0;font-size:11px;color:#9ca3af'>BSG Institute &middot; {DateTime.Now:yyyy} &middot; Integra V5</p>
  </div>

</div>"
                };

                mailService.SetData(mailData);
                mailService.SendMessageTask();
            }
            catch { /* Errores de correo no interrumpen el proceso */ }
        }

        #endregion

        #region Clases internas

        /// <summary>Modelo interno para la respuesta de un request a la Graph API.</summary>
        private class RespuestaGraphApi
        {
            public List<FacebookResenaGrafApiDTO> Lote         { get; set; } = new();
            public string                         UrlSiguiente { get; set; }
            public int                            TotalCount   { get; set; }
        }

        #endregion
    }
}
