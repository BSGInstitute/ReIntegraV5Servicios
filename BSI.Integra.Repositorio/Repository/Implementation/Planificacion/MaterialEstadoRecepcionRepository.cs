using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: MaterialEstadoRecepcionRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 16/08/2023
    /// <summary>
    /// Gestión general de T_MaterialEstadoRecepcion
    /// </summary>
    public class MaterialEstadoRecepcionRepository : GenericRepository<TMaterialEstadoRecepcion>, IMaterialEstadoRecepcionRepository
    {
        private Mapper _mapper;

        public MaterialEstadoRecepcionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TMaterialEstadoRecepcion, MaterialEstadoRecepcion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TMaterialEstadoRecepcion MapeoEntidad(MaterialEstadoRecepcion entidad)
        {
            try
            {
                TMaterialEstadoRecepcion modelo = _mapper.Map<TMaterialEstadoRecepcion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEstadoRecepcion Add(MaterialEstadoRecepcion entidad)
        {
            try
            {
                var MaterialEstadoRecepcion = MapeoEntidad(entidad);
                base.Insert(MaterialEstadoRecepcion);
                return MaterialEstadoRecepcion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TMaterialEstadoRecepcion Update(MaterialEstadoRecepcion entidad)
        {
            try
            {
                var MaterialEstadoRecepcion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                MaterialEstadoRecepcion.RowVersion = entidadExistente.RowVersion;

                base.Update(MaterialEstadoRecepcion);
                return MaterialEstadoRecepcion;
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
        public IEnumerable<TMaterialEstadoRecepcion> Add(IEnumerable<MaterialEstadoRecepcion> listadoEntidad)
        {
            try
            {
                List<TMaterialEstadoRecepcion> listado = new List<TMaterialEstadoRecepcion>();
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
        public IEnumerable<TMaterialEstadoRecepcion> Update(IEnumerable<MaterialEstadoRecepcion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TMaterialEstadoRecepcion> listado = new List<TMaterialEstadoRecepcion>();
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
        /// Obtiene MaterialEstadoRecepcion por id.
        /// </summary>
        /// <returns>MaterialEstadoRecepcion</returns>
        public MaterialEstadoRecepcion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    Nombre,
	                    Descripcion,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM ope.T_MaterialEstadoRecepcion
                    WHERE Estado = 1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<MaterialEstadoRecepcion>(resultado)!;
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



