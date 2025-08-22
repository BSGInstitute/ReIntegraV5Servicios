using AutoMapper;
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteComprobantesService
    /// Autor Modificacion: Adriana Chipana.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteComprobantes
    /// </summary>
    public class ReporteComprobantesService : IReporteComprobantesService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteComprobantesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReporteComprobantes, ReporteComprobantes>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteComprobantesRecibidoDTO, ReporteComprobantes>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }


        public List<ReporteComprobantesDTO> ObtenerReporteComprobantes(int? idTipoAsociado)
        {

            return _unitOfWork.ReporteComprobantesRepository.ObtenerReporteComprobantes(idTipoAsociado);

        }

        /// <summary>
        /// Obtiene el Reporte de Indicadores de Productividad de Ventas
        /// </summary>
        /// <returns></returns>
        public List<ComboDTO> ObtenerTipo()
        {

            return _unitOfWork.ReporteComprobantesRepository.ObtenerTipo();

        } 

    }
}
