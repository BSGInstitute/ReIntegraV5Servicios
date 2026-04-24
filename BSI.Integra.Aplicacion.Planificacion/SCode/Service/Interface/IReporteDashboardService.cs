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
        Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

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
        Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        Task<List<ReporteDashboardCalendarioDTO>> ObtenerSesionesCalendarioAsync(int? anio, int? semanaInicio, int? semanaFin, int? mes);

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// </summary>
        Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(int? anio, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene cambios de estado de programas basados en log
        /// </summary>
        Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? ultimasSemanas = null);

        /// <summary>
        /// Obtiene estados de programas hijo agrupados por dia o semana
        /// </summary>
        Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? agrupacion, DateTime? fechaInicio, DateTime? fechaFin, int? ultimasSemanas = null);

        /// <summary>
        /// Obtiene detalle de cursos V3 con modalidad clasificada (Inhouse/Presencial/Online)
        /// </summary>
        Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin);

        /// <summary>
        /// Obtiene seguimiento de clases por dia de semana con filtro propio
        /// </summary>
        Task<List<ReporteDashboardSeguimientoClaseDTO>> ObtenerSeguimientoClasesAsync(ReporteDashboardSeguimientoFiltroRequestDTO filtro);

        // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────

        /// <summary>
        /// Obtiene lista de docentes para filtro con busqueda
        /// </summary>
        Task<List<ReporteDashboardDocenteFiltroDTO>> ObtenerDocentesFiltroAsync(string? busqueda = null);

        /// <summary>
        /// Obtiene programas especificos donde el docente tiene sesiones asignadas
        /// </summary>
        Task<List<ReporteDashboardPEspecificoPorDocenteDTO>> ObtenerPEspecificoPorDocenteAsync(int idProveedor);

        /// <summary>
        /// Obtiene lista de programas especificos para filtro con busqueda
        /// </summary>
        Task<List<ReporteDashboardPEspecificoFiltroDTO>> ObtenerPEspecificoFiltroAsync(string? busqueda = null);

        /// <summary>
        /// Obtiene el seguimiento completo de sesiones de un docente (KPIs + por programa + detalle)
        /// </summary>
        Task<ReporteDashboardSeguimientoDocenteDTO> ObtenerSeguimientoDocenteAsync(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin);

        /// <summary>
        /// Obtiene notas de alumnos calculadas por PEspecifico
        /// </summary>
        Task<ReporteDashboardNotasPorPEspecificoDTO> ObtenerNotasPorPEspecificoAsync(int idPEspecifico, int grupo);

        // ── Dashboard 3: Furs ─────────────────────────────────────────────────
        /// <summary>
        /// Obtiene FURs del area 19, tipo PO, estados 3 y 5 para Dashboard 3
        /// </summary>
        Task<List<FurDTO>> ObtenerFursDashboard3Async();
    }
}
