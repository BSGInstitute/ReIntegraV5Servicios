using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: SolicitudOperacionesRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 20/10/2022
    /// <summary>
    /// Gestión general de T_SolicitudOperacione
    /// </summary>
    public class SolicitudOperacionesRepository : GenericRepository<TSolicitudOperacione>, ISolicitudOperacionesRepository
    {
        private Mapper _mapper;

        public SolicitudOperacionesRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSolicitudOperacione, SolicitudOperaciones>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TSolicitudOperacione MapeoEntidad(SolicitudOperaciones entidad)
        {
            try
            {
                //crea la entidad padre
                TSolicitudOperacione modelo = _mapper.Map<TSolicitudOperacione>(entidad);

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

        public TSolicitudOperacione Add(SolicitudOperaciones entidad)
        {
            try
            {
                var SolicitudOperaciones = MapeoEntidad(entidad);
                base.Insert(SolicitudOperaciones);
                return SolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSolicitudOperacione Update(SolicitudOperaciones entidad)
        {
            try
            {
                var SolicitudOperaciones = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SolicitudOperaciones.RowVersion = entidadExistente.RowVersion;

                base.Update(SolicitudOperaciones);
                return SolicitudOperaciones;
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


        public IEnumerable<TSolicitudOperacione> Add(IEnumerable<SolicitudOperaciones> listadoEntidad)
        {
            try
            {
                List<TSolicitudOperacione> listado = new List<TSolicitudOperacione>();
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

        public IEnumerable<TSolicitudOperacione> Update(IEnumerable<SolicitudOperaciones> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSolicitudOperacione> listado = new List<TSolicitudOperacione>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 20/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Valor Nuevo a través de los parámetros
        /// </summary>
        /// <param name="aprobado"></param>
        /// <param name="realizado"></param>
        /// <param name="idOportunidad"></param>
        /// <param name="iPlantillaBaseWhatsAppFacebook"></param>
        /// <returns></returns>
        public SolicitudOperaciones ObtenerValorNuevo(bool aprobado, bool realizado, int idOportunidad, int iPlantillaBaseWhatsAppFacebook)
        {
            try
            {
                SolicitudOperaciones respuesta = new SolicitudOperaciones();
                var query = @" SELECT  ValorNuevo FROM ope.T_SolicitudOperaciones WHERE Aprobado = 1 AND Realizado = 1 AND IdOportunidad = @IdOportunidad 
                               AND IdTipoSolicitudOperaciones = 8 AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { aprobado, realizado, idOportunidad, iPlantillaBaseWhatsAppFacebook });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<SolicitudOperaciones>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 31/10/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene tupla por IdSolicitudOperaciones
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns></returns>
        public SolicitudOperaciones ObtenerPorIdSolicitudOperaciones(int idSolicitudOperaciones)
        {
            try
            {
                SolicitudOperaciones respuesta = new SolicitudOperaciones();
                var query = @" SELECT 
                                    Id	,
		                            IdOportunidad,
		                            IdTipoSolicitudOperaciones,
		                            FechaSolicitud,
		                            IdPersonal_Solicitante AS IdPersonalSolicitante,
		                            IdPersonal_Aprobacion AS IdPersonalAprobacion,
		                            ValorAnterior,
		                            ValorNuevo,
		                            Aprobado,
		                            EsCancelado,
		                            ComentarioSolicitante,
		                            Observacion,
		                            Estado,
		                            UsuarioCreacion,
		                            UsuarioModificacion,
		                            FechaCreacion,
		                            FechaModificacion,
		                            IdMigracion,
		                            IdUrlBlockStorage,
		                            NombreArchivo,
		                            ContentType,
		                            Realizado,
		                            ObservacionEncargado,
		                            FechaAprobacion,
		                            RelacionEstadoSubEstado,
		                            EnvioAutomatico 
                               FROM ope.T_SolicitudOperaciones WHERE Id = @IdSolicitudOperaciones AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdSolicitudOperaciones = idSolicitudOperaciones });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<SolicitudOperaciones>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Obtiene solicitudes de cambio con validacion por bloque
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesEnBloque(int IdOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante," +
                                         "PersonalSolicitante,IdPersonalAprobacion,PersonalAprobacion,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante" +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion,RelacionEstadoSubEstado " +
                                         "from ope.V_ObtenerSolicitudOperacionesConRelacionEstado where IdOportunidad=@IdOportunidad and  Aprobado = 0 and EsCancelado = 0 and Realizado = 0 order by FechaSolicitud desc";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, new { IdOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud);
                }

                return datosSolicitudOperaciones;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Miguel Quiñones
        /// Fecha: 15/02/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene tupla por IdSolicitudOperaciones
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns></returns>
        public SolicitudOperaciones ObtenerPorIdAprobadoSolicitudOperaciones(int idSolicitudOperaciones)
        {
            try
            {
                SolicitudOperaciones respuesta = new SolicitudOperaciones();
                var query = @" SELECT 
                                    Id	,
		                            IdOportunidad,
		                            IdTipoSolicitudOperaciones,
		                            FechaSolicitud,
		                            IdPersonal_Solicitante AS IdPersonalSolicitante,
		                            IdPersonal_Aprobacion AS IdPersonalAprobacion,
		                            ValorAnterior,
		                            ValorNuevo,
		                            Aprobado,
		                            EsCancelado,
		                            ComentarioSolicitante,
		                            Observacion,
		                            Estado,
		                            UsuarioCreacion,
		                            UsuarioModificacion,
		                            FechaCreacion,
		                            FechaModificacion,
		                            IdMigracion,
		                            IdUrlBlockStorage,
		                            NombreArchivo,
		                            ContentType,
		                            Realizado,
		                            ObservacionEncargado,
		                            FechaAprobacion,
		                            RelacionEstadoSubEstado,
		                            EnvioAutomatico 
                               FROM ope.T_SolicitudOperaciones WHERE Id = @IdSolicitudOperaciones AND Estado = 1 AND Aprobado = 0";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdSolicitudOperaciones = idSolicitudOperaciones });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    respuesta = JsonConvert.DeserializeObject<SolicitudOperaciones>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene solicitud de operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO> </returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperaciones(int idOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante," +
                                         "PersonalSolicitante,IdPersonalAprobacion,PersonalAprobacion,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante" +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion " +
                                         "from ope.V_ObtenerSolicitudOperaciones where IdOportunidad=@IdOportunidad and  Aprobado = 0 and EsCancelado = 0 and Realizado = 0 order by FechaSolicitud desc";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, new { idOportunidad });
                if (!string.IsNullOrEmpty(querySolicitud) && !querySolicitud.Contains("[]"))
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud)!;
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar
        /// Fecha: 02/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todas las  solicitudes de operaciones
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO> </returns>
        public List<TodoSolicitudOperacionesDTO> ObtenerTodoSolicitudOperaciones()
        {
            try
            {
                List<TodoSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<TodoSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante, " +
                                         "PersonalSolicitante,EmailSolicitante,IdPersonalAprobacion,PersonalAprobacion,EmailAprobador,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante " +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion,NombreCompleto,Correo,Dni,Correo " +
                                         ",CentroCosto,Pespecifico,CodigoMatricula,Direccion " +
                                         "from ope.V_ObtenerTodoSolicitudOperaciones  order by FechaSolicitud desc";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, null);
                if (!string.IsNullOrEmpty(querySolicitud) && !querySolicitud.Contains("[]"))
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<TodoSolicitudOperacionesDTO>>(querySolicitud)!;
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TodoSolicitudOperacionesDTO> ObtenerTodoFiltroOperaciones(filtroReportetipo4DTO filtroSolicitudReporte)
        {

            try
            {
                var filtroOrdenado="";
                if (filtroSolicitudReporte.asesores.Count() > 0)
                {
                    filtroOrdenado = String.Join(",", filtroSolicitudReporte.asesores);
                }

 


                filtroSolicitudReporte.fechaFin = new DateTime(filtroSolicitudReporte.fechaFin.Year, filtroSolicitudReporte.fechaFin.Month, filtroSolicitudReporte.fechaFin.Day, 23, 59, 59);
                filtroSolicitudReporte.fechaInicio = new DateTime(filtroSolicitudReporte.fechaInicio.Year, filtroSolicitudReporte.fechaInicio.Month, filtroSolicitudReporte.fechaInicio.Day, 0, 0, 0);



                List<TodoSolicitudOperacionesDTO> listaSolicitudOperacion = new List<TodoSolicitudOperacionesDTO>();
                var registro = _dapperRepository.QuerySPDapper("com.sp_ReporteSolicitudFiltro", new { 
                        asesores = filtroOrdenado, 
                        fechaInicio = filtroSolicitudReporte.fechaInicio, 
                    fechaFin = filtroSolicitudReporte.fechaFin, 
                    tipoSolicitud = filtroSolicitudReporte.tipoSolicitud, 
                    estadoSolicitud = filtroSolicitudReporte.estadoSolicitud });
                if (!string.IsNullOrEmpty(registro) && !registro.Contains("[]"))
                {
                    listaSolicitudOperacion = JsonConvert.DeserializeObject<List<TodoSolicitudOperacionesDTO>>(registro)!;
                }
                return listaSolicitudOperacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public List<TodoSolicitudOperacionesDTO> ObtenerTodoFiltroOperacionesCompleto(filtroReporteDTO filtroSolicitudReporte)
        {
            try
            {
                List<TodoSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<TodoSolicitudOperacionesDTO>();
                string _querySolicitud = "Select Id,IdOportunidad,IdTipoSolicitudOperaciones,TipoSolicitudOperaciones,FechaSolicitud,IdPersonalSolicitante, " +
                                         "PersonalSolicitante,EmailSolicitante,IdPersonalAprobacion,PersonalAprobacion,EmailAprobador,ValorAnterior,ValorNuevo,Aprobado,EsCancelado,ComentarioSolicitante " +
                                         ",Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,ObservacionEncargado,FechaAprobacion,NombreCompleto,Correo,Dni,Correo " +
                                         ",CentroCosto,Pespecifico,CodigoMatricula,Direccion " +
                                         "from ope.V_ObtenerTodoSolicitudOperaciones  order by FechaSolicitud desc";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, null);
                if (!string.IsNullOrEmpty(querySolicitud) && !querySolicitud.Contains("[]"))
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<TodoSolicitudOperacionesDTO>>(querySolicitud)!;
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public List<TipoSolicitudDTO> ObtenerTipoSolicitud()
        {
            try
            {
                List<TipoSolicitudDTO> datosSolicitudOperaciones = new List<TipoSolicitudDTO>();
                string _querySolicitud = "SELECT id, TipoSolicitud FROM ope.T_TipoSolicitudOperaciones WHERE Estado = 1";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, null);
                if (!string.IsNullOrEmpty(querySolicitud) && !querySolicitud.Contains("[]"))
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<TipoSolicitudDTO>>(querySolicitud)!;
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene solicitud de Operaciones Realizadas
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO </returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerSolicitudOperacionesRealizadas(int idOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> datosSolicitudOperaciones = new List<DatosSolicitudOperacionesDTO>();
                string _querySolicitud = @"SELECT 
                                            Id, IdOportunidad, IdTipoSolicitudOperaciones, TipoSolicitudOperaciones, FechaSolicitud, IdPersonalSolicitante, 
                                            PersonalSolicitante, IdPersonalAprobacion, PersonalAprobacion, ValorAnterior, ValorNuevo, Aprobado, EsCancelado, 
                                            ComentarioSolicitante, Observacion,IdUrlBlockStorage,UrlBlockStorage,NombreArchivo,ContentType,Realizado,
                                            ObservacionEncargado, FechaAprobacion 
                                        FROM
                                            ope.V_ObtenerSolicitudOperaciones 
                                        WHERE 
                                            IdOportunidad=@IdOportunidad AND (EsCancelado = 1 OR Aprobado = 1 OR Realizado = 1) ORDER BY FechaSolicitud DESC";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, new { IdOportunidad = idOportunidad });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(querySolicitud)!;
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// Autor: Joseph Llanque
        /// Fecha: 31/05/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene historial solicitudes
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> List<DatosSolicitudOperacionesDTO </returns>
        public List<HistorialAsesoraDTO> ObtenerHistorialAsesora(int idMatriculaCabecera)
        {
            try
            {
                List<HistorialAsesoraDTO> datosSolicitudOperaciones = new List<HistorialAsesoraDTO>();
                string _querySolicitud = @"
                                        SELECT
                                        	IdOportunidad
                                        	,IdMatriculaCabecera
                                        	,IdOportunidad
                                        	,IdMatriculaCabecera
                                        	,AsesoraAnterior
                                        	,AsesoraNueva
                                        	,FechaInicio
                                        	,FechaFin
                                        	,UsuarioAProbacion
                                        FROM
                                        	ope.V_LogOportunidadAsesor
                                        where IdMatriculaCabecera=@idMatriculaCabecera                                    
";
                string querySolicitud = _dapperRepository.QueryDapper(_querySolicitud, new { IdMatriculaCabecera = idMatriculaCabecera });

                if (!querySolicitud.Contains("[]") && querySolicitud != "")
                {
                    datosSolicitudOperaciones = JsonConvert.DeserializeObject<List<HistorialAsesoraDTO>>(querySolicitud);
                }
                return datosSolicitudOperaciones;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/11/2022
        /// Version: 1.0
        /// <summary>
		/// Se obtiene el historial de acceso temporal por IdOportunidad
		/// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
		/// <returns>Lista de datos de solicitud de operaciones</returns>
        public List<DatosSolicitudOperacionesDTO> ObtenerHistorialAccesoTemporal(int idOportunidad)
        {
            try
            {
                List<DatosSolicitudOperacionesDTO> listaSolicitudOperacion = new List<DatosSolicitudOperacionesDTO>();
                var registro = _dapperRepository.QuerySPDapper("ope.SP_ObtenerSolicitudOportunidadAccesoTemporal", new { IdOportunidad = idOportunidad });
                if (!string.IsNullOrEmpty(registro) && !registro.Contains("[]"))
                {
                    listaSolicitudOperacion = JsonConvert.DeserializeObject<List<DatosSolicitudOperacionesDTO>>(registro)!;
                }
                return listaSolicitudOperacion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <param name="valorNuevo"></param>
        /// <returns> IntDTO </returns>
        public IntDTO ValidarCambioSubEstado(int idOportunidad, string valorNuevo)
        {
            try
            {
                IntDTO resultado = new IntDTO();
                var query = "ope.SP_ValidarCriteriosSubEstadoMatricula";
                var pEspecificoDB = _dapperRepository.QuerySPFirstOrDefault(query, new { IdOportunidad = idOportunidad, ValorNuevo = valorNuevo });
                if (!pEspecificoDB.Contains("[]") && pEspecificoDB != "")
                {
                    resultado = JsonConvert.DeserializeObject<IntDTO>(pEspecificoDB)!;
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdOportunidad de Operaciones 
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> IntDTO </returns>
        public IntDTO ActualizarTerminosPortalWeb(int idOportunidad)
        {
            try
            {
                IntDTO resultado = new IntDTO();
                var query = "ope.SP_ActualizarTerminosPortalWeb";
                var pEspecificoDB = _dapperRepository.QuerySPFirstOrDefault(query, new { IdOportunidad = idOportunidad });
                if (!pEspecificoDB.Contains("[]") && pEspecificoDB != "")
                {
                    resultado = JsonConvert.DeserializeObject<IntDTO>(pEspecificoDB);
                }
                return resultado;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Aprobar solicitud de cambio categoria
        /// </summary>         
        public void AprobarCambioCategoriaAlumno(int idOportunidad, String categoria)
        {
            try
            {
                _dapperRepository.QuerySPDapper("OPE.SP_CambiaCategoriaAlumno", new { idOportunidad, categoria });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene un valor bool por condiciones de la query
        /// </summary>
        /// <param name="idSolicitudOperaciones"></param>
        /// <returns> bool </returns>
        public bool ExisteTotal(int idOportunidad, int idTipoSolicitudOperaciones)
        {
            try
            {
                var respuesta = false;
                var query = @" 
                            SELECT 
                                Id, IdOportunidad
                            FROM 
                                ope.T_SolicitudOperaciones 
                            WHERE 
                                IdOportunidad = @IdOportunidad AND IdTipoSolicitudOperaciones = @IdTipoSolicitudOperaciones AND Aprobado = 0 AND EsCancelado = 0 AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad, IdTipoSolicitudOperaciones = idTipoSolicitudOperaciones });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = true;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene el IdMatriculaCabecera por la oportunidad
        /// </summary>
        /// <param name="idOportunidad"></param>
        /// <returns> int: respuesta.Valor </returns>
        public int ObtenerMatriculaPorOportunidad(int idOportunidad)
        {
            try
            {
                var respuesta = new ValorIntDTO();
                var query = $@"
                              SELECT 
                                IdMatriculaCabecera as Valor 
                              FROM 
                                ope.T_oportunidadClasificacionOperaciones 
                              WHERE 
                                IdOportunidad = @IdOportunidad";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdOportunidad = idOportunidad });

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("null"))
                {
                    respuesta = JsonConvert.DeserializeObject<ValorIntDTO>(resultado);
                }
                return respuesta.Valor.Value;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Registra en el nuevo aula virtual los cursos de prueba segun la solicitud ingresada
        /// </summary>
        /// <param name="idSolicitudOperaciones">Id de la solicitud de operaciones (PK de la tabla ope.T_SolicitudOperaciones)</param>
        public void RegistrarCursoPrueba(int idSolicitudOperaciones)
        {
            try
            {
                _dapperRepository.QuerySPDapper("ope.SP_RegistrarAccesoPruebaSolicitud", new { IdSolicitudOperacion = idSolicitudOperaciones });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/12/2022
        /// Version: 1.0
        /// <summary>
        /// Amplia Accesos Temporales
        /// </summary>
        /// <param name="idAlumno"></param>
        /// <param name="fechaExpiracion"></param>
        /// <param name="idPEspecifico"></param>
        public void AmpliacionAccesosTemporales(int idAlumno, string fechaExpiracion, string idPEspecifico)
        {

            int[] cursoid = idPEspecifico.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
            for (int i = 0; i < cursoid.Length; i++)
            {
                try
                {
                    _dapperRepository.QuerySPDapper("OPE.SP_AmpliacionAccesosTemporales", new { idAlumno = idAlumno, fechaExpiracion = fechaExpiracion, idPEspecifico = cursoid[i] });
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}