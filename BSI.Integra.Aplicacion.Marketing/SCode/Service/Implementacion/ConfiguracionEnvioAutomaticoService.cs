using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: ConfiguracionEnvioAutomaticoService
    /// Autor: Jonathan Caipo
    /// Fecha: 12/12/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioAutomatico
    /// </summary>
    public class ConfiguracionEnvioAutomaticoService : IConfiguracionEnvioAutomaticoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionEnvioAutomaticoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioAutomatico, ConfiguracionEnvioAutomatico>(MemberList.None).ReverseMap();
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
                return _unitOfWork.ConfiguracionEnvioAutomaticoRepository.ObtenerConfiguracionEnvioAutomatico();
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
        /// Inserta una Configuración Automática
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> ConfiguracionEnvioDTO </returns>
        public List<ConfiguracionEnvioDTO> InsertarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioAutomaticoRepository.InsertarConfiguracion(objeto);
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
        /// Actualiza una Configuración Automática
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns> ConfiguracionEnvioDTO </returns>
        public List<ConfiguracionEnvioDTO> ActualizarConfiguracion(ConfiguracionEnvioAutomaticoDTO objeto)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioAutomaticoRepository.ActualizarConfiguracion(objeto);
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
        /// Elimina una Configuración Automática
        /// </summary>
        /// <param name="idConfiguracion"></param>
        /// <param name="usuario"></param>
        /// <returns> List<ConfiguracionEnvioDTO ></returns>
        public List<ConfiguracionEnvioDTO> EliminarConfiguracion(int idConfiguracion, string usuario)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioAutomaticoRepository.EliminarConfiguracion(idConfiguracion, usuario);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
