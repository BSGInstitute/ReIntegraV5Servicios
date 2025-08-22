using AutoMapper;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Comercial.Service.Implementacion
{
    /// Servicio: ContactoConfiguracionService
    /// Autor: Gilmer Quispe.
    /// Fecha: 14/09/2022
    /// <summary>
    /// Gestión general de la tabla ContactoConfiguracion
    /// </summary>
    public class ContactoConfiguracionService : IContactoConfiguracionService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;
        public ContactoConfiguracionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<TContactoConfiguracion, ContactoConfiguracion>(MemberList.None).ReverseMap();
                }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 14/09/2022
        /// version: 1.0
        /// <summary>
        /// Obtiene el registro de la tabla de acuerdo al IdTipoDato.
        /// </summary>
        /// <param name="idTipoDato"></param>
        /// <returns></returns>
        public ContactoConfiguracionDTO ObtenerConfiguracionContactoPorIdTipoDato(int idTipoDato)
        {
            try
            {
                return _unitOfWork.ContactoConfiguracionRepository.ObtenerConfiguracionContactoPorIdTipoDato(idTipoDato);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
