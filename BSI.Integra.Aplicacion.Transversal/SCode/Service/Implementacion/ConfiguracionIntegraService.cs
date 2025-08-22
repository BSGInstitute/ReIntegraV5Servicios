using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: ConfiguracionIntegraService
    /// Autor: Flavio R. Mamani Fabian
    /// Fecha: 25/10/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionIntegra
    /// </summary>
    public class ConfiguracionIntegraService : IConfiguracionIntegraService
    {
        private IUnitOfWork _unitOfWork;
        public ConfiguracionIntegraService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene el estado de validacion de ips
        /// </summary>
        /// <returns> bool => estado validacion ip </returns>
        public bool ObtenerEstadoValidacionIp()
        {
            return _unitOfWork.ConfiguracionIntegraRepository.ObtenerEstadoValidacionIp();
        }
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 30/01/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene las apis para la validacion por ips
        /// </summary>
        /// <returns> Lista de Apis </returns>
        public List<ClaveValorDTO> ObtenerApisValidacionIp()
        {
            return _unitOfWork.ConfiguracionIntegraRepository.ObtenerApisValidacionIp();
        }
    }
}
