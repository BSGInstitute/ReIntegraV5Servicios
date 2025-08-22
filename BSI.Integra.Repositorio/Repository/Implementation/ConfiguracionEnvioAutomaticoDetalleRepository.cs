using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// Repositorio: ConfiguracionEnvioAutomaticoDetalleRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 15/12/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioAutomaticoDetalle
    /// </summary>
    public class ConfiguracionEnvioAutomaticoDetalleRepository : GenericRepository<TConfiguracionEnvioAutomaticoDetalle>, IConfiguracionEnvioAutomaticoDetalleRepository
    {
        private Mapper _mapper;

        public ConfiguracionEnvioAutomaticoDetalleRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalle>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        private TConfiguracionEnvioAutomaticoDetalle MapeoEntidad(ConfiguracionEnvioAutomaticoDetalle entidad)
        {
            try
            {
                //crea la entidad padre
                TConfiguracionEnvioAutomaticoDetalle modelo = _mapper.Map<TConfiguracionEnvioAutomaticoDetalle>(entidad);
                return modelo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionEnvioAutomaticoDetalle Add(ConfiguracionEnvioAutomaticoDetalle entidad)
        {
            try
            {
                var ConfiguracionEnvioAutomaticoDetalle = MapeoEntidad(entidad);
                base.Insert(ConfiguracionEnvioAutomaticoDetalle);
                return ConfiguracionEnvioAutomaticoDetalle;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TConfiguracionEnvioAutomaticoDetalle Update(ConfiguracionEnvioAutomaticoDetalle entidad)
        {
            try
            {
                var ConfiguracionEnvioAutomaticoDetalle = MapeoEntidad(entidad);
                var entidadExistente = base.FirstBy(w => w.Id == entidad.Id, s => new { s.RowVersion });
                ConfiguracionEnvioAutomaticoDetalle.RowVersion = entidadExistente.RowVersion;

                base.Update(ConfiguracionEnvioAutomaticoDetalle);
                return ConfiguracionEnvioAutomaticoDetalle;
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


        public IEnumerable<TConfiguracionEnvioAutomaticoDetalle> Add(IEnumerable<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad)
        {
            try
            {
                List<TConfiguracionEnvioAutomaticoDetalle> listado = new List<TConfiguracionEnvioAutomaticoDetalle>();
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

        public IEnumerable<TConfiguracionEnvioAutomaticoDetalle> Update(IEnumerable<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad)
        {
            try
            {
                if (listadoEntidad == null)
                    throw new ArgumentNullException("El listado es nulo");

                List<TConfiguracionEnvioAutomaticoDetalle> listado = new List<TConfiguracionEnvioAutomaticoDetalle>();
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

        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene centiles asignados a una evaluacion
        /// </summary>
        /// <param name="idExamenTest"></param>
        /// <returns> List<ConfiguracionEnvioAutomaticoDetalleDTO> </returns>
        public List<ConfiguracionEnvioAutomaticoDetalleDTO> ObtenerConfiguracionEnvioAutomaticoDetalle(int idConfiguracionEnvioAutomatico)
        {
            try
            {
                List<ConfiguracionEnvioAutomaticoDetalleDTO> configuracion = new List<ConfiguracionEnvioAutomaticoDetalleDTO>();
                var query = @"
                            SELECT 
                                Id,IdConfiguracionEnvioAutomatico, IdTipoEnvioAutomatico, IdTiempoFrecuencia, IdPlantilla, Valor, HoraEnvioAutomatico 
                            FROM 
                                mkt.T_ConfiguracionEnvioAutomaticoDetalle 
                            WHERE 
                                Estado=1 AND IdConfiguracionEnvioAutomatico = @IdConfiguracionEnvioAutomatico";
                var listaConfiguracionDB = _dapperRepository.QueryDapper(query, new { IdConfiguracionEnvioAutomatico = idConfiguracionEnvioAutomatico });
                if (!listaConfiguracionDB.Contains("[]") && !string.IsNullOrEmpty(listaConfiguracionDB))
                {
                    configuracion = JsonConvert.DeserializeObject<List<ConfiguracionEnvioAutomaticoDetalleDTO>>(listaConfiguracionDB)!;
                }
                return configuracion;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_ConfiguracionEnvioAutomaticoDetalle asociado a un Id.
        /// </summary>
        /// <param name="id">Id de ConfiguracionEnvioAutomaticoDetalle</param>
        /// <returns> ConfiguracionEnvioAutomaticoDetalle </returns>
        public ConfiguracionEnvioAutomaticoDetalle ObtenerPorId(int id)
        {
            try
            {
                ConfiguracionEnvioAutomaticoDetalle respuesta = new ConfiguracionEnvioAutomaticoDetalle();
                var query = @"
                            SELECT 
                                Id, IdConfiguracionEnvioAutomatico, IdTipoEnvioAutomatico, IdTiempoFrecuencia, IdPlantilla, Valor, EnvioWhatsApp,bEnvioCorreo, Estado, 
                                EnvioMensajeTexto, HoraEnvioAutomatico, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion                       
                            FROM 
                                mkt.T_ConfiguracionEnvioAutomaticoDetalle
                            WHERE 
                                Estado = 1 AND Id = @id";
                var resultado = _dapperRepository.FirstOrDefault(query, new { Id = id });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<ConfiguracionEnvioAutomaticoDetalle>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene toda la informacion de T_ConfiguracionEnvioAutomaticoDetalle asociado a un Id.
        /// </summary>
        /// <param name="idConfiguracionEnvioAutomatico">/param>
        /// <returns> ConfiguracionEnvioAutomaticoDetalle </returns>
        public IEnumerable<ConfiguracionEnvioAutomaticoDetalle> ObtenerPorIdConfiguracionEnvioAutomatico(int idConfiguracionEnvioAutomatico)
        {
            try
            {
                List<ConfiguracionEnvioAutomaticoDetalle> respuesta = new List<ConfiguracionEnvioAutomaticoDetalle>();
                var query = @"
                            SELECT 
                                Id, IdConfiguracionEnvioAutomatico, IdTipoEnvioAutomatico, IdTiempoFrecuencia, IdPlantilla, Valor, EnvioWhatsApp,bEnvioCorreo, Estado, 
                                EnvioMensajeTexto, HoraEnvioAutomatico, UsuarioCreacion, UsuarioModificacion, FechaCreacion, FechaModificacion, RowVersion, IdMigracion                       
                            FROM 
                                mkt.T_ConfiguracionEnvioAutomaticoDetalle
                            WHERE 
                                Estado = 1 AND IdConfiguracionEnvioAutomatico = @IdConfiguracionEnvioAutomatico";
                var resultado = _dapperRepository.FirstOrDefault(query, new { IdConfiguracionEnvioAutomatico = idConfiguracionEnvioAutomatico });
                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    respuesta = JsonConvert.DeserializeObject<List<ConfiguracionEnvioAutomaticoDetalle>>(resultado)!;
                }
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
