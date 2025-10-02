using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System.Collections.Concurrent;
using System.Globalization;
using System.Text.RegularExpressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: AgendaInformacionActividadService
    /// Autor: Flavio Rodrigo Mamani Fabian
    /// Fecha: 09/03/2023
    /// <summary>
    /// Gestión general de T_AgendaInformacionActividad
    /// </summary>
    public class AgendaInformacionActividadService : IAgendaInformacionActividadService
    {
        private IUnitOfWork _unitOfWork;
        public AgendaInformacionActividadService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 01/08/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la Oportunidad Compuesta y el Programa Especifico asociados a una Actividad Detalle
        /// </summary>
        /// <param name="idActividadDetalle">Id de la Actividad Detalle</param>
        /// <returns> Retorna 200 y objeto o 400 y mensaje de error </returns>
        public (OportunidadCompuestoDTO? Oportunidad, PEspecificoPorIdCentroCostoDTO? PEspecifico) ObtenerOportunidadYPEspecificoPorIdActividadDetalle(int idActividadDetalle)
        {
            try
            {
                IOportunidadService oportunidadService = new OportunidadService(_unitOfWork);
                IPEspecificoService pEspecificoService = new PEspecificoService(_unitOfWork);
                var oportunidadCompuesto = _unitOfWork.OportunidadRepository.ObtenerOportunidadCompuestoPorIdActividadDetalle(idActividadDetalle);
                var programaEspecifico = _unitOfWork.PEspecificoRepository.ObtenerPorIdCentroCosto(oportunidadCompuesto.IdCentroCosto.GetValueOrDefault());
                if (oportunidadCompuesto.Id == 0)
                    oportunidadCompuesto = null;
                if (programaEspecifico.Id == 0)
                    programaEspecifico = null;
                return (oportunidadCompuesto, programaEspecifico);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //LOLO
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtener resumen programa para Modalidades
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <param name="idCentroCosto">Id de Centro de Costo</param>
        /// <returns>object</returns> 
        public async Task<object> ObtenerResumenPrograma(int idPGeneral, int idCentroCosto)
        {
            var lista = await _unitOfWork.DocumentoAgendaRepository.ObtenerResumenProgramaPorIdPGeneral(idPGeneral, idCentroCosto);

            if (lista == null || !lista.Any())
                return new
                {
                    IdPGeneral = idPGeneral,
                    IdCentroCosto = idCentroCosto,
                    EsProgramaOCurso = (string?)null,
                    Modalidades = new object[0],
                    Error = "No se encontraron datos"
                };

            var primer = lista.First();
            return new
            {
                IdPGeneral = primer.IdProgramaGeneral,
                IdCentroCosto = primer.IdCentroCosto,
                EsProgramaOCurso = primer.Categoria,
                Modalidades = lista.Select(x => new
                {
                    Tipo = x.Tipo,
                    CentroCosto = x.NombreCentroCosto,
                    FechaInicio = x.FechaCreacion.ToString("MMM. yyyy", new CultureInfo("es-ES"))
                }).ToList(),
                Error = (string?)null
            };
        }


        private static readonly ConcurrentDictionary<int, (ObjetivosResponseDTO Data, DateTime Expiry)> _cache =
    new ConcurrentDictionary<int, (ObjetivosResponseDTO, DateTime)>();
        private static readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(15);

        // Regex compiladas para mejor performance
        private static readonly Regex _htmlTagRegex = new Regex("<.*?>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly Regex _splitRegex = new Regex("</p>|</li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get objetivos
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>ObjetivosResponseDTO</returns> 
        public async Task<ObjetivosResponseDTO> GetObjetivosAsync(int idPGeneral)
        {
            // Verificar cache primero
            if (_cache.TryGetValue(idPGeneral, out var cached) && cached.Expiry > DateTime.UtcNow)
            {
                return cached.Data;
            }

            // Timeout para toda la operación
            using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(800));

            try
            {
                var data = await _unitOfWork.DocumentoAgendaRepository.GetObjetivosRawAsync(idPGeneral);

                ObjetivosResponseDTO result;

                if (data == null)
                {
                    result = new ObjetivosResponseDTO
                    {
                        IdPGeneral = idPGeneral,
                        EsProgramaOCurso = "N/A",
                        Objetivos = new List<string>(),
                        Error = "No se encontraron objetivos"
                    };
                }
                else
                {
                    var objetivos = ExtraerObjetivosOptimizado(data.ObjetivosHtml);
                    result = new ObjetivosResponseDTO
                    {
                        IdPGeneral = data.IdPGeneral,
                        EsProgramaOCurso = data.EsProgramaOCurso,
                        Objetivos = objetivos,
                        Error = null
                    };
                }

                // Guardar en cache con limpieza automática
                var expiry = DateTime.UtcNow.Add(_cacheExpiration);
                _cache.AddOrUpdate(idPGeneral, (result, expiry), (key, old) => (result, expiry));

                // Limpiar cache expirado (máximo 100 elementos)
                if (_cache.Count > 100)
                {
                    LimpiarCacheExpirado();
                }

                return result;
            }
            catch (TaskCanceledException)
            {
                // Devolver resultado desde cache si existe, aunque esté expirado
                if (_cache.TryGetValue(idPGeneral, out var expiredCache))
                {
                    return expiredCache.Data;
                }

                // Si no hay cache, devolver error
                return new ObjetivosResponseDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "N/A",
                    Objetivos = new List<string>(),
                    Error = "Timeout - servicio no disponible"
                };
            }
        }

        // Versión optimizada del procesamiento HTML
        private List<string> ExtraerObjetivosOptimizado(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return new List<string>();

            var objetivos = new List<string>();

            // Usar regex compiladas
            var partes = _splitRegex.Split(html);

            foreach (var parte in partes)
            {
                if (string.IsNullOrWhiteSpace(parte)) continue;

                var texto = _htmlTagRegex.Replace(parte, string.Empty).Trim();
                if (!string.IsNullOrEmpty(texto))
                {
                    objetivos.Add(System.Net.WebUtility.HtmlDecode(texto));
                }
            }

            return objetivos;
        }

        // Limpieza automática de cache
        private void LimpiarCacheExpirado()
        {
            var now = DateTime.UtcNow;
            var expiredKeys = _cache
                .Where(kvp => kvp.Value.Expiry <= now)
                .Select(kvp => kvp.Key)
                .Take(50) // Limpiar máximo 50 por vez
                .ToList();

            foreach (var key in expiredKeys)
            {
                _cache.TryRemove(key, out _);
            }
        }

        // Método para invalidar cache manualmente si es necesario
        public void InvalidarCache(int idPGeneral)
        {
            _cache.TryRemove(idPGeneral, out _);
        }

        // Método para limpiar todo el cache si es necesario
        public void LimpiarTodoCache()
        {
            _cache.Clear();
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get beneficios programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>BeneficioProgramaResponseDTO</returns> 
        public async Task<BeneficioProgramaResponseDTO> GetBeneficiosProgramaAsync(int idPGeneral)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.GetBeneficiosRawAsync(idPGeneral);

            if (data == null || !data.Any())
            {
                return new BeneficioProgramaResponseDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "N/A",
                    Beneficios = new List<BeneficioVersionDTO>(),
                    Error = "No se encontraron beneficios"
                };
            }

            var esProgramaOCurso = data.First().EsProgramaOCurso;

            // Agrupa los beneficios por versión
            var beneficiosPorVersion = data
                .GroupBy(x => x.Version)
                .Select(g => new BeneficioVersionDTO
                {
                    Version = g.Key,
                    Beneficios = g.Where(b => !string.IsNullOrEmpty(b.Beneficio)).Select(b => b.Beneficio).Distinct().ToList(),
                    Nota = g.Where(b => !string.IsNullOrEmpty(b.Nota)).Select(b => b.Nota).FirstOrDefault()
                })
                .ToList();

            return new BeneficioProgramaResponseDTO
            {
                IdPGeneral = idPGeneral,
                EsProgramaOCurso = esProgramaOCurso,
                Beneficios = beneficiosPorVersion,
                Error = null
            };
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get certificaciones programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>CertificacionProgramaResponseDTO</returns> 
        public async Task<CertificacionProgramaResponseDTO> GetCertificacionesProgramaAsync(int idPGeneral)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.GetCertificacionesRawAsync(idPGeneral);

            if (data == null || !data.Any())
            {
                return new CertificacionProgramaResponseDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "N/A",
                    Certificacion = new List<string>(),
                    Error = "No se encontraron certificaciones"
                };
            }

            var cabecera = data.FirstOrDefault(x => !string.IsNullOrEmpty(x.Cabecera))?.Cabecera;
            var nota = data.FirstOrDefault(x => !string.IsNullOrEmpty(x.Nota))?.Nota;

            var certificaciones = new List<string>();
            if (!string.IsNullOrEmpty(cabecera))
                certificaciones.Add(cabecera);

            certificaciones.AddRange(data
                .Where(x => !string.IsNullOrEmpty(x.Beneficio))
                .Select(x => x.Beneficio)
                .Distinct());

            if (!string.IsNullOrEmpty(nota))
                certificaciones.Add(nota);

            return new CertificacionProgramaResponseDTO
            {
                IdPGeneral = data.First().IdPGeneral,
                EsProgramaOCurso = data.First().EsProgramaOCurso,
                Certificacion = certificaciones,
                Error = null
            };
        }

        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get metodologia programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>MetodologiaProgramaResponseDTO</returns> 
        public async Task<MetodologiaProgramaResponseDTO> GetMetodologiaProgramaAsync(int idPGeneral)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.GetMetodologiaRawAsync(idPGeneral);

            if (data == null)
            {
                return new MetodologiaProgramaResponseDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "N/A",
                    Metodologia = null,
                    Error = "No se encontró información de metodología"
                };
            }

            // Extraer descripción y componentes del HTML
            string descripcion = "";
            List<string> componentes = new List<string>();

            if (!string.IsNullOrEmpty(data.ContenidoMetodologia))
            {
                var html = data.ContenidoMetodologia;
                // Extraer la descripción (primer <p>)
                var matchDesc = System.Text.RegularExpressions.Regex.Match(html, @"<p>(.*?)<\/p>", System.Text.RegularExpressions.RegexOptions.Singleline);
                if (matchDesc.Success)
                    descripcion = System.Net.WebUtility.HtmlDecode(matchDesc.Groups[1].Value.Trim());

                // Extraer los <li>
                var matches = System.Text.RegularExpressions.Regex.Matches(html, @"<li>(.*?)<\/li>", System.Text.RegularExpressions.RegexOptions.Singleline);
                foreach (System.Text.RegularExpressions.Match m in matches)
                {
                    var componente = System.Text.RegularExpressions.Regex.Replace(m.Groups[1].Value, "<.*?>", ""); // Quitar tags internos
                    componentes.Add(System.Net.WebUtility.HtmlDecode(componente.Trim()));
                }
            }

            return new MetodologiaProgramaResponseDTO
            {
                IdPGeneral = data.IdPGeneral,
                EsProgramaOCurso = data.EsProgramaOCurso,
                Metodologia = new MetodologiaDTO
                {
                    Titulo = data.Titulo,
                    Descripcion = descripcion,
                    Componentes = componentes
                },
                Error = null
            };
        }
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get pautas complementarias programa
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>PautasComplementariasProgramaResponseDTO</returns> 
        public async Task<PautasComplementariasProgramaResponseDTO> GetPautasComplementariasProgramaAsync(int idPGeneral)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.GetPautasComplementariasRawAsync(idPGeneral);

            if (data == null)
            {
                return new PautasComplementariasProgramaResponseDTO
                {
                    IdPGeneral = idPGeneral,
                    EsProgramaOCurso = "N/A",
                    PautasComplementarias = new List<string>(),
                    Error = "No se encontraron pautas complementarias"
                };
            }

            var pautas = new List<string>();
            if (!string.IsNullOrEmpty(data.ContenidoMetodologia))
            {
                // Extraer el texto plano del HTML
                var match = System.Text.RegularExpressions.Regex.Match(data.ContenidoMetodologia, @"<p>(.*?)<\/p>", System.Text.RegularExpressions.RegexOptions.Singleline);
                if (match.Success)
                    pautas.Add(System.Net.WebUtility.HtmlDecode(match.Groups[1].Value.Trim()));
                else
                    pautas.Add(System.Net.WebUtility.HtmlDecode(Regex.Replace(data.ContenidoMetodologia, "<.*?>", "").Trim()));
            }

            return new PautasComplementariasProgramaResponseDTO
            {
                IdPGeneral = data.IdPGeneral,
                EsProgramaOCurso = data.EsProgramaOCurso,
                PautasComplementarias = pautas,
                Error = null
            };
        }
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get perfil profesional cliente
        /// </summary>
        /// <param name="idAlumno">Id de Alumno</param>
        /// <returns>PerfilProfesionalClienteResponseDTO</returns> 
        public async Task<PerfilProfesionalClienteResponseDTO> GetPerfilProfesionalClienteAsync(int idAlumno)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.ObtenerPerfilProfesionalClienteAsync(idAlumno);

            if (data == null)
            {
                return new PerfilProfesionalClienteResponseDTO
                {
                    PerfilProfesionalCliente = null,
                    Error = "No se encontró información de perfil profesional"
                };
            }

            return new PerfilProfesionalClienteResponseDTO
            {
                PerfilProfesionalCliente = data,
                Error = null
            };
        }
        /// Autor: Lolo Zaa
        /// Fecha: 02/10/2025
        /// Version: 1.0
        /// <summary>
        /// Get detalle programa o curso
        /// </summary>
        /// <param name="idPGeneral">Id de Programa General</param>
        /// <returns>object</returns> 
        public async Task<object> GetDetalleProgramaOCursoAsync(int idPGeneral)
        {
            var data = await _unitOfWork.DocumentoAgendaRepository.GetSeccionesProgramaRawAsync(idPGeneral);

            if (data == null || !data.Any())
                return new { Error = "No se encontró información" };

            var tipo = data.First().EsProgramaOCurso?.Trim().ToLower();

            // Utilidades Regex
            string ObtenerTextoPlano(string html)
            {
                if (string.IsNullOrEmpty(html)) return "";
                return Regex.Replace(System.Net.WebUtility.HtmlDecode(html), "<.*?>", "").Trim();
            }

            List<string> ExtraerListaHtml(string html)
            {
                if (string.IsNullOrEmpty(html)) return new List<string>();
                var matches = Regex.Matches(html, @"<li[^>]*>(.*?)<\/li>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                return matches.Cast<Match>()
                    .Select(m => System.Net.WebUtility.HtmlDecode(Regex.Replace(m.Groups[1].Value, "<.*?>", "").Trim()))
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();
            }

            // Extraer secciones por nombre
            string GetContenido(string nombre) =>
                data.FirstOrDefault(x => x.Titulo.Equals(nombre, StringComparison.OrdinalIgnoreCase))?.Contenido ?? "";

            // Estructura Curricular: puede haber varios capítulos/cursos
            var estructuraCurricularHtml = data.Where(x => x.Titulo.Contains("Estructura Curricular", StringComparison.OrdinalIgnoreCase)).ToList();
            var estructuraCurricular = new List<EstructuraCurricularDTO>();
            int ordenCap = 1;
            foreach (var cap in estructuraCurricularHtml)
            {
                // Buscar todos los <strong> como capítulos
                var strongMatches = Regex.Matches(cap.Contenido ?? "", @"<strong[^>]*>(.*?)<\/strong>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                var ulMatches = Regex.Matches(cap.Contenido ?? "", @"<ul[^>]*>(.*?)<\/ul>", RegexOptions.Singleline | RegexOptions.IgnoreCase);

                // Si hay strongs, cada uno es un capítulo
                if (strongMatches.Count > 0)
                {
                    for (int i = 0; i < strongMatches.Count; i++)
                    {
                        var capitulo = ObtenerTextoPlano(strongMatches[i].Groups[1].Value);
                        // Buscar el <ul> más cercano después del <strong>
                        string sesionesHtml = "";
                        if (ulMatches.Count > i)
                            sesionesHtml = ulMatches[i].Groups[1].Value;
                        else if (ulMatches.Count > 0)
                            sesionesHtml = ulMatches[ulMatches.Count - 1].Groups[1].Value;

                        var sesiones = ExtraerListaHtml("<ul>" + sesionesHtml + "</ul>");
                        estructuraCurricular.Add(new EstructuraCurricularDTO
                        {
                            Orden = ordenCap++,
                            Capitulo = capitulo,
                            Sesiones = sesiones
                        });
                    }
                }
                else if (ulMatches.Count > 0)
                {
                    // Si no hay strong, pero sí <ul>, lo tratamos como un solo capítulo
                    var sesiones = ExtraerListaHtml("<ul>" + ulMatches[0].Groups[1].Value + "</ul>");
                    estructuraCurricular.Add(new EstructuraCurricularDTO
                    {
                        Orden = ordenCap++,
                        Capitulo = "Estructura Curricular",
                        Sesiones = sesiones
                    });
                }
            }

            // Objetivos y Público Objetivo
            var objetivos = ExtraerListaHtml(GetContenido("Objetivos"));
            var publicoObjetivo = ExtraerListaHtml(GetContenido("Público Objetivo"));

            // Material y Bibliografía
            var materialCurso = ExtraerListaHtml(GetContenido("Material Curso"));
            var bibliografia = ExtraerListaHtml(GetContenido("Bibliografia"));

            // Presentación y Duración
            var presentacion = ObtenerTextoPlano(GetContenido("Presentación"));
            var duracion = ObtenerTextoPlano(GetContenido("Duración y Horarios"));

            if (tipo == "programas" || tipo == "programa")
            {
                // Para programas, cada capítulo es un curso
                var cursos = estructuraCurricular.Select(ec => new CursoDetalleDTO
                {
                    Orden = ec.Orden,
                    NombreCurso = ec.Capitulo,
                    Duracion = duracion,
                    Presentacion = presentacion,
                    Objetivos = objetivos,
                    PublicoObjetivo = publicoObjetivo,
                    EstructuraCurricular = new List<EstructuraCurricularDTO> { ec },
                    MaterialCurso = materialCurso,
                    Bibliografia = bibliografia
                }).ToList();

                return new ProgramaDetalleResponseDTO
                {
                    NombrePrograma = presentacion,
                    Duracion = duracion,
                    Cursos = cursos,
                    Error = null
                };
            }
            else // Curso
            {
                return new CursoDetalleResponseDTO
                {
                    NombreCurso = presentacion,
                    Duracion = duracion,
                    Presentacion = presentacion,
                    Objetivos = objetivos,
                    PublicoObjetivo = publicoObjetivo,
                    EstructuraCurricular = estructuraCurricular,
                    MaterialCurso = materialCurso,
                    Bibliografia = bibliografia,
                    Error = null
                };
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 10/01/2023
        /// Version: 1.0
        /// <summary>
        /// Envia correo 
        /// </summary>
        /// <param name="idPlantilla"> Id de la plantilla </param>   
        /// <param name="idPersonal"> Id del personal </param>   
        /// <param name="emailPersonal"> Direccion email del personal </param>   
        /// <param name="emailAlumno"> Direccion email del alumno </param>   
        /// <param name="idoportunidad"> Id de la oportunidad </param>   
        /// <returns> Bool </returns>
        public bool EnvioCorreoAlumno(int idPlantilla, int idPersonal, string emailPersonal, string emailAlumno, int idoportunidad)
        {
            try
            {
                var plantillaService = new PlantillaService(_unitOfWork);
                var plantillaBaseService = new PlantillaBaseService(_unitOfWork);
                var reemplazoEtiquetaPlantillaService = new ReemplazoEtiquetaPlantillaService(_unitOfWork);
                if (!plantillaService.ExistePorId(idPlantilla))
                {
                    return false;
                }

                var plantilla = _unitOfWork.PlantillaRepository.ObtenerPorId(idPlantilla);
                if (!plantillaBaseService.ExistePorId(plantilla.IdPlantillaBase))
                {
                    return false;
                }

                var plantillaBase = plantillaService.ObtenerPlantillaCorreo(idPlantilla);

                var resultadoReemplazo = reemplazoEtiquetaPlantillaService.ReemplazarEtiquetas(new ReemplazoEtiquetaPlantillaDTO
                {
                    IdOportunidad = idoportunidad,
                    IdPlantilla = idPlantilla,
                });

                //var destinatarios = "jvillena@bsginstitute.com";
                var destinatarios = emailAlumno;

                var Remitente = string.IsNullOrEmpty(emailPersonal) == true ? "matriculas@bsginstitute.com" : emailPersonal;
                //var Remitente ="jsalazart@bsginstitute.com";

                if (plantilla.IdPlantillaBase == 2) //ValorEstatico.IdPlantillaBaseEmail)
                {

                    var emailCalculado = resultadoReemplazo.EmailReemplazado;
                    List<string> correosPersonalizadosCopia = new List<string>();
                    //cuando la plantilla es condiciones y caracteristicas
                    //1227	Condiciones y Características - PERÚ OPERACIONES
                    //1245	Condiciones y Características - COLOMBIA OPERACIONES
                    if (Remitente == "matriculas@bsginstitute.com" && (idPlantilla == 1227 || idPlantilla == 1245))
                    {
                        correosPersonalizadosCopia.Add("grabaciones@bsginstitute.com");
                    }
                    List<string> correosPersonalizadosCopiaOculta = new List<string>
                    {
                        //"jquinones@bsginstitute.com",
                        //"modpru@bsginstitute.com",
                        //"ccrispin@bsginstitute.com",
                        //"wruiz@bsginstitute.com"
                    };

                    List<string> correosPersonalizados = new List<string>
                    {
                    };
                    correosPersonalizados.Add(destinatarios);

                    TMKMailDataDTO mailDataPersonalizado = new TMKMailDataDTO
                    {
                        Sender = Remitente,
                        //Sender = personal.Email,
                        //Sender = "w.choque.itusaca@isur.edu.pe",
                        Recipient = string.Join(",", correosPersonalizados.Distinct()),
                        Subject = emailCalculado.Asunto,
                        Message = emailCalculado.CuerpoHTML,
                        Cc = string.Join(",", correosPersonalizadosCopia.Distinct()),
                        Bcc = string.Join(",", correosPersonalizadosCopiaOculta.Distinct()),
                        AttachedFiles = emailCalculado.ListaArchivosAdjuntos
                    };
                    var mailServie = new TMK_MailService();

                    mailServie.SetData(mailDataPersonalizado);
                    mailServie.SendMessageTask();

                    //logica guardar envio
                    var gmailCorreo = new GmailCorreo
                    {
                        IdEtiqueta = 1,//sent:1 , inbox:2
                        Asunto = emailCalculado.Asunto,
                        Fecha = DateTime.Now,
                        EmailBody = emailCalculado.CuerpoHTML,
                        Seen = false,
                        Remitente = Remitente,
                        Cc = "",
                        Bcc = "",
                        Destinatarios = string.Join(",", correosPersonalizados.Distinct()),
                        IdPersonal = idPersonal,
                        Estado = true,
                        FechaCreacion = DateTime.Now,
                        FechaModificacion = DateTime.Now,
                        UsuarioCreacion = "SYSTEM",
                        UsuarioModificacion = "SYSTEM",
                        //IdClasificacionPersona = oportunidad.IdClasificacionPersona
                    };

                    _unitOfWork.GmailCorreoRepository.Add(gmailCorreo);
                    _unitOfWork.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// Autor: Jeremy Pacheco.
        /// Fecha: 21/04/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene encuestas de Alumno por Matricula 
        /// </summary>
        /// <param name="idMatricula">Matricula del Alumno</param>
        /// <returns> List<EncuestaAsignadoMatriculaDTO> </returns>
        public List<EncuestaAsignadoMatriculaDTO> ObtenerEncuestaAlumnoMatriculaCurso(int idMatricula)
        {
            try
            {
                var respuesta = _unitOfWork.DocumentoAgendaRepository.ObtenerEncuestaAlumnoMatriculaCurso(idMatricula);
                
                if (respuesta == null || !respuesta.Any())
                    return respuesta;

                foreach (var item in respuesta)
                {

                    //List<PEspecificoSesionEncuestaPreguntaCategoriaDTO > preguntasEncuestas = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                    var preguntasEncuestas = ObtenerPreguntasEncuestaIdPEspecificoSesion(item.IdPEspecificoSesion);
                    var respuestasEncuestas = ObtenerRespuestasEncuestasIdPEspecificoSesion(item.IdPEspecificoSesion, idMatricula);

                    if (preguntasEncuestas != null)
                    {
                        var preguntasFiltradas = preguntasEncuestas
                        ?.Where(p => p.IdEncuestaSesionPrograma == item.IdEncuestaSesionPrograma)
                        .ToList();

                        item.PreguntasEncuesta = preguntasFiltradas;

                    }
                    else
                    {
                        item.PreguntasEncuesta = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                    }

                    if (respuestasEncuestas != null)
                    {
                        var respuestasFiltradas = respuestasEncuestas
                       ?.Where(p => p.IdEncuestaSesionPrograma == item.IdEncuestaSesionPrograma)
                       .ToList();

                        item.RespuestasEncuesta = respuestasFiltradas;
                    }
                    else
                    {
                        item.RespuestasEncuesta = new List<PEspecificoSesionEncuestaAlumnoDTO>();
                    }


                }

                return respuesta;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Preguntas de Encuesta del IdPEspecificoSesion
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <returns> List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> </returns>
        public List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> ObtenerPreguntasEncuestaIdPEspecificoSesion(int IdPEspecificoSesion)
        {
            try
            {
                List<PEspecificoSesionEncuestaPreguntaCategoriaDTO> PreguntasCategorias = new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
                var preguntas = _unitOfWork.DocumentoAgendaRepository.ObtenerPreguntasSesionEncuestaIdPespecifico(IdPEspecificoSesion);
                var alternativas = _unitOfWork.DocumentoAgendaRepository.ObtenerPEspecificoSesionEncuestaPreguntaAlternativaPorIdSesion(IdPEspecificoSesion);
                if (preguntas != null)
                {
                    PreguntasCategorias = preguntas
                    .GroupBy(x => new
                    {
                        x.IdCategoria,
                        x.NombreCategoria,
                        x.IdEncuestaSesionPrograma
                    })
                    .Select(g => new PEspecificoSesionEncuestaPreguntaCategoriaDTO
                    {
                        IdCategoria = g.Key.IdCategoria,
                        NombreCategoria = g.Key.NombreCategoria,
                        IdEncuestaSesionPrograma = g.Key.IdEncuestaSesionPrograma,
                        Preguntas = g.Select(p => new PEspecificoSesionEncuestaPreguntaDTO
                        {
                            Id = p.Id,
                            IdEncuestaSesionPrograma = p.IdEncuestaSesionPrograma,
                            IdEncuestaOnline = p.IdEncuestaOnline,
                            IdPreguntaEncuestaTipo = p.IdPreguntaEncuestaTipo,
                            Pregunta = p.Pregunta,
                            DescripcionActiva = p.DescripcionActiva,
                            Descripcion = p.Descripcion,
                            NombreTipoPregunta = p.NombreTipoPregunta,
                            IdPEspecificoSesion = p.IdPEspecificoSesion,
                            IdCategoria = p.IdCategoria,
                            NombreCategoria = p.NombreCategoria,
                            PreguntaObligatoria = p.PreguntaObligatoria,
                            PreguntaActiva = p.PreguntaActiva
                        }).ToList()
                    })
                    .ToList();

                    foreach (var Categorias in PreguntasCategorias)
                    {
                        foreach (var pregunta in Categorias.Preguntas)
                        {
                            pregunta.Alternativas = new List<PEspecificoSesionEncuestaPreguntaAlternativaDTO>();
                            if (alternativas != null)
                            {
                                var alternativa =
                                    alternativas.Where(x => x.IdPreguntaEncuesta == pregunta.Id)
                                    .OrderBy(x => x.Orden)
                                    .Select(x => new PEspecificoSesionEncuestaPreguntaAlternativaDTO
                                    {
                                        Id = x.Id,
                                        IdEncuestaOnline = x.IdEncuestaOnline,
                                        IdEncuestaSesionPrograma = x.IdEncuestaSesionPrograma,
                                        IdPEspecificoSesion = x.IdPEspecificoSesion,
                                        IdPreguntaEncuesta = x.IdPreguntaEncuesta,
                                        Orden = x.Orden,
                                        Respuesta = x.Respuesta,
                                        Puntaje = x.Puntaje,
                                    }).ToList();
                                if (alternativa != null)
                                {
                                    pregunta.Alternativas.AddRange(alternativa);
                                }
                            }
                        }
                    }
                }
                return PreguntasCategorias;
            }
            catch (Exception ex)
            {
                return new List<PEspecificoSesionEncuestaPreguntaCategoriaDTO>();
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas de las encuesta por IdPEspecifico y matricula
        /// </summary>
        /// <param name="IdPEspecificoSesion"> Identificador de la sesion</param>
        /// <param name="IdMatriculaCabecera"> Identificador de la matricula</param>
        /// <returns> List<PEspecificoSesionEncuestaAlumnoDTO> </returns>
        public List<PEspecificoSesionEncuestaAlumnoDTO> ObtenerRespuestasEncuestasIdPEspecificoSesion(int IdPEspecificoSesion, int IdMatriculaCabecera)
        {
            try
            {
                var respuesta = _unitOfWork.DocumentoAgendaRepository.ObtenerEncuestaAlumnoPorIdPEspecificoSesion(IdPEspecificoSesion, IdMatriculaCabecera);
                var respuestas = _unitOfWork.DocumentoAgendaRepository.ObtenerPEspecificoSesionEncuestaAlumnoRespuestaPorIdSesion(IdPEspecificoSesion, IdMatriculaCabecera);
                if (respuesta != null)
                {
                    foreach (var res in respuesta)
                    {
                        res.Respuestas = new List<PEspecificoSesionEncuestaAlumnoRespuestaDTO>();
                        if (respuestas != null)
                        {
                            var pregunta =
                                respuestas.Where(x => x.IdPEspecificoSesionEncuestaAlumno == res.Id).Select(x => new PEspecificoSesionEncuestaAlumnoRespuestaDTO
                                {
                                    Id = x.Id,
                                    IdPEspecificoSesion = x.IdPEspecificoSesion,
                                    IdPreguntaEncuesta = x.IdPreguntaEncuesta,
                                    IdPEspecificoSesionEncuestaAlumno = x.IdPEspecificoSesionEncuestaAlumno,
                                    IdPreguntaRespuestaEncuesta = x.IdPreguntaRespuestaEncuesta,
                                    Valor = x.Valor,
                                    Puntos = x.Puntos,
                                    IdMatriculaCabecera = x.IdMatriculaCabecera
                                }).ToList();
                            res.Respuestas.AddRange(pregunta);
                        }
                    }
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Agrega encuesta de alumno 
        /// </summary>
        /// <param name="data">Datos para insertar Encuesta</param>
        /// <returns>retorna true o false </returns>
        public bool AgregarPEspecificoSesionEncuestaAlumno(AgregarPEspecificoSesionEncuestaAlumnoDTO data)
        {
            try
            {
                foreach (var categoria in data.Categorias)
                {
                    foreach (var pregunta in categoria.Preguntas)
                    {
                        if (!pregunta.PreguntaObligatoria && pregunta.ValorRespuesta.Count == 0)
                        {
                            EncuestaAvancePreguntaRespuestaDTO RespuestaVacia = new EncuestaAvancePreguntaRespuestaDTO();
                            RespuestaVacia.IdRespuesta = 0;
                            RespuestaVacia.Respuesta = "";
                            RespuestaVacia.Puntaje = 0;
                            pregunta.ValorRespuesta.Add(RespuestaVacia);
                        }
                    }
                };
                var registro = _unitOfWork.DocumentoAgendaRepository.AgregarPEspecificoSesionEncuestaAlumno(data);
                return registro;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// Tipo Función: GET
        /// Autor: Jeremy Pacheco
        /// Fecha: 21/04/2025
        /// Versión: 1.0
        /// <summary>
        /// Actualiza un comentario a una encuesta
        /// </summary>
        /// <param name="Encuesta">Datos para agregar comentario a un Alumno</param>
        /// <returns>Retorna true o false</returns>
        public bool AgregarComentarioEncuesta(EncuestaComentarioDTO Encuesta)
        {
            try
            {
                var registro = _unitOfWork.DocumentoAgendaRepository.AgregarComentarioEncuesta(Encuesta);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
