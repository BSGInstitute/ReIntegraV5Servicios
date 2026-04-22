using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz del repositorio para el Dashboard de Programas de Capacitacion
    /// Autor: Marco Villanueva Torres
    /// Fecha: 2025-04-17
    /// Modificacion: 2025-04-18 - Agregados filtros de PEspecifico y CentroCosto
    /// </summary>
    public interface IReporteDashboardRepository
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

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// </summary>
        Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(int? anio, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null);

        /// <summary>
        /// Obtiene cambios de estado de programas basado en log (Lanzamiento->Ejecucion, Ejecucion->Concluido, *->Cancelado)
        /// </summary>
        Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? ultimasSemanas = null);

        /// <summary>
        /// Obtiene estados de programas hijo agrupados por dia o semana
        /// </summary>
        Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? agrupacion, DateTime? fechaInicio, DateTime? fechaFin, int? ultimasSemanas = null);

        /// <summary>
        /// Obtiene detalle de cursos V3 con modalidad clasificada (Inhouse/Presencial/Online) y filtro por semana
        /// </summary>
        Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, string? centroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin);

        /// <summary>
        /// Obtiene seguimiento de clases por dia de semana (Lunes-Sabado) con filtro propio
        /// </summary>
        Task<List<ReporteDashboardSeguimientoClaseDTO>> ObtenerSeguimientoClasesAsync(ReporteDashboardSeguimientoFiltroRequestDTO filtro);
    }
}
