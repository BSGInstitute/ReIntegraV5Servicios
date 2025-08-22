using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Implementacion
{
    /// Service: ReporteComisionMatriculaService
    /// Autor Modificacion: Griselberto Huaman.
    /// Fecha: 27/06/2022
    /// <summary>
    /// Gestión general de T_ReporteComisionMatricula
    /// </summary>
    public class ReporteComisionMatriculaService : IReporteComisionMatriculaService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteComisionMatriculaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// Autor : Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene combo de Subestados de seguimiento comsiones matricula
        /// </summary>
        /// <returns> List<ReporteComisionMatriculaDTO> </returns>
        public IEnumerable<DTO.ComboDTO> ObtenerListaSubEstadosParaSeguimientoComisiones()
        {
            try
            {
                return _unitOfWork.ReporteComisionMatriculaRepository.ObtenerListaSubEstadosParaSeguimientoComisiones();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// Autor : Griselberto Huaman.
        /// Fecha: 24/06/2022
        /// Version: 1.0
        /// <summary>
        /// Obtiene reporte de Comisiones Por Matricula para Grilla
        /// </summary>
        /// <returns> List<ReporteComisionMatriculaDTO> </returns>
        public IEnumerable<ReporteSeguimientoComisionesDTO> ObtenerDatosReporteSeguimientoComisiones(FiltroReporteSeguimientoComisionesDTO filtro)
        {
            try
            {
                return _unitOfWork.ReporteComisionMatriculaRepository.ObtenerDatosReporteSeguimientoComisiones(filtro);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }

}
