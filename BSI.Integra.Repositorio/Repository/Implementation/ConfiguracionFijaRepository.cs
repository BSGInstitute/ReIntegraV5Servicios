using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionFijaRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 02/08/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionFija
    /// </summary>
    public class ConfiguracionFijaRepository : GenericRepository<TConfiguracionFija>, IConfiguracionFijaRepository
    {
        private Mapper _mapper;

        public ConfiguracionFijaRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionFija, ConfiguracionFija>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionFija MapeoEntidad(ConfiguracionFija entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionFija modelo = _mapper.Map<TConfiguracionFija>(entidad);

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

        public TConfiguracionFija Add(ConfiguracionFija entidad)
        {
            try
            {
                var ConfiguracionFija = MapeoEntidad(entidad);
                base.Insert(ConfiguracionFija);
                return ConfiguracionFija;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionFija Update(ConfiguracionFija entidad)
        {
            try
            {
                var ConfiguracionFija = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionFija.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionFija);
                return ConfiguracionFija;
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


        public IEnumerable<TConfiguracionFija> Add(IEnumerable<ConfiguracionFija> listadoEntidad)
        {
            try
            {
                List<TConfiguracionFija> listado = new List<TConfiguracionFija>();
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

        public IEnumerable<TConfiguracionFija> Update(IEnumerable<ConfiguracionFija> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionFija> listado = new List<TConfiguracionFija>();
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
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_ConfiguracionFija.
        /// </summary>
        /// <returns> List<ConfiguracionFijaDTO> </returns>
        public IEnumerable<ConfiguracionFijaDTO> ObtenerConfiguracionFija()
        {
            try
            {
                List<ConfiguracionFijaDTO> rpta = new List<ConfiguracionFijaDTO>();
                var query = @"
                    SELECT
	                    Id,
	                    Codigo,
	                    NombreTabla,
	                    IdTabla,
	                    NombreColumna,
	                    TipoDato,
	                    Valor,
	                    UsuarioCreacion,
	                    UsuarioModificacion,
	                    FechaCreacion,
	                    FechaModificacion
                    FROM conf.T_ConfiguracionFija
                    WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionFijaDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 02/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_ConfiguracionFija para mostrarse en combo.
        /// </summary>
        /// <returns> List<ConfiguracionFijaComboDTO> </returns>
        public IEnumerable<ConfiguracionFijaComboDTO> ObtenerCombo()
        {
            try
            {
                List<ConfiguracionFijaComboDTO> rpta = new List<ConfiguracionFijaComboDTO>();
                var query = @"SELECT Id,Codigo FROM conf.T_ConfiguracionFija WHERE Estado = 1";
                var resultado = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionFijaComboDTO>>(resultado);
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jashin Salazar Taco
        /// Fecha: 10/11/2022
        /// Version: 1.0
        /// <summary>
        /// Retorna los registros de valor estatico
        /// </summary>
        /// <returns></returns>
        public List<ValorEstaticoDTO> ObtenerTodosLosRegistros()
        {
            List<ValorEstaticoDTO> harcodeos = new List<ValorEstaticoDTO>();
            var _query = string.Empty;
            _query = "SELECT NombreAtributo,Valor,TipoDato FROM conf.V_ConfiguracionFija_Todo Where Estado = 1";
            var _errores = _dapperRepository.QueryDapper(_query, new { });
            harcodeos = JsonConvert.DeserializeObject<List<ValorEstaticoDTO>>(_errores);
            return harcodeos;
        }
    }
}
