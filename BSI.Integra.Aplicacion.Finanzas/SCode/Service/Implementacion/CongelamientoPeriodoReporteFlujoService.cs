using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Implementation;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: CongelamientoPeriodoReporteFlujoService
    /// Autor Modificacion: Adriana Chipana.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_CongelamientoPeriodoReporteFlujo
    /// </summary>
    public class CongelamientoPeriodoReporteFlujoService : ICongelamientoPeriodoReporteFlujoService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public CongelamientoPeriodoReporteFlujoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TCongelamientoPeriodoReporteFlujo, CongelamientoPeriodoReporteFlujo>(MemberList.None).ReverseMap();
                //cfg.CreateMap<CongelamientoPeriodoReporteFlujoRecibidoDTO, CongelamientoPeriodoReporteFlujo>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }

        public bool GenerarCongelamientoReporte(List<FlujoCongelamientoPeriodoDTO> FlujoCongelamientoPeriodo)
        {
            try
            {
                var valor = _unitOfWork.CongelamientoPeriodoReporteFlujoRepository.GenerarCongelamientoReporte(FlujoCongelamientoPeriodo);

                return valor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

 


}
}
