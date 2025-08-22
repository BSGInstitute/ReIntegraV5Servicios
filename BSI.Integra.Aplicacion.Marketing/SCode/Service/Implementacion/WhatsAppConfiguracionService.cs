using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Marketing.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Marketing.Service.Implementacion
{

    /// Service: WhatsAppConfiguracionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 26/09/2022
    /// <summary>
    /// Gestión general de WhatsAppConfiguracion
    /// </summary>
    public class WhatsAppConfiguracionService : IWhatsAppConfiguracionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public WhatsAppConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<TWhatsAppConfiguracion, WhatsAppConfiguracion>(MemberList.None).ReverseMap());
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 26/09/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <param name="idPais">Id del pais (PK de la tabla conf.T_Pais)</param>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        public WhatsAppHostDatosDTO ObtenerCredencialHost(int idPais)
        {
            try
            {
                return _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHost(idPais);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 14/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los datos del host del whatsapp por idPais 
        /// </summary>
        /// <returns>Objeto de tipo WhatsAppHostDatosDTO</returns>
        public List<WhatsAppHostDatosDTO> ObtenerCredencialHostGeneral()
        {
            try
            {
                return _unitOfWork.WhatsAppConfiguracionRepository.ObtenerCredencialHostGeneral();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// Autor: Rodrigo Montesinos.
        /// Fecha: 26/12/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los horarios dispobnibles para los combos 
        /// </summary>
        /// <returns>Objeto de tipo lsita HoraDTO</returns>
        public List<HoraDTO> ObtenerConfiguracionDeHorariosDeEnvioParaCombo(int intervalo)
        {
            try
            {
                return _unitOfWork.WhatsAppConfiguracionRepository.ObtenerConfiguracionDeHorariosDeEnvioParaCombo(intervalo);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
