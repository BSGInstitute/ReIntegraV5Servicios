using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ConfiguracionBeneficioProgramaGeneralVersionRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 10/06/2023
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneralVersion
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralVersionRepository : GenericRepository<TConfiguracionBeneficioProgramaGeneralVersion>, IConfiguracionBeneficioProgramaGeneralVersionRepository
    {
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralVersionRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneralVersion, ConfiguracionBeneficioProgramaGeneralVersion>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        private TConfiguracionBeneficioProgramaGeneralVersion MapeoEntidad(ConfiguracionBeneficioProgramaGeneralVersion entidad)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralVersion modelo = _mapper.Map<TConfiguracionBeneficioProgramaGeneralVersion>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionBeneficioProgramaGeneralVersion Add(ConfiguracionBeneficioProgramaGeneralVersion entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralVersion = MapeoEntidad(entidad);
                base.Insert(ConfiguracionBeneficioProgramaGeneralVersion);
                return ConfiguracionBeneficioProgramaGeneralVersion;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionBeneficioProgramaGeneralVersion Update(ConfiguracionBeneficioProgramaGeneralVersion entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralVersion = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionBeneficioProgramaGeneralVersion.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionBeneficioProgramaGeneralVersion);
                return ConfiguracionBeneficioProgramaGeneralVersion;
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


        public IEnumerable<TConfiguracionBeneficioProgramaGeneralVersion> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralVersion> listadoEntidad)
        {
            try
            {
                List<TConfiguracionBeneficioProgramaGeneralVersion> listado = new List<TConfiguracionBeneficioProgramaGeneralVersion>();
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

        public IEnumerable<TConfiguracionBeneficioProgramaGeneralVersion> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralVersion> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionBeneficioProgramaGeneralVersion> listado = new List<TConfiguracionBeneficioProgramaGeneralVersion>();
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
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// </summary>
        /// <returns> ConfiguracionBeneficioProgramaGeneralVersion </returns>
        public ConfiguracionBeneficioProgramaGeneralVersion? ObtenerPorId(int id)
        {
            try
            {
                var query = @"
                   SELECT 
	                    Id,
	                    IdConfiguracionBeneficioPGneral,
	                    IdVersionPrograma,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ConfiguracionBeneficioProgramaGeneralVersion
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionBeneficioProgramaGeneralVersion>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Gretel Canasa.
        /// Fecha: 10/06/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionBeneficioProgramaGeneralVersion.
        /// </summary>
        /// <returns> List<ConfiguracionBeneficioProgramaGeneralVersionDTO> </returns>
        public List<ConfiguracionBeneficioProgramaGeneralVersion> ObtenerPorIds(List<int> id)
        {
            try
            {
                List<ConfiguracionBeneficioProgramaGeneralVersion> rpta = new();
                var query = @"
SELECT  C.Id,C.Nombre,C.IdPais,CP.Id AS IdCiudad,C.Direccion,C.Telefono,C.Url,NombrePais AS Pais, CP.Nombre AS Ciudad FROM pla.T_ConfiguracionBeneficioProgramaGeneralVersion AS C INNER JOIN conf.T_Pais AS P ON P.id=C.IdPais INNER JOIN conf.T_Ciudad AS CP ON C.IdCiudad=CP.id WHERE C.Estado = 1 AND P.Estado=1 AND CP.Estado=1 AND Id=@id";
                var resultado = _dapperRepository.QueryDapper(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralVersion>>(resultado);
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



