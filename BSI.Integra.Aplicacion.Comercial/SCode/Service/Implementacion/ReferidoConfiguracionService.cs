using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: ReferidoConfiguracionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/09/2022
    /// <summary>
    /// Gestión general de la tabla ReferidoConfiguracion
    /// </summary>
    public class ReferidoConfiguracionService : IReferidoConfiguracionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ReferidoConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TReferidoConfiguracion, ReferidoConfiguracion>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// version: 1.0
        /// <summary>
        /// Obtiene el primer registro de la tabla.
        /// </summary>
        /// <returns>ReferidoConfiguracionDTO</returns>
        public ReferidoConfiguracionDTO ObtenerConfiguracionReferidos()
        {
            try
            {
                return _unitOfWork.ReferidoConfiguracionRepository.ObtenerConfiguracionReferidos();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
