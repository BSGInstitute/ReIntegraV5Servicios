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
    /// Service: ConfiguracionEnvioAutomaticoDetalleService
    /// Autor: Jonathan Caipo
    /// Fecha: 15/12/2022
    /// <summary>
    /// Gestión general de T_ConfiguracionEnvioAutomaticoDetalle
    /// </summary>
    public class ConfiguracionEnvioAutomaticoDetalleService : IConfiguracionEnvioAutomaticoDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ConfiguracionEnvioAutomaticoDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TConfiguracionEnvioAutomaticoDetalle, ConfiguracionEnvioAutomaticoDetalle>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public ConfiguracionEnvioAutomaticoDetalle Add(ConfiguracionEnvioAutomaticoDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionEnvioAutomaticoDetalle>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ConfiguracionEnvioAutomaticoDetalle Update(ConfiguracionEnvioAutomaticoDetalle entidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<ConfiguracionEnvioAutomaticoDetalle>(modelo);
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
                _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionEnvioAutomaticoDetalle> Add(List<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionEnvioAutomaticoDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ConfiguracionEnvioAutomaticoDetalle> Update(List<ConfiguracionEnvioAutomaticoDetalle> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<ConfiguracionEnvioAutomaticoDetalle>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
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
                return _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.ObtenerConfiguracionEnvioAutomaticoDetalle(idConfiguracionEnvioAutomatico);
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
        /// <param name="id">Id de ConfiguracionEnvioAutomaticoDetalle</param>
        /// <returns> ConfiguracionEnvioAutomaticoDetalle </returns>
        public ConfiguracionEnvioAutomaticoDetalle ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.ObtenerPorId(id);
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
                return _unitOfWork.ConfiguracionEnvioAutomaticoDetalleRepository.ObtenerPorIdConfiguracionEnvioAutomatico(idConfiguracionEnvioAutomatico);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
