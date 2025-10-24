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
        public IEnumerable<ChatbotHiloChatPorAlumnoDTO> ObtenerHilosChatConAlumnos()
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerHilosChatConAlumnos();
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
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdFormularioAplicadoChatbot)
        {
            try
            {
                return _unitOfWork.ChatDetalleIntegraRepository.ObtenerRespuestasUsuarioPorFormularioAplicado(IdFormularioAplicadoChatbot);
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

                if (request.IdChatbotPortalHiloChat <= 0)
                    throw new BadRequestException("IdChatbotPortalHiloChat debe ser mayor a 0");

                if (request.IdVersionFormularioEvaluacionChatbot <= 0)
                    throw new BadRequestException("IdVersionFormulario debe ser mayor a 0");

                string respuestasSeleccionadasJson = request.RespuestasSeleccionadas != null && request.RespuestasSeleccionadas.Any()
                    ? JsonConvert.SerializeObject(request.RespuestasSeleccionadas)
                    : null;

                string respuestasTextoJson = request.RespuestasTexto != null && request.RespuestasTexto.Any()
                    ? JsonConvert.SerializeObject(request.RespuestasTexto)
                    : null;

                string problemasIdentificadosJson = request.ProblemasIdentificados != null && request.ProblemasIdentificados.Any()
                    ? JsonConvert.SerializeObject(request.ProblemasIdentificados)
                    : null;

                var resultado = _unitOfWork.ChatDetalleIntegraRepository.InsertarRespuestaEvaluacionCompleta(
                    request.IdChatbotPortalHiloChat,
                    request.IdVersionFormularioEvaluacionChatbot,
                    usuario,
                    respuestasSeleccionadasJson,
                    respuestasTextoJson,
                    problemasIdentificadosJson
                );

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
    }
}
