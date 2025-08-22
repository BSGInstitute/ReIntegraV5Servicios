using AutoMapper;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 02/08/2023
    /// <summary>
    /// Gestión general de T_ConfiguracionBeneficioProgramaGeneralDatoAdicional
    /// </summary>
    public class ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository : GenericRepository<TConfiguracionBeneficioProgramaGeneralDatoAdicional>, IConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository
    {
        private Mapper _mapper;

        public ConfiguracionBeneficioProgramaGeneralDatoAdicionalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionBeneficioProgramaGeneralDatoAdicional, ConfiguracionBeneficioProgramaGeneralDatoAdicional>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionBeneficioProgramaGeneralDatoAdicional MapeoEntidad(ConfiguracionBeneficioProgramaGeneralDatoAdicional entidad)
        {
            try
            {
                TConfiguracionBeneficioProgramaGeneralDatoAdicional modelo = _mapper.Map<TConfiguracionBeneficioProgramaGeneralDatoAdicional>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionBeneficioProgramaGeneralDatoAdicional Add(ConfiguracionBeneficioProgramaGeneralDatoAdicional entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralDatoAdicional = MapeoEntidad(entidad);
                base.Insert(ConfiguracionBeneficioProgramaGeneralDatoAdicional);
                return ConfiguracionBeneficioProgramaGeneralDatoAdicional;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TConfiguracionBeneficioProgramaGeneralDatoAdicional Update(ConfiguracionBeneficioProgramaGeneralDatoAdicional entidad)
        {
            try
            {
                var ConfiguracionBeneficioProgramaGeneralDatoAdicional = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionBeneficioProgramaGeneralDatoAdicional.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionBeneficioProgramaGeneralDatoAdicional);
                return ConfiguracionBeneficioProgramaGeneralDatoAdicional;
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
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralDatoAdicional> Add(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicional> listadoEntidad)
        {
            try
            {
                List<TConfiguracionBeneficioProgramaGeneralDatoAdicional> listado = new List<TConfiguracionBeneficioProgramaGeneralDatoAdicional>();
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
        public IEnumerable<TConfiguracionBeneficioProgramaGeneralDatoAdicional> Update(IEnumerable<ConfiguracionBeneficioProgramaGeneralDatoAdicional> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionBeneficioProgramaGeneralDatoAdicional> listado = new List<TConfiguracionBeneficioProgramaGeneralDatoAdicional>();
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
        /// <returns> ConfiguracionBeneficioProgramaGeneralDatoAdicional </returns>
        public ConfiguracionBeneficioProgramaGeneralDatoAdicional ObtenerPorId(int id)
        {
            try
            {
                ConfiguracionBeneficioProgramaGeneralDatoAdicional rpta = new();
                var query = @"
                   SELECT 
	                    Id,
	                    IdConfiguracionBeneficioPGeneral AS IdConfiguracionBeneficioPgeneral,
	                    IdBeneficioDatoAdicional,
	                    Estado,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion,
	                    RowVersion,
	                    IdMigracion 
                    FROM pla.T_ConfiguracionBeneficioProgramaGeneralDatoAdicional 
                    WHERE Estado=1 AND Id=@id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionBeneficioProgramaGeneralDatoAdicional>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}



