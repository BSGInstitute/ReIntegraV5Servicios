using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.GestionPersonas;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.GestionPersonas
{
    public class ConfiguracionAsignacionExamenRepository : GenericRepository<TConfiguracionAsignacionExaman>, IConfiguracionAsignacionExamenRepository
    {
        private Mapper _mapper;
        public ConfiguracionAsignacionExamenRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAsignacionExaman, ConfiguracionAsignacionExamen>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionExamen, ConfiguracionAsignacionExamenDTO>(MemberList.None).ReverseMap();
                cfg.CreateMap<ConfiguracionAsignacionExamen, TConfiguracionAsignacionExaman>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionAsignacionExaman MapeoEntidad(ConfiguracionAsignacionExamen entidad)
        {
            try
            {
                TConfiguracionAsignacionExaman modelo = _mapper.Map<TConfiguracionAsignacionExaman>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionAsignacionExaman Add(ConfiguracionAsignacionExamen entidad)
        {
            try
            {
                var ConfiguracionAsignacionExamen = MapeoEntidad(entidad);
                base.Insert(ConfiguracionAsignacionExamen);
                return ConfiguracionAsignacionExamen;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionAsignacionExaman Update(ConfiguracionAsignacionExamen entidad)
        {
            try
            {
                var ConfiguracionAsignacionExamen = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionAsignacionExamen.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionAsignacionExamen);
                return ConfiguracionAsignacionExamen;
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
        public IEnumerable<TConfiguracionAsignacionExaman> Add(IEnumerable<ConfiguracionAsignacionExamen> listadoEntidad)
        {
            try
            {
                List<TConfiguracionAsignacionExaman> listado = new List<TConfiguracionAsignacionExaman>();
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
        public IEnumerable<TConfiguracionAsignacionExaman> Update(IEnumerable<ConfiguracionAsignacionExamen> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionAsignacionExaman> listado = new List<TConfiguracionAsignacionExaman>();
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
        /// Obtiene un registro de T_ConfiguracionAsignacionExamen por el Primary Key
        /// </summary>
        /// <returns>ConfiguracionAsignacionExamen o Nulo</returns>
        public ConfiguracionAsignacionExamen? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                    SELECT Id,
	                    IdPersonal,
	                    IdPostulante,
	                    IdExamen,
	                    IdProcesoSeleccion,
	                    EstadoExamen,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion
                    FROM gp.T_ConfiguracionAsignacionExamen
                    WHERE Id = @Id AND Estado = 1";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionAsignacionExamen>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }


        public List<ConfiguracionAsignacionExamen>? ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                var query = @"
                    SELECT Id,
                        IdExamen,
                        IdProcesoSeleccion,
                        Estado,
                        UsuarioCreacion,
                        UsuarioModificacion,
                        FechaCreacion,
                        FechaModificacion,
                        RowVersion,
                        IdMigracion
                    FROM gp.T_ConfiguracionAsignacionExamen
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionAsignacionExamen>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public List<int>? ObtenerPorIdsProcesoSeleccion(int idProcesoSeleccion)
        {
            try
            {
                var query = @"
                    SELECT Id,
                    FROM gp.T_ConfiguracionAsignacionExamen
                    WHERE IdProcesoSeleccion = @IdProcesoSeleccion AND Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, new { IdProcesoSeleccion = idProcesoSeleccion });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<int>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
        public List<NombreEvaluacionAgrupadaComponenteDTO2>? obtenerComponentesEvaluacion()
        {
            try
            {
                var query = @"
                    SELECT IdExamenTest,NombreEvaluacion,IdGrupo,NombreGrupo,IdExamen,NombreComponente
                    FROM [conf].[V_ObtenerEvaluacionComponente]";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<List<NombreEvaluacionAgrupadaComponenteDTO2>>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPorId(), {ex.Message}");
            }
        }
    }
}
