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
    /// Repositorio: ConfiguracionEnvioAutomaticoRepository
    /// Autor: Jonathan Caipo
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioAutomatico
    /// </summary>
    public class ConfiguracionEnvioAutomaticoRepository : GenericRepository<TConfiguracionEnvioAutomatico>, IConfiguracionEnvioAutomaticoRepository
    {
        private Mapper _mapper;

        public ConfiguracionEnvioAutomaticoRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository) : base(context, connectionFactory, dapperRepository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomatico>(MemberList.None).ReverseMap();
                //cfg.CreateMap<THijo, Hijo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 15/12/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene Configuración Envío Automático
        /// </summary>
        /// <returns> List<ObtenerConfiguracionEnvioDTO> </returns>
        public List<ObtenerConfiguracionEnvioDTO> ObtenerConfiguracionEnvioAutomatico()
        {
            try
            {
                var data = new List<ObtenerConfiguracionEnvioDTO>();
                var query = @"
                            SELECT 
                                Id, IdEstado_Inicial AS IdEstadoInicial ,IdEstado_Destino AS IdEstadoDestino, IdSubEstado_Inicial AS IdSubEstadoInicial, Estado, 
                                IdSubEstado_Destino AS IdSubEstadoDestino, EnvioWhatsApp, EnvioCorreo, EnvioMensajeTexto, UsuarioModificacion, FechaModificacion  
                            FROM 
                                mkt.T_ConfiguracionEnvioAutomatico 
                            WHERE 
                                Estado = 1";
                var respuesta = _dapperRepository.QueryDapper(query, null);
                if (!string.IsNullOrEmpty(respuesta) && !respuesta.Contains("[]"))
                {
                    data = JsonConvert.DeserializeObject<List<ObtenerConfiguracionEnvioDTO>>(respuesta);
                }
                return data;
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
        /// Inserta una Configuración Automática
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> ConfiguracionEnvioDTO </returns>
        public List<ConfiguracionEnvioDTO> InsertarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                string query = "mkt.SP_InsertarConfiguracionEnvioAutomatico";
                var queryInsert = _dapperRepository.QuerySPDapper(query, new
                {
                    IdEstadoInicial = objeto.IdEstadoInicial,
                    IdEstadoDestino = objeto.IdEstadoDestino,
                    IdSubEstadoInicial = objeto.IdSubEstadoInicial,
                    IdSubEstadoDestino = objeto.IdSubEstadoDestino,
                    AplicaWhatsApp = objeto.AplicaWhatsApp,
                    AplicaSMS = objeto.AplicaSMS,
                    AplicaCorreo = objeto.AplicaCorreo,
                    UsuarioConfiguracion = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryInsert)!;
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
        /// Actualiza una Configuración Automática
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> ConfiguracionEnvioDTO </returns>
        public List<ConfiguracionEnvioDTO> ActualizarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                string _queryActualizar = "mkt.SP_ActualizarConfiguracionEnvioAutomatico";
                var queryActualizar = _dapperRepository.QuerySPDapper(_queryActualizar, new
                {
                    IdConfiguracion = objeto.Id,
                    IdEstadoInicial = objeto.IdEstadoInicial,
                    IdEstadoDestino = objeto.IdEstadoDestino,
                    IdSubEstadoInicial = objeto.IdSubEstadoInicial,
                    IdSubEstadoDestino = objeto.IdSubEstadoDestino,
                    AplicaWhatsApp = objeto.AplicaWhatsApp,
                    AplicaSMS = objeto.AplicaSMS,
                    AplicaCorreo = objeto.AplicaCorreo,
                    UsuarioConfiguracion = objeto.Usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryActualizar)!;
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
        /// Elimina una Configuración Automática
        /// </summary>
        /// <param name="idConfiguracion"></param>
        /// <param name="usuario"></param>
        /// <returns> List<ConfiguracionEnvioDTO ></returns>

        public List<ConfiguracionEnvioDTO> EliminarConfiguracion(int idConfiguracion, string usuario)
        {
            try
            {
                string query = "mkt.SP_EliminarConfiguracionEnvioAutomatico";
                var queryEliminar = _dapperRepository.QuerySPDapper(query, new
                {
                    IdConfiguracion = idConfiguracion,
                    Usuario = usuario
                });
                return JsonConvert.DeserializeObject<List<ConfiguracionEnvioDTO>>(queryEliminar)!;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
