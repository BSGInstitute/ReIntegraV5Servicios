using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    /// <summary>
    /// Interfaz del servicio para el Dashboard de Programas de Capacitacion
    /// Autor: Marco Villanueva Torres
    /// Fecha: 2025-04-17
    /// Modificacion: 2025-04-18 - Agregados filtros de PEspecifico y CentroCosto
    /// </summary>
    public interface IReporteDashboardService
    {
        /// <summary>
        /// Obtiene el resumen de KPIs principales del dashboard
        /// </summary>
        Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene los valores disponibles para los filtros del dashboard
        /// </summary>
        Task<ReporteDashboardFiltrosDTO> ObtenerFiltrosAsync();

        /// <summary>
        /// Obtiene todos los datos del dashboard con filtros aplicados
        /// </summary>
        Task<List<ReporteDashboardCompletoDTO>> ObtenerDatosCompletosAsync(ReporteDashboardFiltroRequestDTO filtro);

        /// <summary>
        /// Obtiene resumen semanal de sesiones
        /// </summary>
        Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        Task<List<ReporteDashboardCalendarioDTO>> ObtenerSesionesCalendarioAsync(int? anio, int? semanaInicio, int? semanaFin, int? mes);
    }
}
