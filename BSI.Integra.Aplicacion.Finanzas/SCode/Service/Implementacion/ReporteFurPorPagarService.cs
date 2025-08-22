using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteFurPorPagarService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteFurPorPagar
    /// </summary>
    public class ReporteFurPorPagarService : IReporteFurPorPagarService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteFurPorPagarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReporteFurPorPagar, ReporteFurPorPagar>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteFurPorPagarRecibidoDTO, ReporteFurPorPagar>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        /// <summary>
        /// Obtiene el Reporte de Indicadores de Productividad de Ventas
        /// </summary>
        /// <returns></returns>
        public List<FurPorPagarDTO> ObtenerFurPorPagarByFecha(FiltroFurPorPagarDTO filtro)
        {

            return _unitOfWork.ReporteFurPorPagarRepository.ObtenerFurPorPagarByFecha(filtro.FechaInicio, filtro.FechaFin);

        } 

    }
}
