using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
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
    }
}
