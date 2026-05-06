using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Planificacion.Service.Implementacion;
using BSI.Integra.Aplicacion.Planificacion.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Planificacion
{
    /// <summary>
    /// Controlador para el Dashboard de Programas de Capacitacion
    /// Autor: Marco Villanueva Torres
    /// Fecha: 2025-04-17
    /// Modificacion: 2025-04-18 - Agregados filtros de PEspecifico y CentroCosto
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ReporteDashboardController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReporteDashboardController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Obtiene el resumen de KPIs principales del dashboard
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>ReporteDashboardResumenDTO con los KPIs</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerResumen(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerResumenAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardEstadoDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerResumenPorEstado(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerResumenPorEstadoAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="estado">Estado para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardModalidadDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerResumenPorModalidad(int? anio, string? estado, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerResumenPorModalidadAsync(anio, estado, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        /// <param name="estado">Estado para filtrar (opcional)</param>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="fechaInicio">Fecha inicio para filtrar (opcional)</param>
        /// <param name="fechaFin">Fecha fin para filtrar (opcional)</param>
        /// <param name="modalidad">Modalidad para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardProgramaDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerProgramasPorEstado(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerProgramasPorEstadoAsync(estado, anio, fechaInicio, fechaFin, modalidad, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        /// <param name="fecha">Fecha especifica (opcional)</param>
        /// <param name="fechaInicio">Fecha inicio (opcional)</param>
        /// <param name="fechaFin">Fecha fin (opcional)</param>
        /// <param name="idProgramaPadre">Id del programa padre (opcional)</param>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardCursoDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleCursos(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerDetalleCursosAsync(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idDocente">Id del docente (opcional)</param>
        /// <param name="estado">Estado para filtrar (opcional)</param>
        /// <param name="soloActivos">Solo docentes activos (opcional, default false)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardDocenteDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerDocentesAsignados(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerDocentesAsignadosAsync(anio, idDocente, estado, soloActivos, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardGraficoPorMesDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerGraficoPorMes(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerGraficoPorMesAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene los valores disponibles para los filtros del dashboard
        /// </summary>
        /// <returns>ReporteDashboardFiltrosDTO con los valores de los combos</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerFiltros()
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerFiltrosAsync();
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene todos los datos del dashboard con filtros aplicados
        /// </summary>
        /// <param name="filtro">Objeto con los filtros a aplicar</param>
        /// <returns>Lista de ReporteDashboardCompletoDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ObtenerDatosCompletos([FromBody] ReporteDashboardFiltroRequestDTO filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerDatosCompletosAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene resumen semanal de sesiones
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="mesInicio">Mes inicio para filtrar (opcional)</param>
        /// <param name="mesFin">Mes fin para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardSemanalDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerResumenSemanal(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerResumenSemanalAsync(anio, mesInicio, mesFin, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="semanaInicio">Semana inicio (opcional)</param>
        /// <param name="semanaFin">Semana fin (opcional)</param>
        /// <param name="mes">Mes para filtrar (opcional)</param>
        /// <returns>Lista de ReporteDashboardCalendarioDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerSesionesCalendario(int? anio, int? semanaInicio, int? semanaFin, int? mes)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerSesionesCalendarioAsync(anio, semanaInicio, semanaFin, mes);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// Estados: Ejecutada, Cancelada, Por-Reprogramar, Adicional, Por Ejecutar, No Aplica, Recuperada
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardEstadoSesionDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerResumenPorEstadoSesion(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerResumenPorEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idEstadoSesion">Id del estado de sesion (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardSesionDetalleDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerSesionesPorEstado(int? anio, int? idEstadoSesion, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerSesionesPorEstadoAsync(anio, idEstadoSesion, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>Lista de ReporteDashboardEvolucionEstadoSesionDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerEvolucionEstadoSesion(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerEvolucionEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        /// <param name="anio">Anio para filtrar (opcional)</param>
        /// <param name="idProgramaEspecificoPadre">Id del programa especifico padre (opcional)</param>
        /// <param name="idCentroCostoPadre">Centro de costo padre (opcional)</param>
        /// <returns>ReporteDashboardKPIsEstadoSesionDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerKPIsEstadoSesion(int? anio, int? idProgramaEspecificoPadre, int? idCentroCostoPadre)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerKPIsEstadoSesionAsync(anio, idProgramaEspecificoPadre, idCentroCostoPadre);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene cambios de estado de programas basados en log
        /// Transiciones: Lanzamiento->Ejecucion, Ejecucion->Concluido, *->Cancelado, agrupados por semana
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCambiosEstado(int? ultimasSemanas)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerCambiosEstadoAsync(ultimasSemanas);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene estados de programas hijo agrupados por dia o semana
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerEstadosPorDia(
            string? idsPEspecificoHijo,
            string? estados,
            string? agrupacion,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? ultimasSemanas)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerEstadosPorDiaAsync(idsPEspecificoHijo, estados, agrupacion, fechaInicio, fechaFin, ultimasSemanas);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos V3 con modalidad clasificada (Inhouse/Presencial/Online)
        /// y filtro por semana
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerDetalleCursosV3(
            DateTime? fecha,
            DateTime? fechaInicio,
            DateTime? fechaFin,
            int? idProgramaPadre,
            int? anio,
            int? idCentroCostoPadre,
            string? modalidadClasificada,
            int? semanaInicio,
            int? semanaFin)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerDetalleCursosV3Async(fecha, fechaInicio, fechaFin, idProgramaPadre, anio, idCentroCostoPadre, modalidadClasificada, semanaInicio, semanaFin);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene seguimiento de clases por dia de semana (Lunes-Sabado)
        /// con filtro propio: estado de programa y rango de fechas/semanas
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ObtenerSeguimientoClases([FromBody] ReporteDashboardSeguimientoFiltroRequestDTO filtro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerSeguimientoClasesAsync(filtro);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────

        /// <summary>
        /// Retorna lista de docentes (fin.T_Proveedor EsDocente=1) para filtro con busqueda
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerDocentesFiltro(string? busqueda)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerDocentesFiltroAsync(busqueda);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna lista de programas especificos (padre e hijo) para filtro con busqueda
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPEspecificoFiltro(string? busqueda)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerPEspecificoFiltroAsync(busqueda);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna programas especificos donde el docente (idProveedor) tiene sesiones asignadas
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerPEspecificoPorDocente(int idProveedor)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerPEspecificoPorDocenteAsync(idProveedor);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna seguimiento de sesiones de un docente (KPIs + resumen por programa + detalle sesiones)
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerSeguimientoDocente(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerSeguimientoDocenteAsync(idDocente, idPEspecifico, anio, fechaInicio, fechaFin);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retorna notas de alumnos calculadas (Tareas/Asistencia/PromedioFinal) por PEspecifico
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerNotasPorPEspecifico(int idPEspecifico, int grupo = 1)
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerNotasPorPEspecificoAsync(idPEspecifico, grupo);
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Obtiene FURs del area 19, tipo PO, estados 3 y 5 para Dashboard 3
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ObtenerFursDashboard3()
        {
            try
            {
                IReporteDashboardService service = new ReporteDashboardService(_unitOfWork);
                var resultado = await service.ObtenerFursDashboard3Async();
                return Ok(resultado);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
