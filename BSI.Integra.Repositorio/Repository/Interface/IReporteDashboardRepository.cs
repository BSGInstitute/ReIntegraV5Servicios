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
        Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? mes = null, int? semana = null, string? modalidad = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

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
        Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mes = null, int? semana = null, int? diaMes = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        Task<List<ReporteDashboardCalendarioDTO>> ObtenerSesionesCalendarioAsync(int? anio, int? semanaInicio, int? semanaFin, int? mes);

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// </summary>
        Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene cambios de estado de programas basado en log (Lanzamiento->Ejecucion, Ejecucion->Concluido, *->Cancelado)
        /// </summary>
        Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? anio = null, int? mes = null, int? semana = null, int? diaMes = null);

        /// <summary>
        /// Obtiene estados de programas hijo agrupados por Anio, Mes, Semana o Dia
        /// </summary>
        Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, int? anio, int? mes, int? semana, int? diaMes, int? ultimasSemanas = null, DateTime? fechaInicio = null, DateTime? fechaFin = null);

        /// <summary>
        /// Obtiene detalle de cursos V3 con modalidad clasificada (Inhouse/Presencial/Online) y filtro por semana
        /// </summary>
        Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin);

        /// <summary>
        /// Obtiene seguimiento de clases por dia de semana (Lunes-Sabado) con filtro propio
        /// </summary>
        Task<List<ReporteDashboardSeguimientoClaseDTO>> ObtenerSeguimientoClasesAsync(ReporteDashboardSeguimientoFiltroRequestDTO filtro);

        // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────
        Task<List<ReporteDashboardDocenteFiltroDTO>> ObtenerDocentesFiltroAsync(string? busqueda);

        /// <summary>
        /// Obtiene programas especificos donde un docente (IdProveedor) tiene sesiones asignadas
        /// </summary>
        Task<List<ReporteDashboardPEspecificoPorDocenteDTO>> ObtenerPEspecificoPorDocenteAsync(int idProveedor);
        Task<List<ReporteDashboardPEspecificoFiltroDTO>> ObtenerPEspecificoFiltroAsync(string? busqueda);
        Task<List<ReporteDashboardCursoFiltroDTO>> ObtenerCursosPorProgramaAsync(int? idProgramaPadre, string? busqueda);
        Task<List<ReporteDashboardPaisFiltroDTO>> ObtenerPaisesFiltroAsync();
        Task<ReporteDashboardSeguimientoDocenteDTO> ObtenerSeguimientoDocenteAsync(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin);

        /// <summary>
        /// Obtiene notas de alumnos por PEspecifico usando SP_PW_ListadoNotaProcesarOnline
        /// </summary>
        Task<ReporteDashboardNotasPorPEspecificoDTO> ObtenerNotasPorPEspecificoAsync(int idPEspecifico, int grupo);

        // ── Dashboard 3: Furs ─────────────────────────────────────────────────
        /// <summary>
        /// Obtiene FURs del area 19, tipo PO, estados 3 y 5 para Dashboard 3
        /// </summary>
        Task<List<FurDTO>> ObtenerFursDashboard3Async();

        // ── Nuevos endpoints: Por Estado / Por Modalidad / Grafico Por Mes ───

        /// <summary>
        /// Obtiene KPIs de programas agrupados por estado con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardResumenProgramasDTO>> ObtenerResumenPorEstadoProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene KPIs de cursos agrupados por estado con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardResumenCursosDTO>> ObtenerResumenPorEstadoCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene distribucion de programas por modalidad con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardModalidadProgramasDTO>> ObtenerResumenPorModalidadProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene distribucion de cursos por modalidad con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardModalidadCursosDTO>> ObtenerResumenPorModalidadCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idCentroCostoPadre = null);

        /// <summary>
        /// Obtiene evolucion mensual de programas por estado con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardGraficoPorMesProgramasDTO>> ObtenerGraficoPorMesProgramasAsync(string? anios, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null, int? idPais = null, string? estado = null);

        /// <summary>
        /// Obtiene evolucion mensual de cursos por estado con filtros de fecha
        /// </summary>
        Task<List<ReporteDashboardGraficoPorMesCursosDTO>> ObtenerGraficoPorMesCursosAsync(string? anios, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null, int? idPais = null, string? estado = null);
    }
}
