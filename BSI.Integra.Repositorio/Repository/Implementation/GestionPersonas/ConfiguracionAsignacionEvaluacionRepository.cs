using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ConfiguracionAsignacionEvaluacionRepository : GenericRepository<TConfiguracionAsignacionEvaluacion>, IConfiguracionAsignacionEvaluacionRepository
    {
        private Mapper _mapper;
        public ConfiguracionAsignacionEvaluacionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacion>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionEvaluacion, ConfiguracionAsignacionEvaluacionDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionEvaluacion, TConfiguracionAsignacionEvaluacion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionAsignacionEvaluacion MapeoEntidad(ConfiguracionAsignacionEvaluacion entidad)
        {
            try
            {
                TConfiguracionAsignacionEvaluacion modelo = _mapper.Map<TConfiguracionAsignacionEvaluacion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionAsignacionEvaluacion Add(ConfiguracionAsignacionEvaluacion entidad)
        {
            try
            {
                var ConfiguracionAsignacionEvaluacion = MapeoEntidad(entidad);
                base.Insert(ConfiguracionAsignacionEvaluacion);
                return ConfiguracionAsignacionEvaluacion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionAsignacionEvaluacion Update(ConfiguracionAsignacionEvaluacion entidad)
        {
            try
            {
                var ConfiguracionAsignacionEvaluacion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionAsignacionEvaluacion.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionAsignacionEvaluacion);
                return ConfiguracionAsignacionEvaluacion;
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
        public IEnumerable<TConfiguracionAsignacionEvaluacion> Add(IEnumerable<ConfiguracionAsignacionEvaluacion> listadoEntidad)
        {
            try
            {
                List<TConfiguracionAsignacionEvaluacion> listado = new List<TConfiguracionAsignacionEvaluacion>();
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
        public IEnumerable<TConfiguracionAsignacionEvaluacion> Update(IEnumerable<ConfiguracionAsignacionEvaluacion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionAsignacionEvaluacion> listado = new List<TConfiguracionAsignacionEvaluacion>();
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

        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_ConfiguracionAsignacionEvaluacion por el Primary Key
        /// </summary>
        /// <returns>ConfiguracionAsignacionEvaluacion o Nulo</returns>
        public ConfiguracionAsignacionEvaluacion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdProcesoSeleccion,
		                IdEvaluacion,
		                NroOrden,
		                IdProcesoSeleccionEtapa,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_ConfiguracionAsignacionEvaluacion
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionAsignacionEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        /// Autor: Flavio R.M.F.
        /// Fecha: 04/06/2024
        /// <param name="id"> (PK) </param> 
        /// <summary>
        /// Obtiene un registro de T_ConfiguracionAsignacionEvaluacion por el Primary Key
        /// </summary>
        /// <returns>ConfiguracionAsignacionEvaluacion o Nulo</returns>
        public ConfiguracionAsignacionEvaluacion? ObtenerPorIdProcesoSeleccionIdEvaluacion(int idProcesoSeleccion, int idEvaluacion)
        {
            try
            {
                var query = @"
                    SELECT
	                    Id,
	                    IdProcesoSeleccion,
	                    IdEvaluacion,
	                    NroOrden,
	                    IdProcesoSeleccionEtapa,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ConfiguracionAsignacionEvaluacion
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND IdEvaluacion = @IdEvaluacion AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdProcesoSeleccion = idProcesoSeleccion, IdEvaluacion = idEvaluacion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionAsignacionEvaluacion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }



        public List<ConfiguracionAsignacionEvaluacion>? ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                var query = @"
                    SELECT Id,
		                IdProcesoSeleccion,
		                IdEvaluacion,
		                NroOrden,
		                IdProcesoSeleccionEtapa,
		                Estado,
		                UsuarioCreacion,
		                UsuarioModificacion,
		                FechaCreacion,
		                FechaModificacion,
		                RowVersion,
		                IdMigracion
                    FROM gp.T_ConfiguracionAsignacionEvaluacion
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionEvaluacion>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorIdProcesoSeleccion(), {ex.Message}");
            }
        }

    }
}
