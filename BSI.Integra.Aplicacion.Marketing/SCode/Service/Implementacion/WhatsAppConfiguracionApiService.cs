using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{
    /// Service: WhatsAppConfiguracionApiService
    /// Autor: Jorge Gamero.
    /// Fecha: 19/08/2024
    /// <summary>
    /// Gestión general de WhatsAppConfiguracionApi
    /// </summary>
    public class WhatsAppConfiguracionApiService : IWhatsAppConfiguracionApiService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppConfiguracionApiService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TWhatsAppConfiguracionApi, WhatsAppConfiguracionApi>(MemberList.None).ReverseMap();
                cfg.CreateMap<WhatsAppConfiguracionApi, WhatsAppConfiguracionApiDTO>(MemberList.None).ReverseMap();
            }

            );
            _mapper = new Mapper(config);
        }
        #region Metodos Base
        public WhatsAppConfiguracionApi Add(WhatsAppConfiguracionApi entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppConfiguracionApiRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppConfiguracionApi>(modelo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WhatsAppConfiguracionApi Update(WhatsAppConfiguracionApi entidad)
        {
            try
            {
                var modelo = _unitOfWork.WhatsAppConfiguracionApiRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<WhatsAppConfiguracionApi>(modelo);
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
                _unitOfWork.WhatsAppConfiguracionApiRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        public List<WhatsAppConfiguracionApiListaGrillaDTO> ObtenerCredencialesUsuarios()
        {
            try
            {
                var dto = _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerCredencialesUsuarios();
                _unitOfWork.Commit();
                return dto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// Autor: Jorge Gamero
        /// Fecha: 19/08/2024
        /// Version: 1.0
        /// <summary>
        /// Obtiene todos los campos de T_WhatsAppConfiguracionApi por el Id.
        /// </summary>
        /// <param name="id"> Id de la entidad </param>
        /// <returns> WhatsAppConfiguracionApi </returns>
        public WhatsAppConfiguracionApi ObtenerPorId(int id)
        {
            try
            {
                return _unitOfWork.WhatsAppConfiguracionApiRepository.ObtenerPorId(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
