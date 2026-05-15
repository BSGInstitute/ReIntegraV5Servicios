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
        public async Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        public async Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        public async Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorModalidadAsync(anio, estado, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        public async Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerProgramasPorEstadoAsync(estado, anio, fechaInicio, fechaFin, modalidad, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        public async Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerDetalleCursosAsync(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        public async Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerDocentesAsignadosAsync(anio, idDocente, estado, soloActivos, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerGraficoPorMesAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
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
        public async Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mes = null, int? semana = null, int? diaMes = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenSemanalAsync(anio, mes, semana, diaMes, idProgramaEspecificoPadre, idCentroCostoPadre);
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
        public async Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        public async Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(int? anio, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerSesionesPorEstadoAsync(anio, idEstadoSesion, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        public async Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerEvolucionEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        public async Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                return await _unitOfWork.ReporteDashboardRepository.ObtenerKPIsEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? anio = null, int? mes = null, int? semana = null, int? diaMes = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerCambiosEstadoAsync(anio, mes, semana, diaMes); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? agrupacion, DateTime? fechaInicio, DateTime? fechaFin, int? ultimasSemanas = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerEstadosPorDiaAsync(idsPEspecificoHijo, estados, agrupacion, fechaInicio, fechaFin, ultimasSemanas); }
            catch (Exception) { throw; }
        }

        public async Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerDetalleCursosV3Async(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, idCentroCostoPadre, modalidadClasificada, semanaInicio, semanaFin); }
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

        public async Task<List<ReporteDashboardPEspecificoPorDocenteDTO>> ObtenerPEspecificoPorDocenteAsync(int idProveedor)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerPEspecificoPorDocenteAsync(idProveedor); }
            catch (Exception) { throw; }
        }

        public async Task<ReporteDashboardSeguimientoDocenteDTO> ObtenerSeguimientoDocenteAsync(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerSeguimientoDocenteAsync(idDocente, idPEspecifico, anio, fechaInicio, fechaFin); }
            catch (Exception) { throw; }
        }

        public async Task<ReporteDashboardNotasPorPEspecificoDTO> ObtenerNotasPorPEspecificoAsync(int idPEspecifico, int grupo)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerNotasPorPEspecificoAsync(idPEspecifico, grupo); }
            catch (Exception) { throw; }
        }

        public async Task<List<FurDTO>> ObtenerFursDashboard3Async()
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerFursDashboard3Async(); }
            catch (Exception) { throw; }
        }

        // ── Nuevos endpoints: Por Estado / Por Modalidad / Grafico Por Mes ───

        /// <summary>
        /// Obtiene KPIs de programas agrupados por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardResumenProgramasDTO>> ObtenerResumenPorEstadoProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoProgramasAsync(anio, mes, semana, diaMes, idProgramaEspecificoPadre, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtiene KPIs de cursos agrupados por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardResumenCursosDTO>> ObtenerResumenPorEstadoCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorEstadoCursosAsync(anio, mes, semana, diaMes, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtiene distribucion de programas por modalidad con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardModalidadProgramasDTO>> ObtenerResumenPorModalidadProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorModalidadProgramasAsync(anio, mes, semana, diaMes, idEstadoPEspecifico, idProgramaEspecificoPadre, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtiene distribucion de cursos por modalidad con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardModalidadCursosDTO>> ObtenerResumenPorModalidadCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerResumenPorModalidadCursosAsync(anio, mes, semana, diaMes, idEstadoPEspecifico, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtiene evolucion mensual de programas por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesProgramasDTO>> ObtenerGraficoPorMesProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerGraficoPorMesProgramasAsync(anio, mes, semana, diaMes, idProgramaEspecificoPadre, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtiene evolucion mensual de cursos por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesCursosDTO>> ObtenerGraficoPorMesCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null)
        {
            try { return await _unitOfWork.ReporteDashboardRepository.ObtenerGraficoPorMesCursosAsync(anio, mes, semana, diaMes, idCentroCostoPadre); }
            catch (Exception) { throw; }
        }

    }
}
