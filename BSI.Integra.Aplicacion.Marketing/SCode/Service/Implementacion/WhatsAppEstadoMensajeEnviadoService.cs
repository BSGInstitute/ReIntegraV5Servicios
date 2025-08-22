using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppEstadoMensajeEnviadoService
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 18/07/2022
    /// <summary>
    /// Gestión general de T_WhatsAppEstadoMensajeEnviado
    /// </summary>
    public class WhatsAppEstadoMensajeEnviadoService : IWhatsAppEstadoMensajeEnviadoService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppEstadoMensajeEnviadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppEstadoMensajeEnviado, WhatsAppEstadoMensajeEnviado>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }

        #region Metodos Base
        public WhatsAppEstadoMensajeEnviado Add(WhatsAppEstadoMensajeEnviado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppEstadoMensajeEnviado>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppEstadoMensajeEnviado Update(WhatsAppEstadoMensajeEnviado entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppEstadoMensajeEnviado>(modelo);
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
                _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppEstadoMensajeEnviado> Add(List<WhatsAppEstadoMensajeEnviado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppEstadoMensajeEnviado>>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WhatsAppEstadoMensajeEnviado> Update(List<WhatsAppEstadoMensajeEnviado> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<WhatsAppEstadoMensajeEnviado>>(modelo);
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
                _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.Delete(listadoIds, usuario);
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
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los registros de T_WhatsAppEstadoMensajeEnviado
        /// </summary>
        /// <returns> List<WhatsAppEstadoMensajeEnviadoDTO> </returns>
        public IEnumerable<WhatsAppEstadoMensajeEnviadoDTO> ObtenerWhatsAppEstadoMensajeEnviado()
        {
            try
            {
                return _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.ObtenerWhatsAppEstadoMensajeEnviado();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Erick Marcelo Quispe.
        /// Fecha: 18/07/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene registros de T_WhatsAppEstadoMensajeEnviado para mostrarse en combo.
        /// </summary>
        /// <returns> List<WhatsAppEstadoMensajeEnviadoComboDTO> </returns>
        public IEnumerable<WhatsAppEstadoMensajeEnviadoComboDTO> ObtenerCombo()
        {
            try
            {
                return _unitOfWork.WhatsAppEstadoMensajeEnviadoRepository.ObtenerCombo();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
