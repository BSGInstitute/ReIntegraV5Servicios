using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ConfiguracionBeneficioProgramaGeneralPaisRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 02/08/2023
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneralPais
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralPaisRepository : GenericRepository<TConfiguracionBeneficioProgramaGeneralPai>, IConfiguracionBeneficioProgramaGeneralPaisRepository
    {
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralPaisRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneralPai, ConfiguracionBeneficioProgramaGeneralPais>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionBeneficioProgramaGeneralPai MapeoEntidad(ConfiguracionBeneficioProgramaGeneralPais entidad)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralPai modelo = _mapper.Map<TConfiguracionBeneficioProgramaGeneralPai>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionBeneficioProgramaGeneralPai Add(ConfiguracionBeneficioProgramaGeneralPais entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralPais = MapeoEntidad(entidad);
                base.Insert(ConfiguracionBeneficioProgramaGeneralPais);
                return ConfiguracionBeneficioProgramaGeneralPais;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionBeneficioProgramaGeneralPai Update(ConfiguracionBeneficioProgramaGeneralPais entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralPais = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionBeneficioProgramaGeneralPais.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionBeneficioProgramaGeneralPais);
                return ConfiguracionBeneficioProgramaGeneralPais;
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
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralPai> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralPais> listadoEntidad)
        {
            try
            {
                List<TConfiguracionBeneficioProgramaGeneralPai> listado = new List<TConfiguracionBeneficioProgramaGeneralPai>();
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
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralPai> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralPais> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionBeneficioProgramaGeneralPai> listado = new List<TConfiguracionBeneficioProgramaGeneralPai>();
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
        /// Obtiene ConfiguracionBeneficioProgramaGeneralPais por id.
        /// </summary>
        /// <returns> ConfiguracionBeneficioProgramaGeneralPais </returns>
        public ConfiguracionBeneficioProgramaGeneralPais? ObtenerPorId(int id)
        {
            try
            {
                ConfiguracionBeneficioProgramaGeneralPais rpta = new();
                var query = @"
                   SELECT 
	                    Id,
	                    IdConfiguracionBeneficioPGneral AS IdConfiguracionBeneficioPgneral,
	                    IdPais,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ConfiguracionBeneficioProgramaGeneralPais 
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionBeneficioProgramaGeneralPais>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 02/08/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionBeneficioProgramaGeneralPais por id.
        /// </summary>
        /// <returns> ConfiguracionBeneficioProgramaGeneralPais </returns>
        public List<ConfiguracionBeneficioProgramaGeneralPais> ObtenerPorIdConfiguracionBeneficioPGneral(int idConfiguracionBeneficioPGneral)
        {
            try
            {
                ConfiguracionBeneficioProgramaGeneralPais rpta = new();
                var query = @"
                   SELECT 
	                    Id,
	                    IdConfiguracionBeneficioPGneral AS IdConfiguracionBeneficioPgneral,
	                    IdPais,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ConfiguracionBeneficioProgramaGeneralPais 
                    WHERE Estado=1 AND IdConfiguracionBeneficioPGneral=@idConfiguracionBeneficioPGneral";
                var resultado = _dapperRepository.QueryDapper(query, new { idConfiguracionBeneficioPGneral });
                if (!string.IsNullOrEmpty(resultado) && resultado != "[]")
                {
                    return JsonConvert.DeserializeObject<List<ConfiguracionBeneficioProgramaGeneralPais>>(resultado)!;
                }
                return new List<ConfiguracionBeneficioProgramaGeneralPais>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



