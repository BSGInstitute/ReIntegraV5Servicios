using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ChatDetalleIntegraRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_ChatDetalleIntegra
    /// </summary>
    public class ChatDetalleIntegraRepository : GenericRepository<TChatDetalleIntegra>, IChatDetalleIntegraRepository
    {
        private Mapper _mapper;

        public ChatDetalleIntegraRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TChatDetalleIntegra, ChatDetalleIntegra>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TChatDetalleIntegra MapeoEntidad(ChatDetalleIntegra entidad)
        {
            try
            {
                //crea la entidad padre
                TChatDetalleIntegra modelo = _mapper.Map<TChatDetalleIntegra>(entidad);

                //mapea los hijos
                //if (entidad.ListadoHijoNivel1 != null && entidad.ListadoHijoNivel1.Count > 0)
                //{
                //    var listadoHijoNivel1 = _mapper.Map<List<THijo>>(entidad.ListadoHijoNivel1);
                //    foreach (var hijoNivel1 in listadoHijoNivel1)
                //    {
                //        modelo.THijo.Add(hijoNivel1);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TChatDetalleIntegra Add(ChatDetalleIntegra entidad)
        {
            try
            {
                var ChatDetalleIntegra = MapeoEntidad(entidad);
                base.Insert(ChatDetalleIntegra);
                return ChatDetalleIntegra;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TChatDetalleIntegra Update(ChatDetalleIntegra entidad)
        {
            try
            {
                var ChatDetalleIntegra = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ChatDetalleIntegra.RowVersion = entidadExistente.RowVersion;

                base.Update(ChatDetalleIntegra);
                return ChatDetalleIntegra;
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
                base.Delete(id, usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<TChatDetalleIntegra> Add(IEnumerable<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                List<TChatDetalleIntegra> listado = new List<TChatDetalleIntegra>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                base.Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TChatDetalleIntegra> Update(IEnumerable<ChatDetalleIntegra> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TChatDetalleIntegra> listado = new List<TChatDetalleIntegra>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = base.GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                base.Update(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(IEnumerable<int> listadoIds, string usuario)
        {
            try
            {
                base.Delete(listadoIds, usuario);
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
        /// Obtiene todos los registros de T_ChatDetalleIntegra.
        /// </summary>
        /// <returns> List<ChatDetalleIntegraDTO> </returns>
        public IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra()
        {
            try
            {
                List<ChatDetalleIntegraDTO> rpta = new List<ChatDetalleIntegraDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    IdInteraccionChatIntegra,
	                    NombreRemitente,
	                    IdRemitente,
	                    Mensaje,
	                    Fecha,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    MensajeOfensivo,
	                    IdChatDetalleIntegraArchivo
                    FROM com.T_ChatDetalleIntegra
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatDetalleIntegraDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las preguntas de evaluación activas por versión de formulario, ordenadas por el campo Orden.
        /// </summary>
        /// <param name="idVersionFormularioEvaluacionChatbot">ID de la versión del formulario</param>
        /// <returns>Lista de preguntas ordenadas</returns>
        public IEnumerable<PreguntaEvaluacion2DTO> ObtenerPreguntasPorVersionFormulario(int idVersionFormularioEvaluacionChatbot)
            {
                try
                {
                    var resultado = new List<PreguntaEvaluacion2DTO>();
                    var query = "ia.SP_TPreguntaEvaluacionChatbot_ObtenerPorVersionFormulario";
                    var response = _dapperRepository.QuerySPDapper(query, new { idVersionFormularioEvaluacionChatbot });

                    if (!string.IsNullOrEmpty(response))
                    {
                        resultado = JsonConvert.DeserializeObject<List<PreguntaEvaluacion2DTO>>(response);
                    }
                    return resultado;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las respuestas activas por versión de formulario, ordenadas por pregunta y orden.
        /// </summary>
        /// <param name="idVersionFormulario">ID de la versión del formulario</param>
        /// <returns>Lista de respuestas ordenadas</returns>
        public IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot)
            {
                try
                {
                    var resultado = new List<RespuestaEvaluacionDTO>();
                    var query = "ia.SP_TRespuestaEvaluacionChatbot_ObtenerPorVersionFormulario";
                    var response = _dapperRepository.QuerySPDapper(query, new { IdVersionFormularioEvaluacionChatbot });

                    if (!string.IsNullOrEmpty(response))
                    {
                        resultado = JsonConvert.DeserializeObject<List<RespuestaEvaluacionDTO>>(response);
                    }
                    return resultado;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }


        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat entre chatbot y cliente por IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de mensajes del chat</returns>
        public IEnumerable<ChatbotMensajeDTO> ObtenerChatPorAlumno(int idAlumno)
            {
                try
                {
                    var resultado = new List<ChatbotMensajeDTO>();
                    var query = "ia.SP_TChatbotPortalHiloChat_ObtenerPorAlumno";
                    var response = _dapperRepository.QuerySPDapper(query, new { idAlumno });

                    if (!string.IsNullOrEmpty(response))
                    {
                        resultado = JsonConvert.DeserializeObject<List<ChatbotMensajeDTO>>(response);
                    }
                    return resultado;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el chat entre chatbot y cliente por IdAlumno
        /// </summary>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Lista de mensajes del chat</returns>
        public IEnumerable<ChatbotMensajeDTO> ObtenerChatPorPortalSegmento(string IdContactoPortalSegmento)
        {
            try
            {
                var resultado = new List<ChatbotMensajeDTO>();
                var query = "ia.SP_TChatbotPortalHiloChat_ObtenerPorContactoPortalSegmento";
                var response = _dapperRepository.QuerySPDapper(query, new { IdContactoPortalSegmento });

                if (!string.IsNullOrEmpty(response))
                {
                    resultado = JsonConvert.DeserializeObject<List<ChatbotMensajeDTO>>(response);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 18/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las respuestas activas por pregunta, ordenadas por el campo Orden.
        /// </summary>
        /// <param name="idPregunta">ID de la pregunta</param>
        /// <returns>Lista de respuestas ordenadas</returns>
        public IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorPregunta(int idPregunta)
        {
            try
            {
                List<RespuestaEvaluacionDTO> rpta = new List<RespuestaEvaluacionDTO>();
                var query = @"
                SELECT 
                    Id,
                    Respuesta,
                    Orden,
                    Estado,
                    IdPreguntaEvaluacionChatbot,
                    IdTipoEntradaEvaluacionChatbot
                FROM ia.T_RespuestaEvaluacionChatbot
                WHERE IdPreguntaEvaluacionChatbot = @IdPregunta 
                AND Estado = 1
                ORDER BY Orden";

                var parametros = new { IdPregunta = idPregunta };
                var resultado = _dapperRepository.QueryDapper(query, parametros);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RespuestaEvaluacionDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene todas las versiones de formulario activas.
        /// </summary>
        /// <returns>Lista de versiones de formulario activas</returns>
        public IEnumerable<VersionFormularioDTO> ObtenerVersionesFormularioActivas()
        {
            try
            {
                List<VersionFormularioDTO> rpta = new List<VersionFormularioDTO>();
                var query = @"
                SELECT
                    Id,
                    Nombre,
                    Descripcion,
                    Origen,
                    Version,
                    Estado,
                    IdMedioComunicacion
                FROM ia.T_VersionFormularioEvaluacionChatbot
                WHERE Estado = 1
                ORDER BY Version DESC";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<VersionFormularioDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene todos los tipos de entrada activos.
        /// </summary>
        /// <returns>Lista de tipos de entrada activos</returns>
        public IEnumerable<TipoEntradaDTO> ObtenerTiposEntradaActivos()
        {
            try
            {
                List<TipoEntradaDTO> rpta = new List<TipoEntradaDTO>();
                var query = @"
                SELECT 
                    Id,
                    Nombre,
                    Descripcion,
                    Estado
                FROM ia.T_TipoEntradaEvaluacionChatbot
                WHERE Estado = 1
                ORDER BY Nombre";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TipoEntradaDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene hilos de chat con información completa de alumnos y matrículas
        /// </summary>
        /// <returns>Lista de hilos de chat con datos de alumnos</returns>
        public IEnumerable<ChatbotAlumnoChatPaginadoDTO> ObtenerHilosChatConAlumnos(DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize, string? codigoMatricula)
        {
            try
            {
                List<ChatbotAlumnoChatPaginadoDTO> rpta = new List<ChatbotAlumnoChatPaginadoDTO>();
                var query = @"ia.SP_ChatbotPortalHiloChat_ObtenerHilosConAlumno";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    FechaInicio      = fechaInicio,
                    FechaFin         = fechaFin,
                    PageNumber       = pageNumber,
                    PageSize         = pageSize,
                    CodigoMatricula  = codigoMatricula
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatbotAlumnoChatPaginadoDTO>>(resultado);
                }
                return rpta;
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
                List<ChatbotHiloChatPorSegmentoDTO> rpta = new List<ChatbotHiloChatPorSegmentoDTO>();
                var query = @"ia.SP_ChatbotPortalHiloChat_ObtenerHilosSinAlumno";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatbotHiloChatPorSegmentoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ChatbotHiloChatPorSegmentoPaginadoDTO> ObtenerHilosChatPorSegmentoPaginado(DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize)
        {
            try
            {
                List<ChatbotHiloChatPorSegmentoPaginadoDTO> rpta = new List<ChatbotHiloChatPorSegmentoPaginadoDTO>();
                var query = @"ia.SP_ChatbotPortalHiloChat_ObtenerSegmentosSinAlumnoPaginado";

                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    FechaInicio = fechaInicio,
                    FechaFin    = fechaFin,
                    PageNumber  = pageNumber,
                    PageSize    = pageSize
                });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatbotHiloChatPorSegmentoPaginadoDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 23/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las respuestas del cliente por formulario aplicado
        /// </summary>
        /// <param name="IdFormularioAplicadoChatbot">ID del formulario aplicado</param>
        /// <returns>Lista de respuestas del cliente con detalles de pregunta y respuesta predefinida</returns>
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdChatbotPortalHiloChat)
        {
            try
            {
                List<RespuestaClienteDTO> rpta = new List<RespuestaClienteDTO>();
                var query = $@"ia.SP_TRespuestaClienteChatbot_ObtenerRespuestasUsuario";
                var resultado = _dapperRepository.QuerySPDapper(query, new { IdChatbotPortalHiloChat = IdChatbotPortalHiloChat });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RespuestaClienteDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene respuestas del formulario aplicado para un hilo de WhatsApp ATC,
        /// buscando por IdMedioComunicacion + IdOriginal en T_FormularioAplicadoChatbot.
        /// </summary>
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp(int idChatbotWhatsAppHiloChat)
        {
            try
            {
                List<RespuestaClienteDTO> rpta = new List<RespuestaClienteDTO>();
                var resultado = _dapperRepository.QuerySPDapper(
                    "ia.SP_TRespuestaClienteChatbot_ObtenerRespuestasUsuarioWhatsapp",
                    new { IdChatbotWhatsAppHiloChat = idChatbotWhatsAppHiloChat });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<RespuestaClienteDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 27/04/2026
        /// Versión: 1.0
        /// <summary>
        /// Inserta la evaluación completa para un hilo de WhatsApp ATC usando
        /// IdMedioComunicacion + IdOriginal en lugar de IdChatbotPortalHiloChat.
        /// </summary>
        public InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompletaWhatsapp(
            int idMedioComunicacion,
            int idOriginal,
            int idVersionFormularioEvaluacionChatbot,
            string usuarioCreacion,
            int? idSolicitudProblema,
            string respuestasSeleccionadasJson = null,
            string respuestasTextoJson = null,
            string problemasIdentificadosJson = null)
        {
            try
            {
                var _resultado = new InsertarRespuestaEvaluacionResultadoDTO();
                var parametros = new
                {
                    IdMedioComunicacion = idMedioComunicacion,
                    IdOriginal = idOriginal,
                    IdVersionFormularioEvaluacionChatbot = idVersionFormularioEvaluacionChatbot,
                    UsuarioCreacion = usuarioCreacion,
                    IdSolicitudProblema = idSolicitudProblema,
                    RespuestasSeleccionadas = respuestasSeleccionadasJson,
                    RespuestasTexto = respuestasTextoJson,
                    ProblemasIdentificados = problemasIdentificadosJson
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(
                    "ia.SP_TFormularioAplicadoChatbot_InsertarEvaluacionCompletaWhatsapp",
                    parametros);

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<InsertarRespuestaEvaluacionResultadoDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
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
                List<ChatDetalleIntegraComboDTO> rpta = new List<ChatDetalleIntegraComboDTO>();
                var query = @"SELECT Id,NombreRemitente,Mensaje,Fecha FROM com.T_ChatDetalleIntegra WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatDetalleIntegraComboDTO>>(resultado);
                }
                return rpta;
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
                HistorialChatRecibidosDTO rpta = new HistorialChatRecibidosDTO();
                var query = @"
                    SELECT TOP 1
	                    DET.NombreRemitente,DET.Mensaje,DET.Fecha,DET.IdInteraccionChatIntegra AS IdInteraccionChat,DET.IdAsesor,DET.Ubicacion,
	                    DET.IdChatSession AS ChatSession
                    FROM com.V_HistorialChatPortal AS DET
                    WHERE DET.IdAsesor = @idPersonal AND IdAlumno = @idAlumno
                    ORDER BY DET.IdInteraccionChatIntegra,fecha DESC";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal, idAlumno });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<HistorialChatRecibidosDTO>(resultado);
                }
                return rpta;
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
        /// Inserta una evaluación completa de un hilo de chat en la base de datos.
        /// </summary>
        /// <param name="idChatbotPortalHiloChat">ID del hilo de chat que está siendo evaluado.</param>
        /// <param name="idVersionFormularioEvaluacionChatbot">ID de la versión del formulario que se aplicó.</param>
        /// <param name="usuarioCreacion">Nombre del usuario que registra la evaluación.</param>
        /// <param name="respuestasSeleccionadasJson">Cadena JSON con las respuestas de selección.</param>
        /// <param name="respuestasTextoJson">Cadena JSON con las respuestas de texto.</param>
        /// <param name="problemasIdentificadosJson">Cadena JSON con los problemas identificados en la evaluación.</param>
        /// <returns>Un objeto con el resultado de la inserción.</returns>
        public InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompleta(
            int idChatbotPortalHiloChat,
            int idVersionFormularioEvaluacionChatbot,
            string usuarioCreacion,
            int? idSolicitudProblema,
            int idMedioComunicacion,
            int idOriginal,
            string respuestasSeleccionadasJson = null,
            string respuestasTextoJson = null,
            string problemasIdentificadosJson = null)
        {
            try
            {
                var _resultado = new InsertarRespuestaEvaluacionResultadoDTO();
                var query = "ia.SP_TFormularioAplicadoChatbot_InsertarEvaluacionCompleta";

                var parametros = new
                {
                    IdChatbotPortalHiloChat = idChatbotPortalHiloChat,
                    IdVersionFormularioEvaluacionChatbot = idVersionFormularioEvaluacionChatbot,
                    UsuarioCreacion = usuarioCreacion,
                    IdSolicitudProblema = idSolicitudProblema,
                    IdMedioComunicacion = idMedioComunicacion,
                    IdOriginal = idOriginal,
                    RespuestasSeleccionadas = respuestasSeleccionadasJson,
                    RespuestasTexto = respuestasTextoJson,
                    ProblemasIdentificados = problemasIdentificadosJson
                };

                var resultado = _dapperRepository.QuerySPFirstOrDefault(query, parametros);

                if (!string.IsNullOrEmpty(resultado))
                {
                    _resultado = JsonConvert.DeserializeObject<InsertarRespuestaEvaluacionResultadoDTO>(resultado);
                }
                return _resultado;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 2026-04-29
        /// Versión: 1.0
        /// <summary>
        /// Obtiene hilos de chat paginados (Portal + WhatsApp) para un alumno desde una fecha de corte.
        /// </summary>
        public IEnumerable<HiloChatPaginadoDTO> ObtenerHilosPaginadosPorAlumno(
            int idAlumno, DateTime fechaInicio, DateTime fechaFin, int pageNumber, int pageSize)
        {
            try
            {
                var query = "ia.SP_ObtenerHilosPaginadosPorAlumno";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    IdAlumno   = idAlumno,
                    FechaInicio = fechaInicio,
                    FechaFin    = fechaFin,
                    PageNumber = pageNumber,
                    PageSize   = pageSize
                });

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return Enumerable.Empty<HiloChatPaginadoDTO>();

                return JsonConvert.DeserializeObject<List<HiloChatPaginadoDTO>>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<HiloChatPaginadoDTO> ObtenerHilosPaginadosPorSegmento(
            string idContactoPortalSegmento, DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize)
        {
            try
            {
                var query = "ia.SP_ObtenerHilosPaginadosPorSegmento";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    IdContactoPortalSegmento = idContactoPortalSegmento,
                    FechaInicio              = fechaInicio,
                    FechaFin                 = fechaFin,
                    PageNumber               = pageNumber,
                    PageSize                 = pageSize
                });

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return Enumerable.Empty<HiloChatPaginadoDTO>();

                return JsonConvert.DeserializeObject<List<HiloChatPaginadoDTO>>(resultado);
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
        /// Obtiene los Ids de Pregunta únicos a partir de una lista de Ids de Respuestas (opciones).
        /// </summary>
        /// <param name="idsRespuesta">Lista de Ids de T_RespuestaEvaluacionChatbot</param>
        /// <returns>Lista de Ids de T_PreguntaEvaluacionChatbot</returns>
        public IEnumerable<int> ObtenerIdsPreguntaPorIdsRespuesta(IEnumerable<int> idsRespuesta)
        {
            try
            {

                var query = @"
                            SELECT IdPreguntaEvaluacionChatbot 
                            FROM ia.T_RespuestaEvaluacionChatbot
                            WHERE Id IN @IdsRespuesta AND Estado = 1";
                var resultadoJson = _dapperRepository.QueryDapper(query, new { IdsRespuesta = idsRespuesta });

                if (!string.IsNullOrEmpty(resultadoJson) && !resultadoJson.Contains("[]"))
                {
                    var listaDeObjetos = JsonConvert.DeserializeObject<List<dynamic>>(resultadoJson);
                    return listaDeObjetos.Select(o => (int)o.IdPreguntaEvaluacionChatbot);
                }
                return Enumerable.Empty<int>();
            }
            catch (Exception ex)
            {
                throw new Exception("Error en Repositorio:ObtenerIdsPreguntaPorIdsRespuesta", ex);
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
        public List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccion(int idInteraccion)
        {
            var chatDetallesIntegra = new List<ChatDetalleIntegra>();
            var query = string.Empty;
            query = @"SELECT NombreRemitente, Mensaje, Fecha, IdRemitente  FROM ia.V_ChatPortal_ObtenerDetallePorInteraccionIntegra WHERE IdInteraccionChatIntegra = @idInteraccion ORDER BY IdInteraccionChatIntegra , Fecha ASC";
            var chatDetallesIntegraDB = _dapperRepository.QueryDapper(query, new { idInteraccion });
            chatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegra>>(chatDetallesIntegraDB);
            return chatDetallesIntegra;
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
            var cursoOportunidad = new CursoOportunidadDTO();
            var query = string.Empty;
            query = @"SELECT
	                        IdOportunidad
	                        ,IdCentroCosto
	                        ,IdPespecifico
	                        ,IdProgramaGeneral
                        FROM
	                        ia.V_ChatbotPortal_ObtenerDatosOportunidad
                        WHERE
	                        IdOportunidad=@idOportunidad;";
            var resp = _dapperRepository.FirstOrDefault(query, new { idOportunidad });
            cursoOportunidad = JsonConvert.DeserializeObject<CursoOportunidadDTO>(resp);
            return cursoOportunidad;
        }

        /// Autor: Gilmer Quispe.
        /// Fecha: 01/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un listado de ChatDetalleIntegra filtrado por idInteraccion
        /// </summary>
        /// <param name="idInteraccion"></param>
        /// <returns> Lista de Entidad List<ChatDetalleIntegra> </returns>
        public List<ChatHistorialComercialDTO> ObtenerDetalleChatPorIdAlumno(int idAlumno,int idPGeneral)
        {
            var chatDetallesIntegra = new List<ChatHistorialComercialDTO>();
            var query = string.Empty;
            query = @"SELECT IdInteraccionChatIntegra
		                        ,NombreRemitente
		                        ,Mensaje
		                        ,Fecha
		                        ,IdRemitente
		                        ,MensajeOfensivo
		                        ,Estado
		                        ,IdContactoPortalSegmento
		                        ,IdPGeneral
		                        ,IdFaseOportunidadPortalWeb
		                        ,IdAlumno 
                       FROM ia.V_ChatbotPortal_ObtenerHistorialChatComercial WHERE IdAlumno=@idAlumno AND IdPGeneral=@idPGeneral ORDER BY IdInteraccionChatIntegra ASC";
            var chatDetallesIntegraDB = _dapperRepository.QueryDapper(query, new { idAlumno, idPGeneral });
            chatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatHistorialComercialDTO>>(chatDetallesIntegraDB);
            return chatDetallesIntegra;
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
            List<ChatDetalleIntegra> detalleChatSoporte = new List<ChatDetalleIntegra>();
            var detalleChatSoporteDB = _dapperRepository.QuerySPDapper("pla.SP_ObtenerDetalleChatSoporteIdAlumnoArchivoAdjunto", new { IdAlumno = idAlumno });
            if (!string.IsNullOrEmpty(detalleChatSoporteDB) && !detalleChatSoporteDB.Contains("[]"))
            {
                detalleChatSoporte = JsonConvert.DeserializeObject<List<ChatDetalleIntegra>>(detalleChatSoporteDB);
            }
            return detalleChatSoporte;
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
                ChatDetalleIntegra rpta = new ChatDetalleIntegra();
                var query = @"
                            SELECT
	                            Id, IdInteraccionChatIntegra, NombreRemitente, IdRemitente, Mensaje, Fecha, Estado, UsuarioCreacion, UsuarioModificacion,
                                FechaModificacion, RowVersion, IdMigracion, MensajeOfensivo, IdChatDetalleIntegraArchivo, FechaCreacion
                            FROM 
                                com.T_ChatDetalleIntegra
                            WHERE 
                                Estado = 1 AND IdInteraccionChatIntegra = @IdInteraccionChatIntegra AND IdRemitente = 'visitante'";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdInteraccionChatIntegra = idInteraccionChatIntegra, IdRemitente = idRemitente });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<ChatDetalleIntegra>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph LLanque
        /// Fecha: 14/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra.
        /// </summary>
        /// <returns> ChatDetalleIntegra </returns>
        public List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera)
        {
            try
            {
                List<HistorialChatDetalleIntegraDTO> rpta = new List<HistorialChatDetalleIntegraDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[ia].[SP_ChatbotPortal_HistorialChatSoporte]", new { idMatriculaCabecera }) ;
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<HistorialChatDetalleIntegraDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<WhatsAppHistorialMensajesOperacionesDTO> ObtenerHistorialChatDetalleIntegraFlotante(int idMatriculaCabecera)
        {
            try
            {
                List<WhatsAppHistorialMensajesOperacionesDTO> rpta = new List<WhatsAppHistorialMensajesOperacionesDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[ia].[SP_ChatbotPortal_HistorialChatSoporteFlotante]", new { idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<WhatsAppHistorialMensajesOperacionesDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Joseph Llanque
        ///// Fecha: 05/12/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        ///// </summary>
        ///// <param name="idAlumno"> Id de Alumno </param>
        ///// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatAtc(int idMatriculaCabecera, string username)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[ia].[SP_ChatbotPortal_FinalizarChatAtc_Actualizar]", new { IdMatriculaCabecera = idMatriculaCabecera, UsuarioModificacion = username });
                return true;
            }
            catch
            {
                return false;
            }
        }

   

        /// Autor: Gilmer Quispe.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un id de interaccion
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera)
        {
            List<DatosSesionChatDTO> respuesta = new List<DatosSesionChatDTO>();
            var query = @"SELECT  Id
		                         ,IdChatIntegraHistorialAsesor
		                         ,IdAlumno
		                         ,IdEstadoChat
		                         ,IdChatSession
		                         ,Estado
		                         ,UsuarioCreacion
		                         ,FechaCreacion
		                         ,UsuarioModificacion
		                         ,FechaModificacion
		                         ,FechaInicio
		                         ,FechaFin
		                         ,Leido
		                         ,EsAcademico
		                         ,EsSoporteTecnico
		                         ,IdMatriculaCabecera
                            FROM ia.V_ChatbotPortal_ObtenerDatosSesionChat
                            WHERE IdMatriculaCabecera = @idMatriculaCabecera
                            order by fechaInicio desc";
            var resultado = _dapperRepository.QueryDapper(query, new { idMatriculaCabecera = idMatriculaCabecera });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                respuesta = JsonConvert.DeserializeObject<List<DatosSesionChatDTO>>(resultado)!;
            }
            return respuesta;
        }

        /// Autor:joseph llanque.
        /// Fecha: 05/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene un id de interaccion
        /// </summary>
        /// <param name="idAlumno"> Id de Alumno </param>
        /// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno)
        {
            List<DatosSesionChatComercialDTO> respuesta = new List<DatosSesionChatComercialDTO>();
            var query = @"SELECT  Id
		                         ,IdChatIntegraHistorialAsesor
		                         ,IdAlumno
		                         ,IdEstadoChat
		                         ,IdChatSession
		                         ,Estado
		                         ,UsuarioCreacion
		                         ,FechaCreacion
		                         ,UsuarioModificacion
		                         ,FechaModificacion
		                         ,FechaInicio
		                         ,FechaFin
		                         ,Leido
		                         ,EsAcademico
		                         ,EsSoporteTecnico
		                         ,IdMatriculaCabecera
                            FROM ia.V_ChatbotPortal_ObtenerDatosSesionChat
                            WHERE IdAlumno = @idAlumno
                            order by fechaInicio desc";
            var resultado = _dapperRepository.QueryDapper(query, new { idAlumno = idAlumno });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                respuesta = JsonConvert.DeserializeObject<List<DatosSesionChatComercialDTO>>(resultado)!;
            }
            return respuesta;
        }



        /// Autor: Joseph Llanque.
        ///// Fecha: 05/12/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        ///// </summary>
        ///// <param name="idAlumno"> Id de Alumno </param>
        ///// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatComercial(int idOportunidad, string username)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[ia].[SP_ChatbotPortal_FinalizarChatVentas_Actualizar]", new { IdOportunidad = idOportunidad, UsuarioModificacion = username });
                return true;
            }
            catch
            {
                return false;
            }
        }


        
        /// Autor: Max Mantilla
        /// Fecha: 2024-12-10
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ChatDetalleIntegra según el coordinador académico
        /// </summary>
        /// <returns> List<ChatActivoDetalleIntegraDTO> </returns>
        public List<ChatActivoDetalleIntegraDTO> ObtenerChatsAcademicosHabilitadosCoordinadora(int IdCoordinadorAcademico, bool EsOnline)
        {
            try
            {
                List<ChatActivoDetalleIntegraDTO> rpta = new List<ChatActivoDetalleIntegraDTO>();
                var resultado = _dapperRepository.QuerySPDapper("[ia].[SP_ChatbotPortal_ObtenerChatsAtc]", new { IdPersonal_CoordinadorChat=IdCoordinadorAcademico, EsOnline=EsOnline });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]") && resultado != "null" && resultado != null)
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatActivoDetalleIntegraDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene el Id de MatriculaCabecera a partir de IdAlumno e IdPEspecifico.
        /// </summary>
        public int? ObtenerIdMatriculaCabecera(int idAlumno, int idPEspecifico)
        {
            try
            {
                var query = @"SELECT TOP 1 Id FROM fin.T_MatriculaCabecera
                              WHERE Estado = 1 AND IdAlumno = @IdAlumno AND IdPEspecifico = @IdPEspecifico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdAlumno = idAlumno, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado);
                    if (obj != null && obj.ContainsKey("Id"))
                    {
                        return obj["Id"];
                    }
                }
                return null;
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
        /// Obtiene el estado de videos del aula virtual para un programa AONLINE.
        /// </summary>
        public List<VideoAulaVirtualDTO> ObtenerVideosAulaVirtual(int idMatriculaCabecera)
        {
            try
            {
                var rpta = new List<VideoAulaVirtualDTO>();
                var query = @"SELECT IdMatriculaCabecera, IdAlumno, IdPGeneralPadre, IdPGeneralHijo,
                              OrdenSeccion, IdPEspecificoHijo, VideosTerminados, VideosTotal
                              FROM pw.V_PW_ConfiguracionProgramaEstadoVideoAulaVirtual
                              WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<VideoAulaVirtualDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene el estado de encuestas para un programa AONLINE.
        /// </summary>
        public List<EncuestaRealizadaDTO> ObtenerEncuestasRealizadas(int idMatriculaCabecera)
        {
            try
            {
                var rpta = new List<EncuestaRealizadaDTO>();
                var query = @"SELECT IdMatriculaCabecera, IdPEspecifico, IdAlumno, IdPGeneralPadre, IdPGeneralHijo,
                              IdPEspecificoHijo, ExamenProgramados, ExamenRealizado, Completado
                              FROM ope.V_EncuestaRealizadaProgramaAlumno
                              WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<EncuestaRealizadaDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene el estado de tareas para un programa AONLINE.
        /// </summary>
        public List<TareaRealizadaDTO> ObtenerTareasRealizadas(int idMatriculaCabecera)
        {
            try
            {
                var rpta = new List<TareaRealizadaDTO>();
                var query = @"SELECT IdMatriculaCabecera, IdPEspecifico, IdAlumno, IdPGeneralPadre, IdPGeneralHijo,
                              IdPEspecificoHijo, TareasProgramadas, TareasRealizadas, Completado
                              FROM pw.V_PW_TareaRealizadaProgramaAlumno
                              WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.QueryDapper(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<TareaRealizadaDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene los Id de PEspecificoSesion asociados a un PEspecifico.
        /// </summary>
        public List<int> ObtenerIdsPEspecificoSesion(int idPEspecifico)
        {
            try
            {
                var lista = new List<int>();
                var query = "SELECT Id FROM pla.T_PEspecificoSesion WHERE IdPEspecifico = @IdPEspecifico AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    var temp = JsonConvert.DeserializeObject<List<Dictionary<string, int>>>(resultado);
                    if (temp != null)
                    {
                        lista = temp.Select(obj => obj["Id"]).ToList();
                    }
                }
                return lista;
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
        /// Obtiene cuestionarios por PEspecifico con Titulo, FechaEntrega, FechaEntregaSecundaria.
        /// </summary>
        public List<ActividadAtcDTO> ObtenerCuestionariosPorPEspecifico(int idPEspecifico)
        {
            try
            {
                var rpta = new List<ActividadAtcDTO>();
                var query = @"SELECT c.Id AS ActividadId, c.Titulo AS ActividadNombre,
                              c.FechaEntrega AS ActividadFechaEntrega, c.FechaEntregaSecundaria AS ActividadFechaEntregaSecundaria
                              FROM pw.T_PW_PEspecificoSesionCuestionario c
                              INNER JOIN pla.T_PEspecificoSesion s ON c.IdPEspecificoSesion = s.Id
                              WHERE s.IdPEspecifico = @IdPEspecifico AND s.Estado = 1 AND c.Estado = 1 AND c.Publicado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAtcDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene tareas por PEspecifico con Titulo, FechaEntrega, FechaEntregaSecundaria.
        /// </summary>
        public List<ActividadAtcDTO> ObtenerTareasPorPEspecifico(int idPEspecifico)
        {
            try
            {
                var rpta = new List<ActividadAtcDTO>();
                var query = @"SELECT t.Id AS ActividadId, t.Titulo AS ActividadNombre,
                              t.FechaEntrega AS ActividadFechaEntrega, t.FechaEntregaSecundaria AS ActividadFechaEntregaSecundaria
                              FROM pw.T_PW_PEspecificoSesionTarea t
                              INNER JOIN pla.T_PEspecificoSesion s ON t.IdPEspecificoSesion = s.Id
                              WHERE s.IdPEspecifico = @IdPEspecifico AND s.Estado = 1 AND t.Estado = 1 AND t.Publicado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadAtcDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene actividades/recursos de sesion docente para programas ONLINE.
        /// </summary>
        public List<ActividadRecursoSesionDocenteDTO> ObtenerActividadesRecursoSesionDocente(int idPEspecificoSesion)
        {
            try
            {
                var rpta = new List<ActividadRecursoSesionDocenteDTO>();
                var query = @"SELECT * FROM pw.V_PW_ObtenerActividadesRecursoSesionDocente
                              WHERE IdPEspecificoSesion = @IdPEspecificoSesion AND Publicado = 1 AND AsignadoPara = 1
                              ORDER BY Tipo";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPEspecificoSesion = idPEspecificoSesion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ActividadRecursoSesionDocenteDTO>>(resultado);
                }
                return rpta;
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
        /// Obtiene datos del perfil de proyecto de aplicacion.
        /// </summary>
        public DatoPerfilProyectoDTO ObtenerDatoPerfilProyecto(int idMatriculaCabecera)
        {
            try
            {
                var query = @"SELECT ProyectoAplicacion, IdProyecto
                              FROM pw.V_PW_DatoPerfil
                              WHERE IdMatriculaCabecera = @IdMatriculaCabecera";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdMatriculaCabecera = idMatriculaCabecera });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<DatoPerfilProyectoDTO>(resultado);
                }
                return null;
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
        /// Obtiene la configuracion de evaluacion de trabajo por Id.
        /// </summary>
        public ConfigurarEvaluacionTrabajoV2DTO ObtenerConfigurarEvaluacionTrabajo(int idProyecto)
        {
            try
            {
                var query = @"SELECT * FROM pla.V_RegistroConfigurarEvaluacionTrabajo WHERE Id = @Id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = idProyecto });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfigurarEvaluacionTrabajoV2DTO>(resultado);
                }
                return null;
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
        /// Obtiene las instrucciones del documento por seccion.
        /// </summary>
        public List<InstruccionDocumentoSeccionDTO> ObtenerInstruccionesDocumentoSeccion(int idPGeneral, int idDocumento)
        {
            try
            {
                var rpta = new List<InstruccionDocumentoSeccionDTO>();
                var query = @"SELECT Id, Titulo, Contenido, OrdenWeb, ZonaWeb
                              FROM pla.V_registroInstruccionDocumentoSeccion
                              WHERE IdPGeneral = @IdPGeneral AND IdDocumento = @IdDocumento
                              ORDER BY OrdenWeb";
                var resultado = _dapperRepository.QueryDapper(query, new { IdPGeneral = idPGeneral, IdDocumento = idDocumento });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<InstruccionDocumentoSeccionDTO>>(resultado);
                }
                return rpta;
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
        /// Amplia la fecha de entrega de un cuestionario.
        /// </summary>
        public bool AmpliarFechaCuestionario(int idActividad, string fecha)
        {
            try
            {
                var query = @"UPDATE pw.T_PW_PEspecificoSesionCuestionario
                              SET FechaEntregaSecundaria = @Fecha,
                                  UsuarioModificacion = @Usuario,
                                  FechaModificacion = GETDATE()
                              WHERE Id = @IdActividad";
                _dapperRepository.QueryDapper(query, new { IdActividad = idActividad, Fecha = fecha});
                return true;
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
        /// Amplia la fecha de entrega de una tarea.
        /// </summary>
        public bool AmpliarFechaTarea(int idActividad, string fecha)
        {
            try
            {
                var query = @"UPDATE pw.T_PW_PEspecificoSesionTarea
                              SET FechaEntregaSecundaria = @Fecha,
                                  UsuarioModificacion = @Usuario,
                                  FechaModificacion = GETDATE()
                              WHERE Id = @IdActividad";
                _dapperRepository.QueryDapper(query, new { IdActividad = idActividad, Fecha = fecha});
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jose Vega
        /// Fecha: 24/02/2026
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdPEspecifico a partir del Id de una sesion.
        /// </summary>
        public int? ObtenerIdPEspecificoPorSesion(int idPEspecificoSesion)
        {
            try
            {
                var query = @"SELECT TOP 1 IdPEspecifico FROM pla.T_PEspecificoSesion WHERE Id = @IdPEspecificoSesion AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdPEspecificoSesion = idPEspecificoSesion });
                if (!string.IsNullOrEmpty(resultado))
                {
                    var obj = JsonConvert.DeserializeObject<Dictionary<string, int>>(resultado);
                    if (obj != null && obj.ContainsKey("IdPEspecifico"))
                    {
                        return obj["IdPEspecifico"];
                    }
                }
                return null;
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
        /// Obtiene las sesiones con asistencia por matricula.
        /// </summary>
        public List<SesionAsistenciaDTO> ObtenerAsistenciaPorMatricula(int idMatriculaCabecera, int idPEspecifico)
        {
            try
            {
                var rpta = new List<SesionAsistenciaDTO>();
                var resultado = _dapperRepository.QuerySPDapper("pla.SP_ObtenerAsistenciaPorMatricula",
                    new { IdMatriculaCabecera = idMatriculaCabecera, IdPEspecifico = idPEspecifico });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<SesionAsistenciaDTO>>(resultado);
                }
                return rpta;
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
        /// Registra asistencia (justificacion de inasistencia) por matricula y sesion.
        /// </summary>
        public bool RegistrarAsistenciaMatricula(int idMatriculaCabecera, int idPEspecificoSesion)
        {
            try
            {
                _dapperRepository.QuerySPDapper("pw.SP_PW_RegistrarAsistenciaMatricula",
                    new { IdMatriculaCabecera = idMatriculaCabecera, IdPEspecificoSesion = idPEspecificoSesion });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 2026-05-02
        /// Version: 1.0
        /// <summary>
        /// Obtiene las solicitudes vinculadas a un hilo de chat (Portal o WhatsApp).
        /// Llama a ia.SP_ObtenerSolicitudesPorHiloChat.
        /// </summary>
        public IEnumerable<SolicitudPorHiloChatDTO> ObtenerSolicitudesPorHiloChat(int idHiloChat, int idChatbotTipo)
        {
            try
            {
                var query = "ia.SP_ObtenerSolicitudesPorHiloChat";
                var resultado = _dapperRepository.QuerySPDapper(query, new
                {
                    IdHiloChat    = idHiloChat,
                    IdChatbotTipo = idChatbotTipo
                });

                if (string.IsNullOrEmpty(resultado) || resultado.Contains("[]"))
                    return Enumerable.Empty<SolicitudPorHiloChatDTO>();

                return JsonConvert.DeserializeObject<List<SolicitudPorHiloChatDTO>>(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Alexis Arroyo
        /// Fecha: 2026-04-27
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de mensajes WhatsApp ATC de un alumno.
        /// Llama a ia.SP_TChatbotWhatsAppAtcHiloChat_ObtenerMensajesPorAlumno.
        /// </summary>
        public IEnumerable<ChatbotMensajeWhatsAppAtcDTO> ObtenerChatWhatsAppAtcPorAlumno(int idAlumno)
        {
            try
            {
                var resultado = new List<ChatbotMensajeWhatsAppAtcDTO>();
                var query = "ia.SP_TChatbotWhatsAppAtcHiloChat_ObtenerMensajesPorAlumno";
                var response = _dapperRepository.QuerySPDapper(query, new { idAlumno });
                if (!string.IsNullOrEmpty(response) && !response.Contains("[]"))
                    resultado = JsonConvert.DeserializeObject<List<ChatbotMensajeWhatsAppAtcDTO>>(response);
                return resultado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
