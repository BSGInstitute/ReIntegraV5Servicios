using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: PGeneralParametroSeoPwRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 10/08/2022
    /// <summary>
    /// Gestión general de T_PGeneralParametroSEOPw
    /// </summary>
    public class PgeneralParametroSeoPwRepository : GenericRepository<TPgeneralParametroSeoPw>, IPgeneralParametroSeoPwRepository
    {
        private Mapper _mapper;

        public PgeneralParametroSeoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TPgeneralParametroSeoPw, PgeneralParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TPgeneralParametroSeoPw MapeoEntidad(PgeneralParametroSeoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TPgeneralParametroSeoPw modelo = _mapper.Map<TPgeneralParametroSeoPw>(entidad);

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

        public TPgeneralParametroSeoPw Add(PgeneralParametroSeoPw entidad)
        {
            try
            {
                var PGeneralParametroSeoPw = MapeoEntidad(entidad);
                Insert(PGeneralParametroSeoPw);
                return PGeneralParametroSeoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TPgeneralParametroSeoPw Update(PgeneralParametroSeoPw entidad)
        {
            try
            {
                var PGeneralParametroSeoPw = MapeoEntidad(entidad);
                var entidadExistente = FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                PGeneralParametroSeoPw.RowVersion = entidadExistente.RowVersion;

                Update(PGeneralParametroSeoPw);
                return PGeneralParametroSeoPw;
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


        public IEnumerable<TPgeneralParametroSeoPw> Add(IEnumerable<PgeneralParametroSeoPw> listadoEntidad)
        {
            try
            {
                List<TPgeneralParametroSeoPw> listado = new List<TPgeneralParametroSeoPw>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }
                Insert(listado);
                return listado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<TPgeneralParametroSeoPw> Update(IEnumerable<PgeneralParametroSeoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TPgeneralParametroSeoPw> listado = new List<TPgeneralParametroSeoPw>();
                foreach (var entidad in listadoEntidad)
                {
                    listado.Add(MapeoEntidad(entidad));
                }

                var infoExistente = GetBy(w => listadoEntidad.Select(s => s.Id).Contains(w.Id), s => new { s.Id, s.RowVersion });
                foreach (var item in listado)
                {
                    var entidadExistente = infoExistente.FirstOrDefault(w => w.Id == item.Id);
                    item.RowVersion = entidadExistente.RowVersion;
                }
                Update(listado);
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
        /// Fecha: 11/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los parametros SEO de un programa general
        /// </summary>
        /// <param name="idPGeneral">Id Programa General </param>
        /// <returns> Lista Parametros SEO: List<ParametroSeoPGeneralDTO></returns>
        public IEnumerable<PgeneralParametroSeoPwDTO> ObtenerParametrosSEOPorIdPGeneral(int idPGeneral)
        {
            try
            {
                List<PgeneralParametroSeoPwDTO> rpta = new List<PgeneralParametroSeoPwDTO>();
                var query = @"SELECT Id,
	                            IdProgramaGeneral AS IdPgeneral,
	                            Descripcion,
	                            Nombre AS NombreParametroSeo
                            FROM  pla.V_ParametrosSeoPrograma WHERE IdProgramaGeneral = @idPGeneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<PgeneralParametroSeoPwDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 19/06/2023
        /// <summary>
        ///  Obtiene la lista de Parametros Seo y su descripcion  registradas en el sistema
        ///  para un programa.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PgeneralParametroSeoPwDTO> ObtenerPgeneralParametroSeoPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var query = @"SELECT 
                                    IdPGeneralParametroSEO AS Id,
                                   IdPgeneral,
                                   NombreParametro AS NombreParametroSeo,
                                   IdParametroSEO AS IdParametroSeo,
                                   Descripcion
                            FROM pla.V_TPGeneral_ParametrosSeo
                            WHERE EstadoSeo = 1
                                  AND EstadoPrograma = 1
                                  AND IdPgeneral = @idPGeneral;";
                var resultado = _dapperRepository.QueryDapper(query, new { idPGeneral });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralParametroSeoPwDTO>>(resultado)!;
                }
                return new List<PgeneralParametroSeoPwDTO>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene los registros asociados al IdPGeneral
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <returns> IEnumerable<PGeneralParametroSeoPw> </returns>
        public IEnumerable<PgeneralParametroSeoPw> ObtenerPorIdPGeneral(int idPGeneral)
        {
            try
            {
                var _query = @"SELECT Id,
                                   Descripcion,
                                   IdPGeneral AS IdPgeneral,
                                   IdParametroSEO AS IdParametroSeo,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralParametroSEO_PW
                            WHERE Estado = 1
                                  AND IdPGeneral = @IdPGeneral;";
                var respuestaDapper = _dapperRepository.QueryDapper(_query, new { IdPgeneral = idPGeneral });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("[]"))
                {
                    return JsonConvert.DeserializeObject<IEnumerable<PgeneralParametroSeoPw>>(respuestaDapper)!;
                }
                return new List<PgeneralParametroSeoPw>();
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 10/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <param name="idPGeneral"> (PK) de T_PGeneral </param>
        /// <param name="idParametroSeo"></param>
        /// <returns> IEnumerable<PGeneralParametroSeoPw> </returns>
        public PgeneralParametroSeoPw? ObtenerPorIdPGeneralIdParametroSeo(int idPGeneral, int idParametroSeo)
        {
            try
            {
                var _query = @"SELECT Id,
                                   Descripcion,
                                   IdPGeneral AS IdPgeneral,
                                   IdParametroSEO AS IdParametroSeo,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralParametroSEO_PW
                            WHERE Estado = 1
                                  AND IdPGeneral = @idPGeneral AND IdParametroSEO =@idParametroSeo;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { idPGeneral, idParametroSeo });
                if (!string.IsNullOrEmpty(respuestaDapper) && respuestaDapper != "null")
                {
                    return JsonConvert.DeserializeObject<PgeneralParametroSeoPw>(respuestaDapper)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: GIlmer Quispe.
        /// Fecha: 28/06/2023
        /// Version: 1.0
        /// <summary>
        ///  Obtiene el registro por el Id
        /// </summary>
        /// <param name="id"> (PK) </param>
        /// <returns> PGeneralParametroSeoPw </returns>
        public PgeneralParametroSeoPw? ObtenerPorId(int id)
        {
            try
            {
                var _query = @"SELECT Id,
                                   Descripcion,
                                   IdPGeneral AS IdPgeneral,
                                   IdParametroSEO AS IdParametroSeo,
                                   Estado,
                                   UsuarioCreacion,
                                   UsuarioModificacion,
                                   FechaCreacion,
                                   FechaModificacion,
                                   RowVersion,
                                   IdMigracion
                            FROM pla.T_PGeneralParametroSEO_PW
                            WHERE Estado = 1
                                  AND Id = @Id;";
                var respuestaDapper = _dapperRepository.FirstOrDefault(_query, new { Id = id });
                if (!string.IsNullOrEmpty(respuestaDapper) && !respuestaDapper.Equals("null"))
                {
                    return JsonConvert.DeserializeObject<PgeneralParametroSeoPw>(respuestaDapper);
                }
                return null;
            }
            catch (Exception Ex)
            {
                throw new Exception(Ex.Message);
            }
        }
    }
}
