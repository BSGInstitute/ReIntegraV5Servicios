using AutoMapper;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteCambiosCodigosCuotasService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 28/06/2022
    /// <summary>
    /// Gestión general de T_ReporteCambiosCodigosCuotas
    /// </summary>
    public class ReporteCambiosCodigosCuotasService : IReporteCambiosCodigosCuotasService
    {

        private IUnitOfWork _unitOfWork;
        private Mapper _mapper;

        public ReporteCambiosCodigosCuotasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.CreateMap<TReporteCambiosCodigosCuotas, ReporteCambiosCodigosCuotas>(MemberList.None).ReverseMap();
                //cfg.CreateMap<ReporteCambiosCodigosCuotasRecibidoDTO, ReporteCambiosCodigosCuotas>(MemberList.None).ReverseMap();
            });
            _mapper = new Mapper(config);
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de cambios
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambiosDTO> ObtenerReporteCambios(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                return _unitOfWork.ReporteCambiosCodigosCuotasRepository.ObtenerReporteCambios(FiltroCambios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de codigos
        /// </summary>
        /// <returns></returns>
        public List<ReporteCodigosDTO> ObtenerReporteCodigos(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                return _unitOfWork.ReporteCambiosCodigosCuotasRepository.ObtenerReporteCodigos(FiltroCambios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de cuotas
        /// </summary>
        /// <returns></returns>
        public List<ReporteCuotasDTO> ObtenerReporteCuotas(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                return _unitOfWork.ReporteCambiosCodigosCuotasRepository.ObtenerReporteCuotas(FiltroCambios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Griselberto Huaman
        /// Fecha: 28/06/2022
        /// <summary>
        /// Obtiene el reporte de traslaciones
        /// </summary>
        /// <returns></returns>
        public List<ReporteCambioProgramaDTO> ObtenerReporteTraslados(ReporteCambiosCodigosCuotasFiltroDTO FiltroCambios)
        {
            try
            {
                return _unitOfWork.ReporteCambiosCodigosCuotasRepository.ObtenerReporteTraslados(FiltroCambios);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        ///Autor: Griselberto Huamanc
        /// Fecha: 28/06/2022
        /// <summary>
        /// Congela los datos de la tabla T_CronogramaPagoDetalleModLogFinal en base a una fecha fecha 
        /// </summary>
        /// <returns>Objeto</returns>
        /// <param name="FechaCongelamiento"> Fecha de COngelamiento</param>
        /// <param name="Usuario"> Usuario Responsable </param>
        public int CongelarReporteDeCambios(DateTime FechaCongelamiento, string Usuario)
        {
            try
            {
                return _unitOfWork.ReporteCambiosCodigosCuotasRepository.CongelarReporteDeCambios(FechaCongelamiento, Usuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
