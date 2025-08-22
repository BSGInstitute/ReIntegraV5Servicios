using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: SmsConfiguracionEnvioService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/08/2022
    /// <summary>
    /// Gestión general de T_SmsConfiguracionEnvio
    /// </summary>
    public class SmsConfiguracionEnvioService : ISmsConfiguracionEnvioService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public SmsConfiguracionEnvioService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TSmsConfiguracionEnvio, SmsConfiguracionEnvio>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public SmsConfiguracionEnvio Add(SmsConfiguracionEnvio entidad)
        {
            try
            {
                var modelo = _unitOfWork.SmsConfiguracionEnvioRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SmsConfiguracionEnvio>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SmsConfiguracionEnvio Update(SmsConfiguracionEnvio entidad)
        {
            try
            {
                var modelo = _unitOfWork.SmsConfiguracionEnvioRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<SmsConfiguracionEnvio>(modelo);
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
                _unitOfWork.SmsConfiguracionEnvioRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SmsConfiguracionEnvio> Add(List<SmsConfiguracionEnvio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SmsConfiguracionEnvioRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SmsConfiguracionEnvio>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SmsConfiguracionEnvio> Update(List<SmsConfiguracionEnvio> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.SmsConfiguracionEnvioRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<SmsConfiguracionEnvio>>(modelo);
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
                _unitOfWork.SmsConfiguracionEnvioRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_SmsConfiguracionEnvio
        /// </summary>
        /// <returns> List<SmsConfiguracionEnvioDTO> </returns>
        public IEnumerable<SmsConfiguracionEnvioDTO> ObtenerSmsConfiguracionEnvio()
        {
            try
            {
                return _unitOfWork.SmsConfiguracionEnvioRepository.ObtenerSmsConfiguracionEnvio();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene la configuracion de Sms basado en la oportunidad
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase SmsEnvioAnexoDTO</returns>
        public SmsEnvioAnexoDTO ConfiguracionSmsOportunidad(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SmsConfiguracionEnvioRepository.ConfiguracionSmsOportunidad(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/08/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene los dias sin contacto
        /// </summary>
        /// <param name="idOportunidad">Id de la oportunidad (PK de la tabla com.T_Oportunidad)</param>
        /// <returns>Objeto de clase OportunidadDiasSinContactoDTO</returns>
        public OportunidadDiasSinContactoDTO ObtenerDiasSinContacto(int idOportunidad)
        {
            try
            {
                return _unitOfWork.SmsConfiguracionEnvioRepository.ObtenerDiasSinContacto(idOportunidad);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
