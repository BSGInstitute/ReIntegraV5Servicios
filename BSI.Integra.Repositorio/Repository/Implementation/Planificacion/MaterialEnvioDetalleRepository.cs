using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialEnvioDetalleRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_MaterialEnvioDetalle
    /// </summary>
    public class MaterialEnvioDetalleRepository : GenericRepository<TMaterialEnvioDetalle>, IMaterialEnvioDetalleRepository
    {
        private Mapper _mapper;

        public MaterialEnvioDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialEnvioDetalle, MaterialEnvioDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialEnvioDetalle MapeoEntidad(MaterialEnvioDetalle entidad)
        {
            try
            {
                TMaterialEnvioDetalle modelo = _mapper.Map<TMaterialEnvioDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEnvioDetalle Add(MaterialEnvioDetalle entidad)
        {
            try
            {
                var MaterialEnvioDetalle = MapeoEntidad(entidad);
                base.Insert(MaterialEnvioDetalle);
                return MaterialEnvioDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEnvioDetalle Update(MaterialEnvioDetalle entidad)
        {
            try
            {
                var MaterialEnvioDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialEnvioDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialEnvioDetalle);
                return MaterialEnvioDetalle;
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
        public IEnumerable<TMaterialEnvioDetalle> Add(IEnumerable<MaterialEnvioDetalle> listadoEntidad)
        {
            try
            {
                List<TMaterialEnvioDetalle> listado = new List<TMaterialEnvioDetalle>();
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
        public IEnumerable<TMaterialEnvioDetalle> Update(IEnumerable<MaterialEnvioDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialEnvioDetalle> listado = new List<TMaterialEnvioDetalle>();
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
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 16/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene MaterialEnvioDetalle por id.
        /// </summary>
        /// <returns>MaterialEnvioDetalle</returns>
        public MaterialEnvioDetalle? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdMaterialEnvio,
	                    IdMaterialVersion,
	                    IdMaterialEstadoRecepcion,
	                    IdPersonal_Receptor AS IdPersonalReceptor,
	                    CantidadEnvio,
	                    CantidadRecepcion,
	                    ComentarioEnvio,
	                    ComentarioRecepcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_MaterialEnvioDetalle
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MaterialEnvioDetalle>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"#FR-OPI-001@Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}



