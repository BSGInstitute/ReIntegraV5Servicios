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
                    Estado
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
        public IEnumerable<ChatbotHiloChatPorAlumnoDTO> ObtenerHilosChatConAlumnos()
        {
            try
            {
                List<ChatbotHiloChatPorAlumnoDTO> rpta = new List<ChatbotHiloChatPorAlumnoDTO>();
                var query = @"EXEC ia.SP_ChatbotPortalHiloChat_ObtenerHilosActivos";

                var resultado = _dapperRepository.QueryDapper(query, null);

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ChatbotHiloChatPorAlumnoDTO>>(resultado);
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
                var query = @"EXEC ia.SP_ChatbotPortalHiloChat_ObtenerHilosSinAlumno";

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

        /// Autor: Jose Vega
        /// Fecha: 23/10/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todas las respuestas del cliente por formulario aplicado
        /// </summary>
        /// <param name="IdFormularioAplicadoChatbot">ID del formulario aplicado</param>
        /// <returns>Lista de respuestas del cliente con detalles de pregunta y respuesta predefinida</returns>
        public IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdFormularioAplicadoChatbot)
        {
            try
            {
                List<RespuestaClienteDTO> rpta = new List<RespuestaClienteDTO>();
                var query = $@"EXEC ia.SP_TRespuestaClienteChatbot_ObtenerRespuestasUsuario @IdFormularioAplicadoChatbot = @IdFormularioAplicadoChatbot";
                var resultado = _dapperRepository.QueryDapper(query, new { IdFormularioAplicadoChatbot });

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

        // En tu repositorio correspondiente
        public InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompleta(
            int idChatbotPortalHiloChat,
            int idVersionFormularioEvaluacionChatbot,
            string usuarioCreacion,
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
            query = @"SELECT NombreRemitente, Mensaje, Fecha, IdRemitente  FROM com.V_ObtenerChatDetallePorInteraccionChat
                      WHERE IdInteraccionChatIntegra = @idInteraccion ORDER BY IdInteraccionChatIntegra , Fecha ASC";
            var chatDetallesIntegraDB = _dapperRepository.QueryDapper(query, new { idInteraccion });
            chatDetallesIntegra = JsonConvert.DeserializeObject<List<ChatDetalleIntegra>>(chatDetallesIntegraDB);
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
                var resultado = _dapperRepository.QuerySPDapper("[ope].[SP_HistorialChatSoporte]", new { idMatriculaCabecera }) ;
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

        /// Autor: Gilmer Quispe.
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
                var resultado = _dapperRepository.QuerySPDapper("[ope].[SP_UpdateChatFinalizado]", new { IdMatriculaCabecera = idMatriculaCabecera, UsuarioModificacion = username });
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// Autor: Joseph Llanque.
        ///// Fecha: 05/12/2022
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene un listado de ChatDetalleIntegra filtrado por IdAlumno
        ///// </summary>
        ///// <param name="idAlumno"> Id de Alumno </param>
        ///// <returns> Lista de Objeto BO : List<ChatDetalleIntegra> </returns>
        public bool FinalizarChatComercial(int idMatriculaCabecera, string username)
        {
            try
            {
                var resultado = _dapperRepository.QuerySPDapper("[ope].[SP_UpdateChatFinalizadoComercial]", new { IdMatriculaCabecera = idMatriculaCabecera, UsuarioModificacion = username });
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
                            FROM [ope].[V_ObtenerDatosSesionChat]
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
                            FROM [ope].[V_ObtenerDatosSesionChat]
                            WHERE IdAlumno = @idAlumno
                            order by fechaInicio desc";
            var resultado = _dapperRepository.QueryDapper(query, new { idAlumno = idAlumno });
            if (!string.IsNullOrEmpty(resultado) && resultado != "null")
            {
                respuesta = JsonConvert.DeserializeObject<List<DatosSesionChatComercialDTO>>(resultado)!;
            }
            return respuesta;
        }
    }
}
