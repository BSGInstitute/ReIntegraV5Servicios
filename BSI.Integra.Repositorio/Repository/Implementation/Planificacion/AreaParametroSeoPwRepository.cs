using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: CargoRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 14/07/2022
    /// <summary>
    /// Gestión general de T_Cargo
    /// </summary>0
    public class AreaParametroSeoPwRepository : GenericRepository<TAreaParametroSeoPw>, IAreaParametroSeoPwRepository
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public AreaParametroSeoPwRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TAreaParametroSeoPw, AreaParametroSeoPw>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TAreaParametroSeoPw MapeoEntidad(AreaParametroSeoPw entidad)
        {
            try
            {
                //crea la entidad padre
                TAreaParametroSeoPw modelo = _mapper.Map<TAreaParametroSeoPw>(entidad);

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

        public TAreaParametroSeoPw Add(AreaParametroSeoPw entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                base.Insert(Cargo);
                return Cargo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TAreaParametroSeoPw Update(AreaParametroSeoPw entidad)
        {
            try
            {
                var Cargo = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                Cargo.RowVersion = entidadExistente.RowVersion;

                base.Update(Cargo);
                return Cargo;
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


        public IEnumerable<TAreaParametroSeoPw> Add(IEnumerable<AreaParametroSeoPw> listadoEntidad)
        {
            try
            {
                List<TAreaParametroSeoPw> listado = new List<TAreaParametroSeoPw>();
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

        public IEnumerable<TAreaParametroSeoPw> Update(IEnumerable<AreaParametroSeoPw> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TAreaParametroSeoPw> listado = new List<TAreaParametroSeoPw>();
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

        /// <summary>
        /// Obtiene registros por el IdTag
        /// </summary>
        /// <param name="idTag"></param>
        /// <returns></returns>
        public IEnumerable<AreaParametrosSeoPorIdAreaDTO> ObtenerAreaParametrosSeoPorIdArea(int idTag)
        {
            try
            {
                IEnumerable<AreaParametrosSeoPorIdAreaDTO> rpta = new List<AreaParametrosSeoPorIdAreaDTO>();
                var query = @"
                            SELECT Id, NombreParametroSeo as Nombre, NumeroCaracteresParametrosSeo as NumeroCaracteres, ContenidoParametroSeo  as Contenido
                            FROM pla.V_ObtenerAreaParametrosSeoPorIdArea 
                            WHERE IdAreaCapacitacion = @IdTag AND  EstadoAreaParametroSeoPW = 1 AND EstadoParametroSeoPW = 1 ";
                var resultado = _dapperRepository.QueryDapper(query, new { IdTag = idTag });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<AreaParametrosSeoPorIdAreaDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerTodoPorIdTag()", ex);
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public List<AreaParametroSeoPw> ObtenerPorId(int id)
        {
            try
            {
                List<AreaParametroSeoPw> rpta = new List<AreaParametroSeoPw>();
                var query = @"
                    SELECT
	                    Id,Descripcion as Nombre,IdAreaCapacitacion,IdParametroSEOPW
	                    ,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion,RowVersion
                    FROM pla.T_AreaParametroSEO_PW
                    WHERE Estado = 1 AND IdAreaCapacitacion=@Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<List<AreaParametroSeoPw>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception("Se ha producido un error al ejecutar el método ObtenerPorId()", ex);

            }
        }

        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 10/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_MaterialAccion.
        /// </summary>
        /// <returns> List<MaterialAccionDTO> </returns>
        public IEnumerable<AreaParametroSeoPw> ObtenerPorIdAreaCapacitacion(int idAreaCapacitacion)
        {
            try
            {
                IEnumerable<AreaParametroSeoPw> rpta = new List<AreaParametroSeoPw>();
                var query = @"
                    SELECT
	                    Id,Descripcion,IdAreaCapacitacion,IdParametroSEOPW
	                    ,FechaCreacion,FechaModificacion,UsuarioCreacion,UsuarioModificacion,RowVersion
                    FROM pla.T_AreaParametroSEO_PW
                    WHERE Estado = 1 AND IdAreaCapacitacion=@Id";
                var resultado = _dapperRepository.QueryDapper(query, new { Id = idAreaCapacitacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    rpta = JsonConvert.DeserializeObject<IEnumerable<AreaParametroSeoPw>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw new Exception($"Se ha producido un error en ObtenerPorId() : {ex.Message}", ex);

            }
        }


    }
}
