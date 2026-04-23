using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Implementacion
{
    /// <summary>
    /// Servicio para el Dashboard de Programas de Capacitacion
    /// Autor: Marco Villanueva Torres
    /// Fecha: 2025-04-17
    /// </summary>
    public class ReporteDashboardService : IReporteDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReporteDashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene el resumen de KPIs principales del dashboard
        /// </summary>
        public async Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        public async Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        public async Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorModalidadAsync(anio, estado, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        public async Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerProgramasPorEstadoAsync(estado, anio, fechaInicio, fechaFin, modalidad, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        public async Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerDetalleCursosAsync(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        public async Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerDocentesAsignadosAsync(anio, idDocente, estado, soloActivos, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerGraficoPorMesAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene los valores disponibles para los filtros del dashboard
        /// </summary>
        public async Task<ReporteDashboardFiltrosDTO> ObtenerFiltrosAsync()
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerFiltrosAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los datos del dashboard con filtros aplicados
        /// </summary>
        public async Task<List<ReporteDashboardCompletoDTO>> ObtenerDatosCompletosAsync(ReporteDashboardFiltroRequestDTO filtro)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerDatosCompletosAsync(filtro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene resumen semanal de sesiones
        /// </summary>
        public async Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenSemanalAsync(anio, mesInicio, mesFin, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        public async Task<List<ReporteDashboardCalendarioDTO>> ObtenerSesionesCalendarioAsync(int? anio, int? semanaInicio, int? semanaFin, int? mes)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerSesionesCalendarioAsync(anio, semanaInicio, semanaFin, mes);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// </summary>
        public async Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoSesionAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        public async Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(int? anio, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerSesionesPorEstadoAsync(anio, idEstadoSesion, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        public async Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerEvolucionEstadoSesionAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        public async Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerKPIsEstadoSesionAsync(anio, idProgramaEspecificoPadre, centroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? ultimasSemanas = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerCambiosEstadoAsync(ultimasSemanas); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? agrupacion, DateTime? fechaInicio, DateTime? fechaFin, int? ultimasSemanas = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerEstadosPorDiaAsync(idsPEspecificoHijo, estados, agrupacion, fechaInicio, fechaFin, ultimasSemanas); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, string? centroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerDetalleCursosV3Async(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, centroCostoPadre, modalidadClasificada, semanaInicio, semanaFin); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardSeguimientoClaseDTO>> ObtenerSeguimientoClasesAsync(ReporteDashboardSeguimientoFiltroRequestDTO filtro)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerSeguimientoClasesAsync(filtro); }
            catch (Exception) { throw; }
        }

        // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────

        public async Task<List<ReporteDashboardDocenteFiltroDTO>> ObtenerDocentesFiltroAsync(string? busqueda = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerDocentesFiltroAsync(busqueda); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardPEspecificoFiltroDTO>> ObtenerPEspecificoFiltroAsync(string? busqueda = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerPEspecificoFiltroAsync(busqueda); }
            catch (Exception) { throw; }
        }

        public async Task<ReporteDashboardSeguimientoDocenteDTO> ObtenerSeguimientoDocenteAsync(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerSeguimientoDocenteAsync(idDocente, idPEspecifico, anio, fechaInicio, fechaFin); }
            catch (Exception) { throw; }
        }

        public async Task<ReporteDashboardNotasAlumnosDTO> ObtenerNotasAlumnosPorProgramaAsync(int? idPEspecifico)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerNotasAlumnosPorProgramaAsync(idPEspecifico); }
            catch (Exception) { throw; }
        }
    }
}
