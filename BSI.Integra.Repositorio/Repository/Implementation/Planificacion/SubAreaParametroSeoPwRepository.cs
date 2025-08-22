using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: AreaCentroCostoRepository
    /// Autor Creacion: Gilmer Qm.
    /// Fecha: 09/10/2023
    /// <summary>
    /// Gestión general de T_SubAreaParametroSeoPw
    /// </summary>
    public class SubAreaParametroSeoPwRepository : GenericRepository<TSubAreaParametroSeoPw>, ISubAreaParametroSeoPwRepository
    {
        private Mapper _mapper;
        public SubAreaParametroSeoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSubAreaParametroSeoPw, SubAreaParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TSubAreaParametroSeoPw MapeoEntidad(SubAreaParametroSeoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TSubAreaParametroSeoPw modelo = _mapper.Map<TSubAreaParametroSeoPw>(entidad);

                //mapea los hijos
                //if (entidad.SubAreaParametroSeoPws != null && entidad.SubAreaParametroSeoPws.Count > 0)
                //{
                //    var SubAreaParametroSeoPws = _mapper.Map<List<TSubAreaParametroSeoPw>>(entidad.SubAreaParametroSeoPws);
                //    foreach (var SubAreaParametroSeoPw in SubAreaParametroSeoPws)
                //    {
                //        modelo.TSubAreaParametroSeoPws.Add(SubAreaParametroSeoPw);
                //    }
                //}

                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubAreaParametroSeoPw Add(SubAreaParametroSeoPw entidad)
        {
            try
            {
                var SubAreaParametroSeoPw = MapeoEntidad(entidad);
                base.Insert(SubAreaParametroSeoPw);
                return SubAreaParametroSeoPw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TSubAreaParametroSeoPw Update(SubAreaParametroSeoPw entidad)
        {
            try
            {
                var SubAreaParametroSeoPw = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                SubAreaParametroSeoPw.RowVersion = entidadExistente.RowVersion;

                base.Update(SubAreaParametroSeoPw);
                return SubAreaParametroSeoPw;
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


        public IEnumerable<TSubAreaParametroSeoPw> Add(IEnumerable<SubAreaParametroSeoPw> listadoEntidad)
        {
            try
            {
                List<TSubAreaParametroSeoPw> listado = new List<TSubAreaParametroSeoPw>();
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

        public IEnumerable<TSubAreaParametroSeoPw> Update(IEnumerable<SubAreaParametroSeoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TSubAreaParametroSeoPw> listado = new List<TSubAreaParametroSeoPw>();
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
        /// Autor: Gilmer Qm
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Verifica si existe un dato con los parametros enviados
        /// </summary>
        /// <param name="idParametroSeoPw"> Primary key de T_ParametroSeoPw </param> 
        /// <param name="idSubAreaCapacitacion"> Primary key de T_SubAreaCapacitacion </param> 
        /// <returns> bool </returns> 
        public bool ExistePorIdParametroSeoPwIdSubAreaCapacitacion(int idParametroSeoPw, int idSubAreaCapacitacion)
        {
            try
            {
                var query = @"SELECT Id FROM pla.T_SubAreaParametroSeo_Pw WHERE IdParametroSeoPw = @IdParametroSeoPw AND IdSubAreaCapacitacion =@IdSubAreaCapacitacion AND Estado =1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdParametroSeoPw = idParametroSeoPw, IdSubAreaCapacitacion = idSubAreaCapacitacion });
                return (!string.IsNullOrEmpty(resultado) && resultado != "null");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene el dato por los parametros enviados
        /// </summary>
        /// <param name="idParametroSeoPw"> Primary key de T_ParametroSeoPw </param> 
        /// <param name="idSubAreaCapacitacion"> Primary key de T_SubAreaCapacitacion </param> 
        /// <returns> SubAreaParametroSeoPw </returns> 
        public SubAreaParametroSeoPw ObtenerPorIdParametroSeoPwIdSubAreaCapacitacion(int idParametroSeoPw, int idSubAreaCapacitacion)
        {
            try
            {
                var subAreaParametroSeoPw = new SubAreaParametroSeoPw();
                var query = @"
                    SELECT Id,
                            Descripcion,
                            IdSubAreaCapacitacion,
                            IdParametroSeoPw,
                            Estado,
                            UsuarioCreacion,
                            UsuarioModificacion,
                            FechaCreacion,
                            FechaModificacion,
                            RowVersion,
                            IdMigracion
                    FROM pla.T_SubAreaParametroSeo_Pw
                    WHERE IdParametroSeoPw = @IdParametroSeoPw
                            AND IdSubAreaCapacitacion = @IdSubAreaCapacitacion
                            AND Estado = 1;";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdParametroSeoPw = idParametroSeoPw, IdSubAreaCapacitacion = idSubAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    subAreaParametroSeoPw = JsonConvert.DeserializeObject<SubAreaParametroSeoPw>(resultado)!;
                }
                return subAreaParametroSeoPw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm
        /// Fecha: 09/05/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos asociados al idSubAreaCapacitacion
        /// </summary> 
        /// <param name="idSubAreaCapacitacion"> Primary key de T_SubAreaCapacitacion </param> 
        /// <returns> SubAreaParametroSeoPw </returns> 
        public IEnumerable<SubAreaParametroSeoPw> ObtenerPorIdSubAreaCapacitacion(int idSubAreaCapacitacion)
        {
            IEnumerable<SubAreaParametroSeoPw> subAreaParametroSeoPw = new List<SubAreaParametroSeoPw>();
            try
            {
                var query = @"SELECT Id,
                                       Descripcion,
                                       IdSubAreaCapacitacion,
                                       IdParametroSeoPw,
                                       Estado,
                                       UsuarioCreacion,
                                       UsuarioModificacion,
                                       FechaCreacion,
                                       FechaModificacion,
                                       RowVersion,
                                       IdMigracion
                                FROM pla.T_SubAreaParametroSeo_Pw
                                WHERE IdSubAreaCapacitacion = @IdSubAreaCapacitacion
                                      AND Estado = 1;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdSubAreaCapacitacion = idSubAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    subAreaParametroSeoPw = JsonConvert.DeserializeObject<IEnumerable<SubAreaParametroSeoPw>>(resultado)!;

                }
                return subAreaParametroSeoPw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Gilmer Qm.
        /// Fecha: 02/05/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el informacion contenido de ParametroSeoPw por el IdSubAreaCapacitacion
        /// </summary> 
        /// <returns> List<ParametroContenidoDTO> </returns>
        public IEnumerable<ParametroContenidoDTO> ObtenerParametroContenidoPorIdSubAreaCapacitacion(int idSubAreaCapacitacion)
        {
            try
            {
                IEnumerable<ParametroContenidoDTO> rpta = new List<ParametroContenidoDTO>();
                string query = @"SELECT Id,
                                           Nombre,
                                           NumeroCaracteres,
                                           Contenido
                                    FROM pla.V_ObtenerSubAreaParametrosSeoPorIdSubAreaCapacitacion
                                    WHERE IdSubAreaCapacitacion = @IdSubAreaCapacitacion
                                          AND EstadoSubAreaParametroSeoPW = 1
                                          AND EstadoParametroSeoPW = 1;";
                var resultado = _dapperRepository.QueryDapper(query, new { IdSubAreaCapacitacion = idSubAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<ParametroContenidoDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception e)
            {
                throw new Exception($"Error en ObtenerParametroContenidoPorIdSubAreaCapacitacion(): {e.Message}");
            }
        }
    }
}
