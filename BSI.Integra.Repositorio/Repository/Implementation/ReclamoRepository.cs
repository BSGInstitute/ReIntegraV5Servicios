using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{

    /// Repositorio: ReclamoRepository
    /// Autor: Gilmer Quispe.
    /// Fecha: 11/11/2022
    /// <summary>
    /// Gestión general de T_Reclamo
    /// </summary>
    public class ReclamoRepository : GenericRepository<TReclamo>, IReclamoRepository
    {
        private Mapper _mapper;

        public ReclamoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TReclamo, Reclamo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        private TReclamo MapeoEntidad(Reclamo entidad)
        {
            try
            {
                //crea la entidad padre
                TReclamo modelo = _mapper.Map<TReclamo>(entidad);

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
        public TReclamo Update(Reclamo entidad)
        {
            try
            {
                var Reclamo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Reclamo.RowVersion = entidadExistente.RowVersion;

                base.Update(Reclamo);
                return Reclamo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TReclamo Add(Reclamo entidad)
        {
            try
            {
                var Reclamo = MapeoEntidad(entidad);
                base.Insert(Reclamo);
                return Reclamo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 11/11/2022
        /// <summary>
        /// Obtiene la lista de reclamo del alumno por el idmatricula
        /// </summary>
        /// <param name="idMatricula"> Codigo Matricula </param>
        /// <returns> List<ListarReclamosDTO> </returns> obtener
        public List<ListarReclamosDTO> ListarReclamosAlumno(int idMatricula)
        {
            try
            {
                var programasGenerales = new List<ListarReclamosDTO>();
                var _query = string.Empty;
                _query = @"SELECT   Id,IdReclamoEstado,IdMatricula,CodigoMatricula,DNI,NombreAlumno,PersonalAsignado,Descripcion,Origen,IdOrigen,CentroCosto,EstadoMatricula,ReclamoEstado,
                                    IdEstadoReclamo,FechaUltimaLlamada,FechaUltimoCorreo,FechaUltimoWapp,FechaCreacion,IdTipoReclamoAlumno,FechaModificacion,TipoReclamoAlumno 
	                       FROM mkt.V_DatosReclamo 
                           WHERE IdMatricula = @IdMatricula and (IdReclamoEstado = 2 or IdReclamoEstado = 4)";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, new { IdMatricula = idMatricula });
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }

        /// Autor: Jorge Quiñones
        /// Fecha: 30/01/2023
        /// <summary>
        /// actualiza estado de reclamo
        /// </summary>
        /// <param name="id"> id del registro , name="usuario" usuario modificacion</param>
        /// <returns> bool </returns> 
        public bool ConfirmarReclamo(int id, string usuario, string comentario)
        {
            try
            {
                var _query = string.Empty;
                _query = @"mkt.SP_ConfirmarReclamo";
                var pgeneralDB = _dapperRepository.QuerySPDapper(_query, new { Id = id, Usuario = usuario, Comentario = comentario });

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Jorge Quiñones
        /// Fecha: 30/01/2023
        /// <summary>
        /// actualiza estado de reclamo
        /// </summary>
        /// <param name="id"> id del registro , name="usuario" usuario modificacion</param>
        /// <returns> bool </returns> 
        public bool EnviarReclamo(int id, string usuario)
        {
            try
            {
                var _query = string.Empty;
                _query = @"mkt.SP_EnviarReclamo"; 
                var pgeneralDB = _dapperRepository.QuerySPDapper(_query, new { Id = id, Usuario = usuario});

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/01/2023
        /// <summary>
        /// actualiza estado de reclamo
        /// </summary>
        /// <param name="id"> id del registro , name="usuario" usuario modificacion</param>
        /// <returns> bool </returns> 
        public bool EliminarReclamo(int id, string usuario)
        {
            try
            {
                var _query = string.Empty;
                _query = @"mkt.SP_EliminarReclamo";
                var pgeneralDB = _dapperRepository.QuerySPDapper(_query, new { Id = id, UsuarioModificacion = usuario });
                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/01/2023
        /// <summary>
        /// actualiza estado de reclamo
        /// </summary>
        /// <param name="id"> id del registro , name="usuario" usuario modificacion</param>
        /// <returns> bool </returns> 
        public bool ReclamoSinContacto(int id, string usuario)
        {
            try
            {
                var _query = string.Empty;
                _query = @"[mkt].[SP_ReprogramarReclamo]";
                var pgeneralDB = _dapperRepository.QuerySPDapper(_query, new { Id = id, Usuario = usuario });

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 30/01/2023
        /// <summary>
        /// actualiza estado de reclamo
        /// </summary>
        /// <param name="id"> id del registro , name="usuario" usuario modificacion</param>
        /// <returns> bool </returns> 
        public bool ResolverReclamoAreas(ReclamoSolucionDTO reclamo)
        {
            try
            {
                var _query = string.Empty;
                _query = @"[mkt].[SP_ResolverReclamoAreas]";
                var queryDelete = _dapperRepository.QuerySPDapper(_query, new
                {
                    Id = reclamo.Id,
                    Usuario = reclamo.Usuario,
                    Comentario = reclamo.Comentario
                });

                return true;

            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> ListarReclamos()
        {
            try
            {

                List<ListarReclamosDTO> programasGenerales = new List<ListarReclamosDTO>();
                var _query = string.Empty;
                _query = @"SELECT TOP 100
                                   Id,
                                   IdReclamoEstado,
                                   IdMatricula,
                                   CodigoMatricula,
                                   DNI,
                                   NombreAlumno,
                                   PersonalAsignado,
                                   Descripcion,
                                   Origen,
                                   IdOrigen,
                                   CentroCosto,
                                   EstadoMatricula,
                                   ReclamoEstado,
                                   IdEstadoReclamo,
                                   FechaUltimaLlamada,
                                   FechaUltimoCorreo,
                                   FechaUltimoWapp,
                                   FechaCreacion,
                                   IdTipoReclamoAlumno,
                                   FechaModificacion,
                                   TipoReclamoAlumno,
                                   ComentarioSolucion 
                            FROM mkt.V_DatosReclamo order by FechaCreacion desc ";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: joseph Llanque
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> ObtenerReclamosPorAlumno(int idMatricula)
        {
            try
            {

                List<ListarReclamosDTO> programasGenerales = new List<ListarReclamosDTO>();
                var _query = string.Empty;
                _query = @"SELECT Id,
                                   IdReclamoEstado,
                                   IdMatricula,
                                   CodigoMatricula,
                                   DNI,
                                   NombreAlumno,
                                   PersonalAsignado,
                                   Descripcion,
                                   Origen,
                                   IdOrigen,
                                   CentroCosto,
                                   EstadoMatricula,
                                   ReclamoEstado,
                                   IdEstadoReclamo,
                                   FechaUltimaLlamada,
                                   FechaUltimoCorreo,
                                   FechaUltimoWapp,
                                   FechaCreacion,
                                   IdTipoReclamoAlumno,
                                   FechaModificacion,
                                   TipoReclamoAlumno,
                                   ComentarioSolucion
                            FROM mkt.V_DatosReclamo where IdMatricula =" + idMatricula;
                var pgeneralDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Gilmer Quispe
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosAreasDTO> ListarReclamosAreas()
        {
            try
            {

                List<ListarReclamosAreasDTO> programasGenerales = new List<ListarReclamosAreasDTO>();
                var _query = string.Empty;
                _query = @"SELECT Id,
                                  IdReclamoEstado,
                                  ReclamoEstado,
                                  IdOrigen,
                                  Origen,
                                  Area,
                                  IdTipoReclamo,
                                  TipoReclamo,
                                  Usuario,
                                  CodigoMatricula,
                                  Descripcion,
                                  ComentarioSolucion,
                                  FechaCreacion,
                                  FechaSolucion FROM mkt.V_ObtenerListaSolicitudAreas ORDER BY  FechaCreacion DESC";
                var pgeneralDB = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(pgeneralDB) && !pgeneralDB.Contains("[]"))
                {
                    programasGenerales = JsonConvert.DeserializeObject<List<ListarReclamosAreasDTO>>(pgeneralDB);
                }
                return programasGenerales;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 17/10/2023
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<registroTipoReclamoAlumnoDTO> ObtenerListaTipoReclamoAlumno()
        {
            try
            {

                List<registroTipoReclamoAlumnoDTO> tipos = new List<registroTipoReclamoAlumnoDTO>();
                var _query = string.Empty;
                _query = @"select  Id,
                                   Nombre FROM mkt.T_TipoReclamoAlumno WHERE Estado=1";
                var tipoReclamo = _dapperRepository.QueryDapper(_query, null);
                if (!string.IsNullOrEmpty(tipoReclamo) && !tipoReclamo.Contains("[]"))
                {
                    tipos = JsonConvert.DeserializeObject<List<registroTipoReclamoAlumnoDTO>>(tipoReclamo);
                }
                return tipos;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }


        /// Autor:Joseph Llanque
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public ListarReclamosAreasDTO InsertarReclamosArea(ReclamoAreasDTO listadoBO)
        {
            try
            {

                string _queryInsertar = "mkt.SP_InsertarReclamoAreas";
                var query = _dapperRepository.QuerySPFirstOrDefault(_queryInsertar, new
                {
                    IdReclamoEstado = listadoBO.IdReclamoEstado,
                    IdOrigen = listadoBO.IdOrigen,
                    IdArea = listadoBO.IdArea,
                    IdTipoReclamo = listadoBO.IdTipoReclamoAlumno,
                    CodigoMatricula = listadoBO.CodigoMatricula,
                    Descripcion = listadoBO.Descripcion,
                    Estado = listadoBO.Estado,
                    UsuarioCreacion = listadoBO.UsuarioCreacion,
                    UsuarioModificacion = listadoBO.UsuarioModificacion,
                    FechaCreacion = listadoBO.FechaCreacion,
                    FechaModificacion = listadoBO.FechaModificacion,

                });
                return JsonConvert.DeserializeObject<ListarReclamosAreasDTO>(query);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor:Joseph Llanque
        /// Fecha: 26/12/2022
        /// <summary>
        /// Obtiene la lista de reclamo 
        /// </summary> 
        /// <returns> List<ListarReclamosDTO> </returns> 
        public List<ListarReclamosDTO> GenerarReporteReclamo(ReclamoFiltroDTO listadoBO)
        {
            try
            {

                string _queryInsertar = "mkt.SP_GenerarReporteReclamos";
                 var query = _dapperRepository.QuerySPDapper(_queryInsertar, new
                {
                     idMatricula = (int?)listadoBO.idMatricula,
                     FechaInicio = listadoBO.FechaInicio,                  
                     FechaFin = listadoBO.FechaFin
                 });
                return JsonConvert.DeserializeObject<List<ListarReclamosDTO>>(query);
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Joseph Llanque
        /// Fecha: 19/10/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los atributos de reclamo
        /// </summary>
        /// <param name="id">Id de la Empresa</param>
        /// <returns> ValorIntDTO </returns>
        public Reclamo ObtenerPorId(int id)
        {
            try
            {
                var rpta = new Reclamo();
                var query = @"SELECT Id,
                              IdMatriculaCabecera,
                              Descripcion,
                              IdReclamoEstado,
                              IdOrigen,
                              Estado,
                              UsuarioCreacion,
                              UsuarioModificacion,
                              FechaCreacion,
                              FechaModificacion,
                              RowVersion,
                              IdMigracion,
                              IdTipoReclamoAlumno,
                              NroDiasSolucion,
                              IdEstadoMatricula_Previo,
                              FechaReclamoRealizadoFin,
                              IdCategoriaTicket,
                              ComentarioSolucion,
                              AutoReprogramacion FROM mkt.T_Reclamo WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<Reclamo>(resultado);
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
