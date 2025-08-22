using AutoMapper;
using BSI.Integra.Aplicacion.Base.Enums;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionAccesoPersonalRepository
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 23/07/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionAccesoPersonal
    /// </summary>
    public class ConfiguracionAccesoPersonalRepository : GenericRepository<TConfiguracionAccesoPersonal>, IConfiguracionAccesoPersonalRepository
    {
        private Mapper _mapper;

        public ConfiguracionAccesoPersonalRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionAccesoPersonal, ConfiguracionAccesoPersonal>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionAccesoPersonal MapeoEntidad(ConfiguracionAccesoPersonal entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionAccesoPersonal modelo = _mapper.Map<TConfiguracionAccesoPersonal>(entidad);

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

        public TConfiguracionAccesoPersonal Add(ConfiguracionAccesoPersonal entidad)
        {
            try
            {
                var ConfiguracionAccesoPersonal = MapeoEntidad(entidad);
                base.Insert(ConfiguracionAccesoPersonal);
                return ConfiguracionAccesoPersonal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionAccesoPersonal Update(ConfiguracionAccesoPersonal entidad)
        {
            try
            {
                var ConfiguracionAccesoPersonal = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionAccesoPersonal.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionAccesoPersonal);
                return ConfiguracionAccesoPersonal;
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


        public IEnumerable<TConfiguracionAccesoPersonal> Add(IEnumerable<ConfiguracionAccesoPersonal> listadoEntidad)
        {
            try
            {
                List<TConfiguracionAccesoPersonal> listado = new List<TConfiguracionAccesoPersonal>();
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

        public IEnumerable<TConfiguracionAccesoPersonal> Update(IEnumerable<ConfiguracionAccesoPersonal> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionAccesoPersonal> listado = new List<TConfiguracionAccesoPersonal>();
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
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public IEnumerable<ConfiguracionAccesoPersonalDTO> ObtenerPorIdPersonal(int idPersonal)
        {
            try
            {
                List<ConfiguracionAccesoPersonalDTO> rpta = new List<ConfiguracionAccesoPersonalDTO>();
                var query = @"
                    SELECT 
                    Id, IdPersonal, IdPersonal_Acceso AS IdPersonalAcceso, FechaExpiracion, IdModuloSistema 
                    FROM conf.T_ConfiguracionAccesoPersonal
                    WHERE Estado=1 AND IdPersonal=@idPersonal";
                var resultado = _dapperRepository.QueryDapper(query, new { idPersonal });
                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    rpta = JsonConvert.DeserializeObject<List<ConfiguracionAccesoPersonalDTO>>(resultado)!;
                }
                return rpta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal idModulo
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idModulo">Id del Modulo</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalIdModulo(int idPersonal, int idModulo)
        {
            try
            {
                var query = @"
                    SELECT Id, IdPersonal, IdPersonal_Acceso AS IdPersonalAcceso, FechaExpiracion, IdModuloSistema 
                    FROM conf.T_ConfiguracionAccesoPersonal WHERE Estado=1 AND IdPersonal=@idPersonal AND IdModuloSistema=@idModulo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal, idModulo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionAccesoPersonalDTO>(resultado)!;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 18/07/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene ConfiguracionAccesoPersonal por idPersonal idModulo
        /// </summary>
        /// <param name="idPersonal">Id del Personal</param>
        /// <param name="idModulo">Id del Modulo</param>
        /// <returns> Lista ConfiguracionAccesoPersonalDTO </returns>
        public ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalUrlModulo(int idPersonal, string urlModulo)
        {
            try
            {
                var query = @"
                    SELECT Id, IdPersonal, IdPersonalAcceso, FechaExpiracion, IdModuloSistema, ModuloSistema, UrlModuloSistema FROM mkt.V_ConfiguracionAccesoPersonal WHERE IdPersonal=@idPersonal AND UrlModuloSistema=@urlModulo";
                var resultado = _dapperRepository.FirstOrDefault(query, new { idPersonal, urlModulo });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ConfiguracionAccesoPersonalDTO>(resultado)!;
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
