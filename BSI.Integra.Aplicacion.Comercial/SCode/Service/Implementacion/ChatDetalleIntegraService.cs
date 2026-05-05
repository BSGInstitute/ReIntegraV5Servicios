using AutoMapper;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Service: ChatDetalleIntegraService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_ChatDetalleIntegra
    /// </summary>
    public class ChatDetalleIntegraService : IChatDetalleIntegraService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ChatDetalleIntegraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TChatDetalleIntegra, ChatDetalleIntegra>(MemberList.None).ReverseMap();
                    cfg.CreateMap<ChatDetalleIntegraDTO, ComboDTO>(MemberList.None);
                }
            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public ChatDetalleIntegra Add(ChatDetalleIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegra>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ChatDetalleIntegra Update(ChatDetalleIntegra entidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ChatDetalleIntegra>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.ChatDetalleIntegraRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegra> Add(List<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegra>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ChatDetalleIntegra> Update(List<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ChatDetalleIntegraRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ChatDetalleIntegra>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ChatDetalleIntegraRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra
        /// </summary>
        /// <returns> List<ChatDetalleIntegraDTO> </returns>
        public IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatDetalleIntegra();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene preguntas ordenadas por versión de formulario
        /// </summary>
        /// <param name="idVersionFormulario">ID de la versión del formulario</param>
        /// <returns>Lista de preguntas ordenadas</returns>
        public IEnumerable<PreguntaEvaluacion2DTO> ObtenerPreguntasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerPreguntasPorVersionFormulario(IdVersionFormularioEvaluacionChatbot);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene respuestas ordenadas por pregunta
        /// </summary>
        /// <param name="idPregunta">ID de la pregunta</param>
        /// <returns>Lista de respuestas ordenadas</returns>
        public IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorPregunta(int idPregunta)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasPorPregunta(idPregunta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene respuestas ordenadas por versión de formulario
        /// </summary>
        /// <param name="idVersionFormulario">ID de la versión del formulario</param>
        /// <returns>Lista de respuestas ordenadas</returns>
        public IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasPorVersionFormulario(IdVersionFormularioEvaluacionChatbot);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene preguntas con sus respuestas incluidas por versión de formulario
        /// </summary>
        /// <param name="idVersionFormulario">ID de la versión del formulario</param>
        /// <returns>Lista de preguntas con respuestas ordenadas</returns>
        public IEnumerable<PreguntaEvaluacion2DTO> ObtenerPreguntasConRespuestas(int IdVersionFormularioEvaluacionChatbot)
        {
            try
            {
                // Primero obtenemos las preguntas
                var preguntas = _unitOfWork.ChatDetalleIntegraRepository.ObtenerPreguntasPorVersionFormulario(IdVersionFormularioEvaluacionChatbot).ToList();

                if (!preguntas.Any())
                    return preguntas;

                // Obtenemos todas las respuestas para esta versión de formulario
                var todasLasRespuestas = _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasPorVersionFormulario(IdVersionFormularioEvaluacionChatbot)
                    .GroupBy(r => r.IdPreguntaEvaluacionChatbot)
                    .ToDictionary(g => g.Key, g => g.OrderBy(r => r.Orden).ToList());

                // Asignamos las respuestas a cada pregunta
                foreach (var pregunta in preguntas)
                {
                    if (todasLasRespuestas.TryGetValue(pregunta.Id, out var respuestas))
                    {
                        pregunta.Respuestas = respuestas;
                    }
                    else
                    {
                        pregunta.Respuestas = new List<RespuestaEvaluacionDTO>();
                    }
                }

                return preguntas.OrderBy(p => p.Orden);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene versiones de formulario activas
        /// </summary>
        /// <returns>Lista de versiones de formulario activas</returns>
        public IEnumerable<VersionFormularioDTO> ObtenerVersionesFormularioActivas()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerVersionesFormularioActivas();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene tipos de entrada activos
        /// </summary>
        /// <returns>Lista de tipos de entrada activos</returns>
        public IEnumerable<TipoEntradaDTO> ObtenerTiposEntradaActivos()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerTiposEntradaActivos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el chat entre chatbot y cliente por IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de mensajes del chat</returns>
        public IEnumerable<ChatbotMensajeDTO> ObtenerChatPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 20/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el chat entre chatbot y cliente por IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de mensajes del chat</returns>
        public IEnumerable<ChatbotMensajeDTO> ObtenerChatPorPortalSegmento(string IdContactoPortalSegmento)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatPorPortalSegmento(IdContactoPortalSegmento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 22/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene hilos de chat con información de alumnos y matrículas
        /// </summary>
        /// <returns>Lista de hilos de chat con datos de alumnos</returns>
        public PagedResponseDTO<ChatbotAlumnoChatPaginadoDTO> ObtenerHilosChatConAlumnos(DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize, string? codigoMatricula)
        {
            try
            {
                var items = _unitOfWork.ChatDetalleIntegraRepository
                    .ObtenerHilosChatConAlumnos(fechaInicio, fechaFin, pageNumber, pageSize, codigoMatricula)
                    .ToList();

                var totalCount = items.FirstOrDefault()?.TotalCount ?? 0;

                return new PagedResponseDTO<ChatbotAlumnoChatPaginadoDTO>
                {
                    Items      = items,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize   = pageSize
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 22/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene hilos de chat sin alumno asociado
        /// </summary>
        /// <returns>Lista de hilos de chat sin alumno</returns>
        public IEnumerable<ChatbotHiloChatPorSegmentoDTO> ObtenerHilosChatPorSegmento()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHilosChatPorSegmento();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las respuestas del cliente por formulario aplicado
        /// </summary>
        /// <param name="idFormularioAplicadoChatbot">ID del formulario aplicado</param>
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdChatbotPortalHiloChat)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasUsuarioPorFormularioAplicado(IdChatbotPortalHiloChat);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en servicio al obtener respuestas del cliente: {ex.Message}", ex);
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 20/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Inserta las respuestas del cliente ingresadas en el formulario
        /// </summary>
        /// <param name="usuario">usuario</param>
        public InsertarRespuestaEvaluacionCompletaResponseDTO InsertarRespuestaEvaluacionCompleta(InsertarRespuestaEvaluacionCompletaRequestDTO request, string usuario)
        {
            try
            {
                if (request == null)
                    throw new BadRequestException("Entidad Nula");

                if (request.IdMedioComunicacion <= 0)
                    throw new BadRequestException("IdMedioComunicacion debe ser mayor a 0");

                if (request.IdOriginal <= 0)
                    throw new BadRequestException("IdOriginal debe ser mayor a 0");

                if (request.IdVersionFormularioEvaluacionChatbot <= 0)
                    throw new BadRequestException("IdVersionFormulario debe ser mayor a 0");

                var todasLasPreguntas = _unitOfWork.ChatDetalleIntegraRepository.ObtenerPreguntasPorVersionFormulario(
                    request.IdVersionFormularioEvaluacionChatbot
                ).ToList();

                if (!todasLasPreguntas.Any())
                {
                    throw new BadRequestException($"Validación fallida: No se encontraron preguntas para la versión de formulario {request.IdVersionFormularioEvaluacionChatbot}.");
                }

                var idsPreguntasRequeridas = todasLasPreguntas
                    .Where(p => p.EsRequerido)
                    .Select(p => p.Id)
                    .ToHashSet();

                if (idsPreguntasRequeridas.Any())
                {

                    var idsPreguntasTexto = request.RespuestasTexto?
                        .Select(r => r.IdPreguntaEvaluacionChatbot)
                        .Distinct() ?? Enumerable.Empty<int>();

                    var idsOpcionesSeleccionadas = request.RespuestasSeleccionadas?
                        .Select(r => r.IdRespuestaEvaluacionChatbot)
                        .ToList() ?? new List<int>();
                    var idsProblemasIdentificados = request.ProblemasIdentificados?
                        .Select(p => p.IdRespuestaEvaluacionChatbot)
                        .ToList() ?? new List<int>();

                    var todosLosIdsOpciones = idsOpcionesSeleccionadas.Union(idsProblemasIdentificados).Distinct().ToList();
                    IEnumerable<int> idsPreguntasDesdeOpciones = new List<int>();
                    if (todosLosIdsOpciones.Any())
                    {
                        idsPreguntasDesdeOpciones = _unitOfWork.ChatDetalleIntegraRepository
                            .ObtenerIdsPreguntaPorIdsRespuesta(todosLosIdsOpciones);
                    }

                    var idsRespuestasUnicasEnviadas = idsPreguntasTexto.Union(idsPreguntasDesdeOpciones).ToHashSet();

                    var idsRequeridasFaltantes = idsPreguntasRequeridas
                        .Where(idRequerido => !idsRespuestasUnicasEnviadas.Contains(idRequerido))
                        .ToList();

                    if (idsRequeridasFaltantes.Any())
                    {
                        string idsFaltantesStr = string.Join(", ", idsRequeridasFaltantes);
                        throw new BadRequestException(
                            $"Validación fallida: Faltan respuestas para {idsRequeridasFaltantes.Count} pregunta(s) requerida(s). IDs de pregunta faltantes: [{idsFaltantesStr}]."
                        );
                    }
                }

                string respuestasSeleccionadasJson = request.RespuestasSeleccionadas != null && request.RespuestasSeleccionadas.Any()
                    ? JsonConvert.SerializeObject(request.RespuestasSeleccionadas)
                    : null;

                string respuestasTextoJson = request.RespuestasTexto != null && request.RespuestasTexto.Any()
                    ? JsonConvert.SerializeObject(request.RespuestasTexto)
                    : null;

                string problemasIdentificadosJson = request.ProblemasIdentificados != null && request.ProblemasIdentificados.Any()
                    ? JsonConvert.SerializeObject(request.ProblemasIdentificados)
                    : null;

                InsertarRespuestaEvaluacionResultadoDTO resultado;

                if (request.IdMedioComunicacion == 5)
                {
                    // Portal Web: SP original con IdChatbotPortalHiloChat = IdOriginal
                    resultado = _unitOfWork.ChatDetalleIntegraRepository.InsertarRespuestaEvaluacionCompleta(
                        request.IdOriginal,
                        request.IdVersionFormularioEvaluacionChatbot,
                        usuario,
                        request.IdSolicitudProblema,
                        request.IdMedioComunicacion,
                        request.IdOriginal,
                        respuestasSeleccionadasJson,
                        respuestasTextoJson,
                        problemasIdentificadosJson
                    );
                }
                else if (request.IdMedioComunicacion == 1)
                {
                    // WhatsApp ATC: SP con IdMedioComunicacion + IdOriginal
                    resultado = _unitOfWork.ChatDetalleIntegraRepository.InsertarRespuestaEvaluacionCompletaWhatsapp(
                        request.IdMedioComunicacion,
                        request.IdOriginal,
                        request.IdVersionFormularioEvaluacionChatbot,
                        usuario,
                        request.IdSolicitudProblema,
                        respuestasSeleccionadasJson,
                        respuestasTextoJson,
                        problemasIdentificadosJson
                    );
                }
                else
                {
                    throw new BadRequestException($"IdMedioComunicacion {request.IdMedioComunicacion} no soportado. Valores válidos: 1 (WhatsApp ATC), 5 (Portal Web).");
                }

                return new InsertarRespuestaEvaluacionCompletaResponseDTO
                {
                    Success = resultado.Success == 1,
                    Message = resultado.Success == 1 ? "Evaluación guardada exitosamente" : "Error al guardar la evaluación",
                    IdFormularioAplicado = resultado.IdFormularioAplicado,
                    TotalRespuestasInsertadas = resultado.TotalRespuestasInsertadas,
                    TotalProblemasIdentificados = resultado.TotalProblemasIdentificados
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene las respuestas del formulario aplicado para un hilo de WhatsApp ATC.
        /// </summary>
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp(int idChatbotWhatsAppHiloChat)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp(idChatbotWhatsAppHiloChat);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en servicio al obtener respuestas WhatsApp: {ex.Message}", ex);
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta la evaluación completa para un hilo de WhatsApp ATC.
        /// Usa IdMedioComunicacion + IdOriginal en lugar de IdChatbotPortalHiloChat.
        /// </summary>
        public InsertarRespuestaEvaluacionCompletaResponseDTO InsertarRespuestaEvaluacionCompletaWhatsapp(InsertarRespuestaEvaluacionCompletaWhatsappRequestDTO request, string usuario)
        {
            var requestUnificado = new InsertarRespuestaEvaluacionCompletaRequestDTO
            {
                IdMedioComunicacion                  = request.IdMedioComunicacion,
                IdOriginal                           = request.IdOriginal,
                IdVersionFormularioEvaluacionChatbot = request.IdVersionFormularioEvaluacionChatbot,
                UsuarioCreacion                      = request.UsuarioCreacion,
                IdSolicitudProblema                  = request.IdSolicitudProblema,
                RespuestasSeleccionadas              = request.RespuestasSeleccionadas,
                RespuestasTexto                      = request.RespuestasTexto,
                ProblemasIdentificados               = request.ProblemasIdentificados
            };
            return InsertarRespuestaEvaluacionCompleta(requestUnificado, usuario);
        }

        /// Autor: BSI Dev
        /// Fecha: 2026-04-29
        /// Versión: 1.0
        /// <summary>
        /// Obtiene hilos paginados (Portal + WhatsApp) para un alumno desde una fecha de corte.
        /// </summary>
        public PagedResponseDTO<HiloChatPaginadoDTO> ObtenerHilosPaginadosPorAlumno(
            ObtenerHilosPaginadosRequestDTO request)
        {
            try
            {
                var items = _unitOfWork.ChatDetalleIntegraRepository
                    .ObtenerHilosPaginadosPorAlumno(
                        request.IdAlumno,
                        request.FechaInicio.Value,
                        request.FechaFin.Value,
                        request.PageNumber,
                        request.PageSize)
                    .ToList();

                var totalCount = items.FirstOrDefault()?.TotalCount ?? 0;

                return new PagedResponseDTO<HiloChatPaginadoDTO>
                {
                    Items      = items,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize   = request.PageSize
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

		///Autor: BSI Dev
		/// Fecha: 2026-05-02
		/// Versión: 1.0
		/// <summary>
		/// Obtiene las solicitudes vinculadas a un hilo de chat (Portal o WhatsApp).
		/// </summary>
		public IEnumerable<SolicitudPorHiloChatDTO> ObtenerSolicitudesPorHiloChat(
            ObtenerSolicitudesPorHiloChatRequestDTO request)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository
                    .ObtenerSolicitudesPorHiloChat(request.IdHiloChat, request.IdChatbotTipo);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ChatDetalleIntegra para mostrarse en combo.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraComboDTO> </returns>
        public IEnumerable<ChatDetalleIntegraComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene historial de chat para pantalla2
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idAlumno">Id del Alumno</param>
        /// <returns> HistorialChatRecibidosDTO </returns>
        public HistorialChatRecibidosDTO ObtenerHistorialChatRecibidos(int idPersonal, int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHistorialChatRecibidos(idPersonal, idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        public List<ChatDetalleIntegra> DetalleChatPorIdInteraccion(int idInteraccion)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerDetalleChatPorIdInteraccion(idInteraccion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        public CursoOportunidadDTO ObtenerCursoOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerCursoOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        public List<ChatHistorialComercialDTO> DetalleChatPorIdAlumno(int idAlumno, int idPGeneral)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerDetalleChatPorIdAlumno(idAlumno,idPGeneral);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


 
        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatAtc(int idMatriculaCabecera,string userName)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.FinalizarChatAtc(idMatriculaCabecera, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatComercial(int idOportunidad, string userName)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.FinalizarChatComercial(idOportunidad, userName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra.
        /// </summary>
        /// <returns> ChatDetalleIntegra </returns>
        public ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerPorIntegraChatYRemintente(idInteraccionChatIntegra, idRemitente);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra
        /// </summary>
        /// <returns> List<ChatDetalleIntegraDTO> </returns>
        public List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHistorialChatDetalleIntegra(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor:Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id de ultima interaccion de cchat alumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.GetIdUltimaInteraccion(idMatriculaCabecera);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 05/12/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene id de ultima interaccion de cchat alumno
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.GetIdUltimaInteraccionComercial(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Max Mantilla
        /// Fecha: 2024-12-10
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra segun su IdCoordinadorAcademico
        /// </summary>
        /// <returns> List<ChatActivoDetalleIntegraDTO> </returns>
        public List<ChatActivoDetalleIntegraDTO> ObtenerChatsAcademicosHabilitadosCoordinadora(int IdCoordinadorAcademico, bool EsOnline)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatsAcademicosHabilitadosCoordinadora(IdCoordinadorAcademico,EsOnline);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las actividades AONLINE/ONLINE para un alumno y programa especifico.
        /// </summary>
        public ObtenerActividadesAtcResponseDTO ObtenerActividadesAtc(int idPEspecifico, int idAlumno)
        {
            try
            {
                var response = new ObtenerActividadesAtcResponseDTO();

                var idMatriculaCabecera = _unitOfWork.ChatDetalleIntegraRepository.ObtenerIdMatriculaCabecera(idAlumno, idPEspecifico);
                if (idMatriculaCabecera != null)
                {
                    int idMC = idMatriculaCabecera.Value;

                    // AONLINE: Videos
                    response.Videos = _unitOfWork.ChatDetalleIntegraRepository.ObtenerVideosAulaVirtual(idMC);

                    // AONLINE: Encuestas
                    response.Encuestas = _unitOfWork.ChatDetalleIntegraRepository.ObtenerEncuestasRealizadas(idMC);

                    // AONLINE: Tareas
                    response.Tareas = _unitOfWork.ChatDetalleIntegraRepository.ObtenerTareasRealizadas(idMC);

                    // Proyecto de Aplicacion
                    var perfil = _unitOfWork.ChatDetalleIntegraRepository.ObtenerDatoPerfilProyecto(idMC);
                    if (perfil != null && perfil.IdProyecto.HasValue && perfil.IdProyecto.Value > 0)
                    {
                        response.Proyecto = perfil;
                        var evaluacion = _unitOfWork.ChatDetalleIntegraRepository.ObtenerConfigurarEvaluacionTrabajo(perfil.IdProyecto.Value);
                        if (evaluacion != null && evaluacion.Id > 0)
                        {
                            response.ProyectoConfiguracion = evaluacion;
                            if (evaluacion.IdPGeneral.HasValue && evaluacion.IdDocumentoPw.HasValue)
                            {
                                response.ProyectoInstrucciones = _unitOfWork.ChatDetalleIntegraRepository.ObtenerInstruccionesDocumentoSeccion(
                                    evaluacion.IdPGeneral.Value, evaluacion.IdDocumentoPw.Value);
                            }
                        }
                    }
                }

                // ONLINE: Actividades por sesion (cuestionarios, tareas, actividades adicionales)
                var idsSesion = _unitOfWork.ChatDetalleIntegraRepository.ObtenerIdsPEspecificoSesion(idPEspecifico);
                foreach (var idSesion in idsSesion)
                {
                    var actividades = _unitOfWork.ChatDetalleIntegraRepository.ObtenerActividadesRecursoSesionDocente(idSesion);
                    response.ActividadesOnline.AddRange(actividades);
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Amplia la fecha de entrega de un cuestionario o tarea.
        /// </summary>
        public AmpliarFechaEntregaResponseDTO AmpliarFechaEntrega(int idPEspecifico, int idAlumno, int idActividad, string fecha, string tipoActividad)
        {
            try
            {
                var response = new AmpliarFechaEntregaResponseDTO { Error = new Dictionary<string, string>() };

                if (string.IsNullOrEmpty(tipoActividad))
                {
                    response.Mensaje = "Error";
                    response.Error.Add("TipoActividad", "El campo TipoActividad es requerido (CUESTIONARIO o TAREA).");
                    return response;
                }

                if (string.IsNullOrEmpty(fecha))
                {
                    response.Mensaje = "Error";
                    response.Error.Add("Fecha", "El campo Fecha es requerido.");
                    return response;
                }

                bool resultado;
                if (tipoActividad.ToUpper() == "CUESTIONARIO")
                {
                    resultado = _unitOfWork.ChatDetalleIntegraRepository.AmpliarFechaCuestionario(idActividad, fecha);
                }
                else if (tipoActividad.ToUpper() == "TAREA")
                {
                    resultado = _unitOfWork.ChatDetalleIntegraRepository.AmpliarFechaTarea(idActividad, fecha);
                }
                else
                {
                    response.Mensaje = "Error";
                    response.Error.Add("TipoActividad", "TipoActividad debe ser CUESTIONARIO o TAREA.");
                    return response;
                }

                if (resultado)
                {
                    response.Mensaje = "Fecha de entrega ampliada correctamente.";
                }
                else
                {
                    response.Mensaje = "Error al ampliar la fecha de entrega.";
                    response.Error.Add("General", "No se pudo actualizar el registro.");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene sesiones con estado de asistencia para un alumno y programa.
        /// </summary>
        public ObtenerAsistenciaAtcResponseDTO ObtenerAsistenciaAtc(int idPEspecifico, int idAlumno)
        {
            try
            {
                var response = new ObtenerAsistenciaAtcResponseDTO();

                var idMatriculaCabecera = _unitOfWork.ChatDetalleIntegraRepository.ObtenerIdMatriculaCabecera(idAlumno, idPEspecifico);
                if (idMatriculaCabecera == null)
                {
                    return response;
                }

                var sesiones = _unitOfWork.ChatDetalleIntegraRepository.ObtenerAsistenciaPorMatricula(idMatriculaCabecera.Value, idPEspecifico);
                response.Sesiones = sesiones;
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 23/02/2026
        /// Version: 1.0
        /// <summary>
        /// Registra justificacion de inasistencia para una sesion.
        /// </summary>
        public RegistrarAsistenciaAtcResponseDTO RegistrarAsistenciaAtc(int sesionId, int idAlumno)
        {
            try
            {
                var response = new RegistrarAsistenciaAtcResponseDTO { Error = new Dictionary<string, string>() };

                var idPEspecifico = _unitOfWork.ChatDetalleIntegraRepository.ObtenerIdPEspecificoPorSesion(sesionId);
                if (idPEspecifico == null)
                {
                    response.Mensaje = "Error";
                    response.Error.Add("Sesion", "No se encontro una sesion activa con el Id especificado.");
                    return response;
                }

                var idMatriculaCabecera = _unitOfWork.ChatDetalleIntegraRepository.ObtenerIdMatriculaCabecera(idAlumno, idPEspecifico.Value);
                if (idMatriculaCabecera == null)
                {
                    response.Mensaje = "Error";
                    response.Error.Add("MatriculaCabecera", "No se encontro una matricula activa para el alumno y programa especificado.");
                    return response;
                }

                var resultado = _unitOfWork.ChatDetalleIntegraRepository.RegistrarAsistenciaMatricula(idMatriculaCabecera.Value, sesionId);
                if (resultado)
                {
                    response.Mensaje = "Asistencia registrada correctamente.";
                }
                else
                {
                    response.Mensaje = "Error al registrar la asistencia.";
                    response.Error.Add("General", "No se pudo registrar la asistencia.");
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: BSI Institute - Backend Team
        /// Fecha: 2026-04-27
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de mensajes WhatsApp ATC de un alumno.
        /// </summary>
        public IEnumerable<ChatbotMensajeWhatsAppAtcDTO> ObtenerChatWhatsAppAtcPorAlumno(int idAlumno)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerChatWhatsAppAtcPorAlumno(idAlumno);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
