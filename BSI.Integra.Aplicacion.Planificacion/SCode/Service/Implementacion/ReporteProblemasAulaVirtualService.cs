using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// Service: ReporteProblemasAulaVirtualService
    /// Autor: Jonathan Caipo
    /// Fecha: 29/04/2023
    /// Version 1.0
    /// <summary>
    /// Gestión general del Reporte Problemas Aula Virtual
    /// </summary>
    public class ReporteProblemasAulaVirtualService : IReporteProblemasAulaVirtualService
    {
        private IUnitOfWork _unitOfWork;

        public ReporteProblemasAulaVirtualService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 29/04/2023
        /// Version 1.0
        /// <summary>
        /// Obtiene el combo completo
        /// </summary>
        /// <returns> DTO - ObtenerComboDTO - listaCombos </returns>
        public ReporteProblemaAulaVirtuaCombolDTO ObtenerCombos()
        {
            try
            {
                ReporteProblemaAulaVirtuaCombolDTO listaCombos = new()
                {
                    MatriculasCabecera = _unitOfWork.MatriculaCabeceraRepository.ObtenerCombo().ToList(),
                    Coordinadores = _unitOfWork.PersonalRepository.ObtenerCoordinadorasOperaciones().ToList(),
                    CentroCostos = _unitOfWork.CentroCostoRepository.ObtenerCombo().ToList(),
                    TiposCategoriaError = _unitOfWork.AlumnoRepository.ObtenerTodoFiltroTipoCategoriaError().ToList()
                };
                return listaCombos;
            }
            catch
            {
                throw;
            }
        }
        /// Autor: Jonathan Caipo
        /// Fecha: 24/04/2023
        /// Version: 1.0
        /// <summary>
        /// Obtiene Reporte completo de Problemas Aula Virtual
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        public IEnumerable<ReporteProblemasAulaVirtualResultadoDTO> ReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtro)
        {
            try
            {
                filtro.FechaInicio = new DateTime(filtro.FechaInicio.Year, filtro.FechaInicio.Month, filtro.FechaInicio.Day, 0, 0, 0);
                filtro.FechaFin = new DateTime(filtro.FechaFin.Year, filtro.FechaFin.Month, filtro.FechaFin.Day, 23, 59, 59);

                return _unitOfWork.ReporteProblemasAulaVirtualRepository.ObtenerReporteProblemasAulaVirtual(filtro);
            }
            catch
            {
                throw;
            }
        }
    }
}
