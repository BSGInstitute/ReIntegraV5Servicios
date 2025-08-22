using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Service: CampaniaMailingDetalleService
    /// Autor: Gilmer Quispe.
    /// Fecha: 03/10/2022
    /// <summary>
    /// Gestión general de CampaniaMailingDetalle
    /// </summary>
    public class CampaniaMailingDetalleService : ICampaniaMailingDetalleService
    {
        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CampaniaMailingDetalleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TCampaniaMailingDetalle, CampaniaMailingDetalle>(MemberList.None).ReverseMap();
            }
            );
            _mapper = new Mapper(config);
        }
        /// Autor: Gilmer Quispe.
        /// Fecha: 03/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el id de la campania mailing por el codigo enviado
        /// </summary>
        /// <param name="codMailing">Codigo Mailing</param>
        /// <returns>Objeto de clase ValorIntDTO</returns>
        public ValorIntDTO IdCampaniaMailing(string codMailing)
        {
            try
            {
                return _unitOfWork.CampaniaMailingDetalleRepository.ObtenerIdCampaniaMailing(codMailing);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
