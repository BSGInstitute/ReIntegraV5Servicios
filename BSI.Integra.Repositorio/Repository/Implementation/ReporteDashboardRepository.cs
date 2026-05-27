using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Infrastructure;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.Repository.Interface;
using Dapper;
using Newtonsoft.Json;
using System.Data;

namespace BSI.Integra.Repositorio.Repository.Implementation
{
    /// <summary>
    /// Repositorio para el Dashboard de Programas de Capacitacion
    /// Autor: Marco Villanueva Torres
    /// Fecha: 2025-04-17
    /// Modificacion: 2025-04-18 - Agregados filtros de PEspecifico y CentroCosto, uso de SPs V2
    /// </summary>
    public class ReporteDashboardRepository : GenericRepository<TPespecifico>, IReporteDashboardRepository
    {
        public ReporteDashboardRepository(IntegraDBContext context, IConnectionFactory connectionFactory, IDapperRepository dapperRepository)
            : base(context, connectionFactory, dapperRepository)
        {
        }

        /// <summary>
        /// Obtiene el resumen de KPIs principales del dashboard
        /// </summary>
        public async Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? mes = null, int? semana = null, string? modalidad = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryFirstOrDefaultAsync<ReporteDashboardResumenDTO>(
                    "pla.SP_ReporteDashboardResumenGeneral",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        Modalidad              = modalidad,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado ?? new ReporteDashboardResumenDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        public async Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEstadoDTO>(
                    "pla.SP_ReporteDashboard_ObtenerResumenPorEstado",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        public async Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardModalidadDTO>(
                    "pla.SP_ReporteDashboard_ObtenerResumenPorModalidad",
                    new { Anio = anio, Estado = estado, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorModalidadAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        public async Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardProgramaDTO>(
                    "pla.SP_ReporteDashboard_ObtenerProgramasPorEstado",
                    new { Estado = estado, Anio = anio, FechaInicio = fechaInicio, FechaFin = fechaFin, Modalidad = modalidad, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProgramasPorEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        public async Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCursoDTO>(
                    "pla.SP_ReporteDashboard_ObtenerDetalleCursos",
                    new { Fecha = fecha, FechaInicio = fechaInicio, FechaFin = fechaFin, IdProgramaPadre = idProgramaPadre, Anio = anio, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDetalleCursosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        public async Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardDocenteDTO>(
                    "pla.SP_ReporteDashboard_ObtenerDocentesAsignados",
                    new { Anio = anio, IdDocente = idDocente, Estado = estado, SoloActivos = soloActivos, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDocentesAsignadosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardGraficoPorMesDTO>(
                    "pla.SP_ReporteDashboard_ObtenerGraficoPorMes",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGraficoPorMesAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los valores disponibles para los filtros del dashboard.
        /// Llama 5 SPs individuales con filas planas (sin FOR JSON PATH).
        /// </summary>
        public async Task<ReporteDashboardFiltrosDTO> ObtenerFiltrosAsync()
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var filtros = new ReporteDashboardFiltrosDTO();

                var anios = await conn.QueryAsync<int>(
                    "pla.SP_ReporteDashboard_ObtenerFiltroAnios",
                    commandType: CommandType.StoredProcedure);
                filtros.Anios = anios.Where(x => x > 0).ToList();

                var estados = await conn.QueryAsync<string>(
                    "pla.SP_ReporteDashboard_ObtenerFiltroEstados",
                    commandType: CommandType.StoredProcedure);
                filtros.Estados = estados.Where(x => !string.IsNullOrEmpty(x)).ToList();

                var modalidades = await conn.QueryAsync<string>(
                    "pla.SP_ReporteDashboard_ObtenerFiltroModalidades",
                    commandType: CommandType.StoredProcedure);
                filtros.Modalidades = modalidades.Where(x => !string.IsNullOrEmpty(x)).ToList();

                var programas = await conn.QueryAsync<ReporteDashboardProgramaEspecificoItemDTO>(
                    "pla.SP_ReporteDashboard_ObtenerFiltroProgramasPadre",
                    commandType: CommandType.StoredProcedure);
                filtros.ProgramasEspecificos = programas.Where(x => x.Id.HasValue && x.Id > 0).ToList();

                var centros = await conn.QueryAsync<ReporteDashboardCentroCostoItemDTO>(
                    "pla.SP_ReporteDashboard_ObtenerFiltroCentrosCosto",
                    commandType: CommandType.StoredProcedure);
                filtros.CentrosCosto = centros.Where(x => !string.IsNullOrEmpty(x.Nombre)).ToList();

                filtros.Areas ??= new List<string>();
                filtros.Ciudades ??= new List<string>();

                return filtros;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFiltrosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene todos los datos del dashboard con filtros aplicados
        /// </summary>
        public async Task<List<ReporteDashboardCompletoDTO>> ObtenerDatosCompletosAsync(ReporteDashboardFiltroRequestDTO filtro)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCompletoDTO>(
                    "pla.SP_ReporteDashboard_ObtenerDatosCompletos",
                    new { Anio = filtro.Anio, Estado = filtro.Estado, Modalidad = filtro.Modalidad, FechaInicio = filtro.FechaInicio, FechaFin = filtro.FechaFin, Area = filtro.Area, Ciudad = filtro.Ciudad, IdProgramaEspecificoPadre = filtro.IdProgramaEspecificoPadre, IdCentroCostoPadre = filtro.IdCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosCompletosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene resumen semanal de sesiones
        /// </summary>
        public async Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mes = null, int? semana = null, int? diaMes = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardSemanalDTO>(
                    "pla.SP_ReporteDashboardObtenerResumenSemanal",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        FechaHoraInicio_DiaMes = diaMes,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenSemanalAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene datos de sesiones para vista de calendario
        /// </summary>
        public async Task<List<ReporteDashboardCalendarioDTO>> ObtenerSesionesCalendarioAsync(int? anio, int? semanaInicio, int? semanaFin, int? mes)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCalendarioDTO>(
                    "pla.SP_ReporteDashboard_ObtenerSesionesCalendario",
                    new { Anio = anio, SemanaInicio = semanaInicio, SemanaFin = semanaFin, Mes = mes },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSesionesCalendarioAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene resumen de sesiones agrupadas por estado de sesion
        /// </summary>
        public async Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEstadoSesionDTO>(
                    "pla.SP_ReporteDashboardObtenerResumenPorEstadoSesion",
                    new
                    {
                        FechaHoraInicio_Inicio = fechaInicio.HasValue ? (object)fechaInicio.Value.Date : null,
                        FechaHoraInicio_Fin    = fechaFin.HasValue    ? (object)fechaFin.Value.Date    : null,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorEstadoSesionAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene detalle de sesiones filtradas por estado
        /// </summary>
        public async Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardSesionDetalleDTO>(
                    "pla.SP_ReporteDashboardObtenerSesionesPorEstado",
                    new
                    {
                        FechaHoraInicio_Inicio = fechaInicio.HasValue ? (object)fechaInicio.Value.Date : null,
                        FechaHoraInicio_Fin    = fechaFin.HasValue    ? (object)fechaFin.Value.Date    : null,
                        IdPEspecificoSesionEstado = idEstadoSesion,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSesionesPorEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de estados de sesion
        /// </summary>
        public async Task<List<ReporteDashboardEvolucionEstadoSesionDTO>> ObtenerEvolucionEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEvolucionEstadoSesionDTO>(
                    "pla.SP_ReporteDashboard_ObtenerEvolucionEstadoSesion",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerEvolucionEstadoSesionAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene KPIs de estados de sesion
        /// </summary>
        public async Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(DateTime? fechaInicio = null, DateTime? fechaFin = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryFirstOrDefaultAsync<ReporteDashboardKPIsEstadoSesionDTO>(
                    "pla.SP_ReporteDashboardObtenerKPIsEstadoSesion",
                    new
                    {
                        FechaHoraInicio_Inicio = fechaInicio.HasValue ? (object)fechaInicio.Value.Date : null,
                        FechaHoraInicio_Fin    = fechaFin.HasValue    ? (object)fechaFin.Value.Date    : null,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado ?? new ReporteDashboardKPIsEstadoSesionDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerKPIsEstadoSesionAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene cambios de estado de programas basado en log
        /// </summary>
        public async Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? anio = null, int? mes = null, int? semanaDesde = null, int? semanaHasta = null, int? diaMes = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCambioEstadoDTO>(
                    "pla.SP_ReporteDashboardObtenerCambiosEstado",
                    new
                    {
                        FechaHoraInicio_Anio        = anio,
                        FechaHoraInicio_Mes         = mes,
                        FechaHoraInicio_SemanaDesde = semanaDesde,
                        FechaHoraInicio_SemanaHasta = semanaHasta,
                        FechaHoraInicio_DiaMes      = diaMes
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCambiosEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene estados de programas hijo agrupados por dia o semana
        /// </summary>
        public async Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? anos = null, string? meses = null, int? semanaDesde = null, int? semanaHasta = null, int? diaMes = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEstadoPorDiaDTO>(
                    "pla.SP_ReporteDashboardObtenerEstadosPorDia",
                    new
                    {
                        FechaHoraInicio_AnioLista   = string.IsNullOrWhiteSpace(anos) ? null : anos.Trim(),
                        FechaHoraInicio_MesLista    = string.IsNullOrWhiteSpace(meses) ? null : meses.Trim(),
                        FechaHoraInicio_SemanaDesde = semanaDesde,
                        FechaHoraInicio_SemanaHasta = semanaHasta,
                        FechaHoraInicio_DiaMes      = diaMes,
                        IdPEspecifico_HijoLista     = idsPEspecificoHijo,
                        EstadoPEspecifico_Lista     = estados
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerEstadosPorDiaAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos V3 con modalidad clasificada
        /// El SP devuelve filas normales (sin FOR JSON PATH) para evitar nulls con window functions
        /// </summary>
        public async Task<List<ReporteDashboardCursoV3DTO>> ObtenerDetalleCursosV3Async(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, int? idCentroCostoPadre, string? modalidadClasificada, int? semanaInicio, int? semanaFin)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCursoV3DTO>(
                    "pla.SP_ReporteDashboard_ObtenerDetalleCursosClasificado",
                    new
                    {
                        Fecha = fecha,
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        IdProgramaPadre = idProgramaPadre,
                        Anio = anio,
                        IdCentroCostoPadre = idCentroCostoPadre,
                        ModalidadClasificada = modalidadClasificada,
                        SemanaInicio = semanaInicio,
                        SemanaFin = semanaFin
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDetalleCursosV3Async: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene seguimiento de clases por dia de semana
        /// </summary>
        public async Task<List<ReporteDashboardSeguimientoClaseDTO>> ObtenerSeguimientoClasesAsync(ReporteDashboardSeguimientoFiltroRequestDTO filtro)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardSeguimientoClaseDTO>(
                    "pla.SP_ReporteDashboardObtenerSeguimientoClases",
                    new
                    {
                        FechaHoraInicio_Inicio     = filtro.FechaInicio,
                        FechaHoraInicio_Fin        = filtro.FechaFin,
                        EstadoCurso                = filtro.EstadoCurso,
                        FechaHoraInicio_Anio       = filtro.Anio,
                        FechaHoraInicio_SemanaInicio = filtro.SemanaInicio,
                        FechaHoraInicio_SemanaFin  = filtro.SemanaFin,
                        Modalidad                  = filtro.Modalidad,
                        IdPEspecifico_Padre        = filtro.IdProgramaPadre,
                        IdPEspecifico              = filtro.IdCurso
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSeguimientoClasesAsync: {ex.Message}");
            }
        }

        // ── Dashboard 2: Seguimiento por Docente ─────────────────────────────

        public async Task<List<ReporteDashboardDocenteFiltroDTO>> ObtenerDocentesFiltroAsync(string? busqueda)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardDocenteFiltroDTO>(
                    "pla.SP_ReporteDashboard_ObtenerDocentesFiltro",
                    new { Busqueda = busqueda },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDocentesFiltroAsync: {ex.Message}");
            }
        }

        public async Task<List<ReporteDashboardPEspecificoFiltroDTO>> ObtenerPEspecificoFiltroAsync(string? busqueda)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardPEspecificoFiltroDTO>(
                    "pla.SP_ReporteDashboard_ObtenerPEspecificoFiltro",
                    new { Busqueda = busqueda },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoFiltroAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene cursos (programas especificos hijo) filtrando opcionalmente por programa padre
        /// </summary>
        public async Task<List<ReporteDashboardCursoFiltroDTO>> ObtenerCursosPorProgramaAsync(int? idProgramaPadre, string? busqueda)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCursoFiltroDTO>(
                    "pla.SP_ReporteDashboard_ObtenerCursosPorPrograma",
                    new { IdProgramaPadre = idProgramaPadre, Busqueda = busqueda },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCursosPorProgramaAsync: {ex.Message}");
            }
        }

        public async Task<List<ReporteDashboardPGeneralFiltroDTO>> ObtenerPGeneralesFiltroAsync(string? busqueda = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardPGeneralFiltroDTO>(
                    "pla.SP_ReporteDashboard_ObtenerPGeneralesFiltro",
                    new { Busqueda = string.IsNullOrWhiteSpace(busqueda) ? null : busqueda.Trim() },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPGeneralesFiltroAsync: {ex.Message}");
            }
        }

        public async Task<List<ReporteDashboardPEspecificoFiltroDTO>> BuscarPEspecificosPorFiltroNotasAsync(
            string? filtroEstadoNotas = null,
            string? filtroCentroCosto = null,
            DateTime? filtroFechaDesde = null,
            DateTime? filtroFechaHasta = null,
            int? idDocente = null,
            string? codigoMatricula = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardPEspecificoFiltroDTO>(
                    "pla.SP_ReporteDashboard_BuscarPEspecificosPorFiltroNotas",
                    new
                    {
                        FiltroEstadoNotas = string.IsNullOrWhiteSpace(filtroEstadoNotas) ? null : filtroEstadoNotas.Trim(),
                        FiltroCentroCosto = string.IsNullOrWhiteSpace(filtroCentroCosto) ? null : filtroCentroCosto.Trim(),
                        FiltroFechaDesde  = filtroFechaDesde.HasValue ? filtroFechaDesde.Value.Date : (DateTime?)null,
                        FiltroFechaHasta  = filtroFechaHasta.HasValue ? filtroFechaHasta.Value.Date : (DateTime?)null,
                        IdDocente         = idDocente,
                        CodigoMatricula   = string.IsNullOrWhiteSpace(codigoMatricula) ? null : codigoMatricula.Trim()
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en BuscarPEspecificosPorFiltroNotasAsync: {ex.Message}");
            }
        }

        public async Task<ReporteDashboardCalificacionAlumnosDTO> ObtenerCalificacionAlumnosAsync(
            string?   filtroEstadoNotas   = null,
            int?      idCentroCosto       = null,
            DateTime? fechaTermino_Inicio = null,
            DateTime? fechaTermino_Fin    = null,
            int?      idProveeedor        = null,
            string?   codigoMatricula     = null,
            string?   idsPEspecifico      = null,
            int       grupo               = 1,
            int       pagina              = 1,
            int       tamanoPagina        = 20)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;

                var paginaNorm     = pagina < 1 ? 1 : pagina;
                var tamanoPaginaN  = tamanoPagina < 1 ? 20 : tamanoPagina;
                var idsPEspecificN = string.IsNullOrWhiteSpace(idsPEspecifico) ? null : idsPEspecifico.Trim();
                var codigoMatN     = string.IsNullOrWhiteSpace(codigoMatricula) ? null : codigoMatricula.Trim();

                int? idMatriculaCabecera = null;
                if (codigoMatN != null)
                {
                    idMatriculaCabecera = await conn.QueryFirstOrDefaultAsync<int?>(
                        @"SELECT TOP 1 Id
                          FROM   fin.T_MatriculaCabecera WITH (NOLOCK)
                          WHERE  CodigoMatricula = @Codigo AND Estado = 1",
                        new { Codigo = codigoMatN }
                    );
                    if (idMatriculaCabecera == null)
                        return new ReporteDashboardCalificacionAlumnosDTO
                        {
                            TotalRegistros = 0,
                            Pagina         = paginaNorm,
                            TamanoPagina   = tamanoPaginaN,
                            Programas      = new List<ReporteDashboardNotasPorPEspecificoDTO>()
                        };
                }

                using var sp1 = await conn.QueryMultipleAsync(
                    "pla.SP_ReporteDashboardFiltrarProgramasCalificacion",
                    new
                    {
                        FiltroEstadoNotas        = string.IsNullOrWhiteSpace(filtroEstadoNotas) ? null : filtroEstadoNotas.Trim(),
                        IdCentroCosto            = idCentroCosto,
                        FechaTermino_Inicio      = fechaTermino_Inicio.HasValue ? fechaTermino_Inicio.Value.Date : (DateTime?)null,
                        FechaTermino_Fin         = fechaTermino_Fin.HasValue    ? fechaTermino_Fin.Value.Date    : (DateTime?)null,
                        IdProveeedor             = idProveeedor,
                        IdMatriculaCabecera      = idMatriculaCabecera,
                        IdPEspecifico_PadreLista = idsPEspecificN,
                        Grupo                    = grupo,
                        Pagina                   = paginaNorm,
                        TamanoPagina             = tamanoPaginaN,
                    },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 60
                );

                var totalRow       = await sp1.ReadFirstOrDefaultAsync<dynamic>();
                int totalRegistros = totalRow != null ? (int)totalRow.TotalRegistros : 0;

                var idsPagina = (await sp1.ReadAsync<ReporteDashboardCalificacionFiltroResultDTO>()).ToList();

                var resultado = new ReporteDashboardCalificacionAlumnosDTO
                {
                    TotalRegistros = totalRegistros,
                    Pagina         = paginaNorm,
                    TamanoPagina   = tamanoPaginaN,
                };

                if (!idsPagina.Any())
                    return resultado;

                var idsPaginaCsv = string.Join(",", idsPagina.Select(x => x.IdPEspecifico));

                using var sp2 = await conn.QueryMultipleAsync(
                    "pla.SP_ReporteDashboardObtenerDatosProgramasCalificacion",
                    new { IdPEspecifico_PaginaLista = idsPaginaCsv, GrupoCurso = grupo },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 60
                );

                var programasRaw = (await sp2.ReadAsync<ReporteDashboardCalificacionProgramaRawDTO>()).ToList();
                var criteriosRaw = (await sp2.ReadAsync<ReporteDashboardCalificacionCriterioRawDTO>()).ToList();

                var alumnosRaw = (await conn.QueryAsync<ReporteDashboardCalificacionAlumnoRawDTO>(
                    "pla.SP_ReporteDashboardObtenerAlumnosCalificacion",
                    new
                    {
                        IdsPEspecifico_Lista = idsPaginaCsv,
                        IdMatriculaCabecera  = idMatriculaCabecera,
                        Grupo                = grupo,
                    },
                    commandType: CommandType.StoredProcedure,
                    commandTimeout: 60
                )).ToList();

                foreach (var prog in programasRaw)
                {
                    var criteriosProg = criteriosRaw
                        .Where(c => c.IdPEspecifico == prog.IdPEspecifico)
                        .OrderBy(c => c.Orden)
                        .Select(c => new ReporteDashboardNotaEvaluacionDTO
                        {
                            Id         = c.IdEvaluacion,
                            Nombre     = c.NombreCriterio,
                            Porcentaje = c.Porcentaje
                        }).ToList();

                    decimal sumaPonderacionesEval = criteriosProg.Sum(e => e.Porcentaje);
                    decimal ponderacionFaltanteEval = 100 - sumaPonderacionesEval;

                    if (ponderacionFaltanteEval > 0)
                    {
                        var criterioTareasEval = criteriosProg.FirstOrDefault(e => e.Nombre.Contains("Tareas"));
                        if (criterioTareasEval != null)
                        {
                            criterioTareasEval.Porcentaje += ponderacionFaltanteEval;
                        }
                    }

                    var alumnosProg = alumnosRaw
                        .Where(a => a.IdPEspecifico == prog.IdPEspecifico)
                        .Select(a =>
                        {
                            var notasCriterio = criteriosProg.Select((evl, idx) =>
                            {
                                decimal nota = idx switch
                                {
                                    0 => a.NotaCriterio1 ?? 0,
                                    1 => a.NotaCriterio2 ?? 0,
                                    2 => a.NotaCriterio3 ?? 0,
                                    3 => a.NotaCriterio4 ?? 0,
                                    4 => a.NotaCriterio5 ?? 0,
                                    _ => 0
                                };
                                return new ReporteDashboardNotaCriterioDTO
                                {
                                    IdEvaluacion   = evl.Id,
                                    NombreCriterio = evl.Nombre,
                                    Porcentaje     = evl.Porcentaje,
                                    Nota           = nota
                                };
                            }).ToList();

                            decimal sumaPonderaciones = notasCriterio.Sum(n => n.Porcentaje);
                            decimal ponderacionFaltante = 100 - sumaPonderaciones;

                            if (ponderacionFaltante > 0)
                            {
                                var criterioTareas = notasCriterio.FirstOrDefault(n => n.NombreCriterio.Contains("Tareas"));
                                if (criterioTareas != null)
                                {
                                    criterioTareas.Porcentaje += ponderacionFaltante;
                                }
                            }
                            decimal promedio = notasCriterio.Sum(n => n.Nota * n.Porcentaje / 100);

                            return new ReporteDashboardNotaAlumnoDTO
                            {
                                IdMatriculaCabecera   = a.IdMatriculaCabecera,
                                CodigoMatricula       = a.CodigoMatricula,
                                Alumno                = a.NombreAlumno,
                                CoordinadoraAcademica = a.CoordinadoraAcademica,
                                CentroCosto           = a.CentroCosto,
                                Curso                 = a.Curso,
                                EstadoMatricula       = a.EstadoMatricula,
                                Notas                 = notasCriterio,
                                PromedioFinal         = Math.Round(promedio, 0, MidpointRounding.AwayFromZero),
                                EstadoNotaAlumno      = a.EstadoNotaAlumno,
                                ObservacionChatbot    = a.ObservacionChatbot,
                            };
                        }).ToList();

                    resultado.Programas.Add(new ReporteDashboardNotasPorPEspecificoDTO
                    {
                        Programa = new ReporteDashboardNotaProgramaDTO
                        {
                            IdPEspecifico          = prog.IdPEspecifico,
                            Programa               = prog.Programa,
                            EstadoPrograma         = prog.EstadoPrograma,
                            ModalidadPrograma      = prog.ModalidadPrograma,
                            CentroCostoProg        = prog.CentroCostoProg,
                            Curso                  = prog.Programa,
                            FechaInicio            = prog.FechaInicio,
                            FechaTermino           = prog.FechaTermino,
                            EstadoRegistroNotas    = prog.EstadoRegistroNotas,
                            SubEstadoRegistroNotas = prog.SubEstadoRegistroNotas,
                            Docente                = prog.Docente,
                            Coordinadora           = prog.Coordinadora,
                            ObservacionChatbot     = prog.ObservacionChatbot,
                            ConteoChatbot          = prog.ConteoChatbot ?? 0,
                            FechaRegistroNota      = prog.FechaRegistroNota,
                            PlazoCalificacion      = prog.PlazoCalificacion,
                            TotalAlumnos           = prog.TotalAlumnos,
                            TotalCriterios         = prog.TotalCriterios,
                            AlumnosConNotaCompleta = prog.AlumnosConNota,
                        },
                        Evaluaciones = criteriosProg,
                        Alumnos      = alumnosProg,
                        EsOnline     = false,
                    });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerCalificacionAlumnosAsync: {ex.Message}");
            }
        }

        public async Task<List<ReporteDashboardPaisFiltroDTO>> ObtenerPaisesFiltroAsync()
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardPaisFiltroDTO>(
                    @"SELECT P.Id, P.NombrePais AS Nombre
                      FROM CONF.T_Pais P WITH (NOLOCK)
                      INNER JOIN CONF.T_RegionCiudad RC WITH (NOLOCK) ON RC.IdPais = P.Id AND RC.Estado = 1
                      INNER JOIN pla.T_PEspecifico PE WITH (NOLOCK) ON PE.IdCiudad = RC.CodigoBS AND PE.Estado = 1
                      WHERE P.Estado = 1
                      GROUP BY P.Id, P.NombrePais
                      ORDER BY P.NombrePais"
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPaisesFiltroAsync: {ex.Message}");
            }
        }

        public async Task<ReporteDashboardSeguimientoDocenteDTO> ObtenerSeguimientoDocenteAsync(int? idDocente, int? idPEspecifico, int? anio, DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                using var multi = await conn.QueryMultipleAsync(
                    "pla.SP_ReporteDashboard_ObtenerSeguimientoDocente",
                    new { IdDocente = idDocente, IdPEspecifico = idPEspecifico, Anio = anio, FechaInicio = fechaInicio, FechaFin = fechaFin },
                    commandType: CommandType.StoredProcedure
                );

                var resultado = new ReporteDashboardSeguimientoDocenteDTO
                {
                    KPIs = (await multi.ReadFirstOrDefaultAsync<ReporteDashboardSeguimientoDocenteKPIsDTO>()) ?? new ReporteDashboardSeguimientoDocenteKPIsDTO(),
                    Programas = (await multi.ReadAsync<ReporteDashboardSeguimientoDocenteProgramaDTO>()).ToList(),
                    Sesiones = (await multi.ReadAsync<ReporteDashboardSeguimientoDocenteSesionDTO>()).ToList()
                };
                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSeguimientoDocenteAsync: {ex.Message}");
            }
        }

        public async Task<List<ReporteDashboardPEspecificoPorDocenteDTO>> ObtenerPEspecificoPorDocenteAsync(int idProveedor)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                const string sql = @"
                    SELECT PE.Id, PE.Nombre
                    FROM pla.T_PEspecifico AS PE
                    INNER JOIN (
                        SELECT DISTINCT IdPEspecifico
                        FROM pla.T_PEspecificoSesion
                        WHERE IdProveedor = @IdProveedor
                    ) AS PES ON PES.IdPEspecifico = PE.Id
                    WHERE PE.Estado = 1
                    ORDER BY PE.Nombre";

                var resultado = await conn.QueryAsync<ReporteDashboardPEspecificoPorDocenteDTO>(
                    sql, new { IdProveedor = idProveedor });
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerPEspecificoPorDocenteAsync: {ex.Message}");
            }
        }

        public async Task<ReporteDashboardNotasPorPEspecificoDTO> ObtenerNotasPorPEspecificoAsync(
            int idPEspecifico, int grupo,
            string? filtroPGeneral    = null,
            string? filtroEstadoNotas = null,
            string? filtroCodigoMat   = null,
            string? filtroCentroCosto = null,
            DateTime? filtroFechaDesde = null,
            DateTime? filtroFechaHasta = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;

                // ── Determinar modo presencial / online ──────────────────────────────
                var evaluacionesPresencial = (await conn.QueryAsync<ReporteDashboardNotaEvaluacionRawDTO>(
                    @"SELECT Id, Nombre, Porcentaje
                      FROM   ope.T_Evaluacion
                      WHERE  IdPespecifico = @Id AND Grupo = @Grupo AND Aprobado = 1 AND Estado = 1",
                    new { Id = idPEspecifico, Grupo = grupo }
                )).ToList();

                bool esOnline = !evaluacionesPresencial.Any();

                // ── RS1: datos del programa (SP nuevo) ───────────────────────────────
                var programaRaw = await conn.QueryFirstOrDefaultAsync<ReporteDashboardNotaProgramaRawDTO>(
                    "pla.SP_ReporteDashboard_ObtenerNotasProgramaRS1",
                    new
                    {
                        IdPEspecifico     = idPEspecifico,
                        Grupo             = grupo,
                        FiltroPGeneral    = string.IsNullOrWhiteSpace(filtroPGeneral)    ? null : filtroPGeneral.Trim(),
                        FiltroEstadoNotas = string.IsNullOrWhiteSpace(filtroEstadoNotas) ? null : filtroEstadoNotas.Trim(),
                        FiltroCentroCosto = string.IsNullOrWhiteSpace(filtroCentroCosto) ? null : filtroCentroCosto.Trim(),
                        FiltroFechaDesde  = filtroFechaDesde.HasValue ? filtroFechaDesde.Value.Date : (DateTime?)null,
                        FiltroFechaHasta  = filtroFechaHasta.HasValue ? filtroFechaHasta.Value.Date : (DateTime?)null,
                    },
                    commandType: CommandType.StoredProcedure
                );

                var programa = programaRaw == null ? null : new ReporteDashboardNotaProgramaDTO
                {
                    IdPEspecifico          = programaRaw.IdPEspecifico,
                    Programa               = programaRaw.Programa,
                    EstadoPrograma         = programaRaw.EstadoPrograma,
                    ModalidadPrograma      = programaRaw.ModalidadPrograma,
                    CentroCostoProg        = programaRaw.CentroCostoProg,
                    Curso                  = programaRaw.Programa,
                    FechaInicio            = programaRaw.FechaInicio,
                    FechaTermino           = programaRaw.FechaTermino,
                    EstadoRegistroNotas    = programaRaw.EstadoRegistroNotas,
                    SubEstadoRegistroNotas = programaRaw.SubEstadoRegistroNotas,
                    Docente                = programaRaw.Docente,
                    Coordinadora           = programaRaw.Coordinadora,
                    ObservacionChatbot     = programaRaw.ObservacionChatbot,
                    ConteoChatbot          = programaRaw.ConteoChatbot,
                    FechaRegistroNota      = programaRaw.FechaRegistroNota,
                    PlazoCalificacion      = programaRaw.PlazoCalificacion,
                    TotalAlumnos           = programaRaw.TotalAlumnos,
                    TotalCriterios         = programaRaw.TotalCriterios,
                    AlumnosConNotaCompleta = programaRaw.AlumnosConNotaCompleta,
                };

                // ── Criterios/evaluaciones (encabezados de columnas) ─────────────────
                List<ReporteDashboardNotaEvaluacionRawDTO> evaluaciones;
                if (!esOnline)
                {
                    evaluaciones = evaluacionesPresencial;
                }
                else
                {
                    evaluaciones = (await conn.QueryAsync<ReporteDashboardNotaEvaluacionRawDTO>(
                        @"SELECT Id, Nombre, Ponderacion AS Porcentaje
                          FROM   pw.V_PW_ObtenerCriteriosPorProgramaEspecifico
                          WHERE  IdPespecifico = @Id",
                        new { Id = idPEspecifico }
                    )).ToList();
                }

                // ── Notas y detalle (lógica existente intacta) ───────────────────────
                List<ReporteDashboardNotaRawDTO> notas;
                List<ReporteDashboardNotaDetalleRawDTO> notasDetalle;

                if (!esOnline)
                {
                    var ids = evaluaciones.Select(e => e.Id).ToList();
                    notas = ids.Any()
                        ? (await conn.QueryAsync<ReporteDashboardNotaRawDTO>(
                            @"SELECT IdMatriculaCabecera, IdEvaluacion AS IdCriterioEvaluacion, Nota
                              FROM   ope.T_Nota
                              WHERE  IdEvaluacion IN @Ids AND Estado = 1",
                            new { Ids = ids }
                          )).ToList()
                        : new List<ReporteDashboardNotaRawDTO>();

                    notasDetalle = new List<ReporteDashboardNotaDetalleRawDTO>();
                }
                else
                {
                    // SP retorna columna 'IdEvaluacion'; mapeamos manualmente a IdCriterioEvaluacion
                    var notasRaw = (await conn.QueryAsync<dynamic>(
                        "pw.SP_PW_ObtenerNotaOnlinePorProgramaEspecificoV2",
                        new { IdPEspecifico = idPEspecifico },
                        commandType: CommandType.StoredProcedure
                    )).ToList();
                    notas = notasRaw.Select(r => new ReporteDashboardNotaRawDTO
                    {
                        IdMatriculaCabecera  = (int)r.IdMatriculaCabecera,
                        IdCriterioEvaluacion = (int)r.IdEvaluacion,
                        Nota                 = (decimal)r.Nota
                    }).ToList();

                    notasDetalle = (await conn.QueryAsync<ReporteDashboardNotaDetalleRawDTO>(
                        "pw.SP_PW_ObtenerDetalleNotaOnlinePorProgramaEspecificov2",
                        new { IdPEspecifico = idPEspecifico },
                        commandType: CommandType.StoredProcedure
                    )).ToList();
                }

                // ── RS2: alumnos con datos nuevos (SP nuevo) ─────────────────────────
                var alumnosRaw = (await conn.QueryAsync<ReporteDashboardNotaAlumnoRawDTO>(
                    "pla.SP_ReporteDashboard_ObtenerNotasAlumnosRS2",
                    new
                    {
                        IdPEspecifico    = idPEspecifico,
                        Grupo            = grupo,
                        EsOnline         = esOnline ? 1 : 0,
                        FiltroCodigoMat  = string.IsNullOrWhiteSpace(filtroCodigoMat) ? null : filtroCodigoMat.Trim(),
                    },
                    commandType: CommandType.StoredProcedure
                )).ToList();

                // Lookup rápido de datos nuevos por IdMatriculaCabecera
                var alumnosRawDict = alumnosRaw
                    .GroupBy(a => a.IdMatriculaCabecera)
                    .ToDictionary(g => g.Key, g => g.First());

                // ── Matriculas (para mantener compatibilidad con lógica de notas) ────
                var matriculas = (await conn.QueryAsync<ReporteDashboardMatriculaRawDTO>(
                    @"SELECT MIN(IdMatriculaIntegra) AS IdMatriculaIntegra,
                             IdMatriculaCabecera, IdPEspecifico, CodigoMatricula, GrupoCurso, Alumno
                      FROM   pw.V_PW_MatriculasActivas_PorPEspecificoV2
                      WHERE  IdPespecifico = @Id AND GrupoCurso = @Grupo AND SoloSeguimientoAtc = 0
                      GROUP  BY IdMatriculaCabecera, IdPEspecifico, CodigoMatricula, GrupoCurso, Alumno
                      ORDER  BY Alumno",
                    new { Id = idPEspecifico, Grupo = grupo }
                )).ToList();

                // ── Sesiones y asistencias (lógica existente intacta) ────────────────
                var sesiones = (await conn.QueryAsync<ReporteDashboardSesionRawDTO>(
                    @"SELECT Id AS IdPEspecificoSesion
                      FROM   pla.T_PEspecificoSesion
                      WHERE  IdPespecifico = @Id AND Grupo = @Grupo AND Estado = 1",
                    new { Id = idPEspecifico, Grupo = grupo }
                )).ToList();

                List<ReporteDashboardAsistenciaRawDTO> asistencias;
                var sesionIds = sesiones.Select(s => s.IdPEspecificoSesion).ToList();
                if (sesionIds.Any())
                {
                    asistencias = (await conn.QueryAsync<ReporteDashboardAsistenciaRawDTO>(
                        @"SELECT IdPEspecificoSesion, IdMatriculaCabecera, Asistio
                          FROM   ope.T_Asistencia
                          WHERE  IdPEspecificoSesion IN @Ids AND Estado = 1",
                        new { Ids = sesionIds }
                    )).ToList();
                }
                else
                {
                    asistencias = new List<ReporteDashboardAsistenciaRawDTO>();
                }

                // ── Escala de calificacion (lógica existente intacta) ────────────────
                decimal escalaGlobal = 100;
                if (!esOnline)
                {
                    var cc = await conn.QueryFirstOrDefaultAsync<dynamic>(
                        @"SELECT TOP 1 CentroCosto
                          FROM   pla.V_DatosCentroCostoPorIdPEspecifico
                          WHERE  IdPEspecifico = @Id",
                        new { Id = idPEspecifico }
                    );
                    if (cc != null)
                    {
                        var escalas = (await conn.QueryAsync<dynamic>(
                            @"SELECT CodigoCiudad, EscalaCalificacion
                              FROM   ope.T_EvaluacionEscalaCalificacion
                              WHERE  IdModalidadCurso = 0 AND Estado = 1
                              ORDER  BY Id"
                        )).ToList();

                        string centroCosto = (string)(cc.CentroCosto ?? "");
                        foreach (var escala in escalas)
                        {
                            string codigo = (string)(escala.CodigoCiudad ?? "");
                            if (!string.IsNullOrEmpty(codigo) && centroCosto.Contains(codigo))
                                escalaGlobal = (decimal)escala.EscalaCalificacion;
                        }
                    }
                    if (escalaGlobal <= 0) escalaGlobal = 100;
                }

                int totalSesiones = sesiones.Count;

                // ── Encabezados de columnas ──────────────────────────────────────────
                var encabezados = evaluaciones.Select(e => new ReporteDashboardNotaEvaluacionDTO
                {
                    Id = e.Id,
                    Nombre = (e.Nombre ?? "").Replace("Portal-", "").Replace("Portal -", ""),
                    Porcentaje = e.Porcentaje
                }).ToList();

                // ── Calcular nota por alumno (lógica existente + campos nuevos) ──────
                var alumnos = new List<ReporteDashboardNotaAlumnoDTO>();
                foreach (var m in matriculas)
                {
                    var notasCriterio = new List<ReporteDashboardNotaCriterioDTO>();
                    decimal promedioFinal = 0;

                    foreach (var evl in evaluaciones)
                    {
                        decimal nota = 0;
                        string nombreSinPortal = (evl.Nombre ?? "").Replace("Portal-", "").Replace("Portal -", "");
                        bool esAsistencia = nombreSinPortal.Equals("ASISTENCIA", StringComparison.OrdinalIgnoreCase)
                                         || nombreSinPortal.IndexOf("ASISTENCIA", StringComparison.OrdinalIgnoreCase) >= 0;

                        if (esAsistencia)
                        {
                            int asistidas = asistencias.Count(a =>
                                a.IdMatriculaCabecera == m.IdMatriculaCabecera && a.Asistio);
                            nota = totalSesiones > 0
                                ? Math.Round((decimal)asistidas / totalSesiones * 100, 2)
                                : 0;
                        }
                        else
                        {
                            var detalles = notasDetalle
                                .Where(d => d.IdMatriculaCabecera == m.IdMatriculaCabecera
                                         && d.IdCriterioEvaluacion == evl.Id)
                                .ToList();

                            if (detalles.Any())
                            {
                                nota = Math.Round(detalles.Sum(d => d.Nota) / detalles.Count, 2);
                            }
                            else
                            {
                                var notasRows = notas
                                    .Where(n => n.IdMatriculaCabecera == m.IdMatriculaCabecera
                                             && n.IdCriterioEvaluacion == evl.Id)
                                    .ToList();
                                nota = notasRows.Any() ? notasRows.Sum(n => n.Nota) : 0;
                            }

                            if (escalaGlobal != 100)
                                nota = Math.Round(nota * 100 / escalaGlobal, 2);
                        }

                        notasCriterio.Add(new ReporteDashboardNotaCriterioDTO
                        {
                            IdEvaluacion   = evl.Id,
                            NombreCriterio = nombreSinPortal,
                            Porcentaje     = evl.Porcentaje,
                            Nota           = nota
                        });

                        promedioFinal += nota * evl.Porcentaje / 100;
                    }

                    // Enriquecer con datos del SP RS2
                    alumnosRawDict.TryGetValue(m.IdMatriculaCabecera, out var raw);

                    // Estado nota alumno: un criterio está calificado si:
                    //   - Es asistencia y hay sesiones (siempre se calcula), o
                    //   - Tiene registro en notas o notasDetalle (incluyendo nota=0)
                    // Nota=0 es calificación válida; no es lo mismo que "sin nota".
                    int criteriosConNota = notasCriterio.Count(nc =>
                    {
                        bool esAsistCrit = nc.NombreCriterio
                            .IndexOf("ASISTENCIA", StringComparison.OrdinalIgnoreCase) >= 0;
                        if (esAsistCrit) return totalSesiones > 0;
                        return notas.Any(r => r.IdMatriculaCabecera == m.IdMatriculaCabecera
                                           && r.IdCriterioEvaluacion == nc.IdEvaluacion)
                            || notasDetalle.Any(r => r.IdMatriculaCabecera == m.IdMatriculaCabecera
                                                  && r.IdCriterioEvaluacion == nc.IdEvaluacion);
                    });
                    string estadoNota = criteriosConNota == 0
                        ? "Pendiente"
                        : criteriosConNota < evaluaciones.Count
                            ? "Parcialmente registrada"
                            : "Completa";

                    alumnos.Add(new ReporteDashboardNotaAlumnoDTO
                    {
                        IdMatriculaCabecera   = m.IdMatriculaCabecera,
                        CodigoMatricula       = m.CodigoMatricula,
                        Alumno                = m.Alumno,
                        CoordinadoraAcademica = raw?.CoordinadoraAcademica,
                        CentroCosto           = raw?.CentroCosto,
                        Curso                 = raw?.Curso,
                        EstadoMatricula       = raw?.EstadoMatricula,
                        Notas                 = notasCriterio,
                        PromedioFinal         = Math.Round(promedioFinal, 0, MidpointRounding.AwayFromZero),
                        EstadoNotaAlumno      = estadoNota,
                        ObservacionChatbot    = raw?.ObservacionChatbot,
                    });
                }

                return new ReporteDashboardNotasPorPEspecificoDTO
                {
                    Programa     = programa,
                    Evaluaciones = encabezados,
                    Alumnos      = alumnos,
                    EsOnline     = esOnline
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerNotasPorPEspecificoAsync: {ex.Message}");
            }
        }

        // ── Dashboard 3: Furs ─────────────────────────────────────────────────

        public async Task<List<FurDTO>> ObtenerFursDashboard3Async()
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var sql = @"
                    SELECT Id, Codigo, IdCentroCosto, CentroCosto, Programa, IdCiudad, IdFurTipoPedido, NumeroSemana,
                           IdProveedor, RazonSocial, IdProducto, Producto, IdProductoPresentacion, ProductoPresentacion,
                           IdMoneda_Proveedor, FechaLimite, Descripcion, NumeroCuenta, Cuenta, Cantidad,
                           IdFaseAprobacion1, FaseAprobacion1, PrecioUnitarioMonedaOrigen, PrecioUnitarioDolares,
                           PrecioTotalMonedaOrigen, PrecioTotalDolares, UsuarioCreacion, UsuarioModificacion,
                           FechaModificacion, FechaCreacion, Observaciones, IdMonedaPagoReal,
                           IdPersonalAreaTrabajo, IdCondicionTipoPago, MonedaPagoReal, IdEmpresa
                    FROM FIN.V_ObtenerFurFinanzas
                    WHERE IdPersonalAreaTrabajo = 19
                      AND IdFaseAprobacion1 IN (3, 5, 9)
                      AND Antiguo = 0
                    ORDER BY Id DESC";
                return (await conn.QueryAsync<FurDTO>(sql)).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerFursDashboard3Async: {ex.Message}");
            }
        }

        // ── Nuevos endpoints: Por Estado / Por Modalidad / Grafico Por Mes ───

        /// <summary>
        /// Obtiene KPIs de programas agrupados por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardResumenProgramasDTO>> ObtenerResumenPorEstadoProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardResumenProgramasDTO>(
                    "pla.SP_ReporteDashboardResumenProgramasPorEstado",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        FechaHoraInicio_DiaMes = diaMes,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorEstadoProgramasAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene KPIs de cursos agrupados por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardResumenCursosDTO>> ObtenerResumenPorEstadoCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardResumenCursosDTO>(
                    "pla.SP_ReporteDashboardObtenerResumenCursos",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        FechaHoraInicio_DiaMes = diaMes,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorEstadoCursosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene distribucion de programas por modalidad con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardModalidadProgramasDTO>> ObtenerResumenPorModalidadProgramasAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardModalidadProgramasDTO>(
                    "pla.SP_ReporteDashboardObtenerModalidadProgramas",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        FechaHoraInicio_DiaMes = diaMes,
                        IdEstadoPEspecifico    = idEstadoPEspecifico,
                        IdPEspecifico_Padre    = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorModalidadProgramasAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene distribucion de cursos por modalidad con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardModalidadCursosDTO>> ObtenerResumenPorModalidadCursosAsync(int? anio, int? mes, int? semana, int? diaMes, int? idEstadoPEspecifico = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardModalidadCursosDTO>(
                    "pla.SP_ReporteDashboardObtenerModalidadCursos",
                    new
                    {
                        FechaHoraInicio_Anio   = anio,
                        FechaHoraInicio_Mes    = mes,
                        FechaHoraInicio_Semana = semana,
                        FechaHoraInicio_DiaMes = diaMes,
                        IdEstadoPEspecifico    = idEstadoPEspecifico,
                        IdCentroCosto_Padre    = idCentroCostoPadre
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorModalidadCursosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de programas por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesProgramasDTO>> ObtenerGraficoPorMesProgramasAsync(string? anios, int? mes, int? semana, int? diaMes, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null, int? idPais = null, string? estado = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardGraficoPorMesProgramasDTO>(
                    "pla.SP_ReporteDashboardObtenerGraficoPorMesProgramas",
                    new
                    {
                        FechaHoraInicio_AnioLista = anios,
                        FechaHoraInicio_Mes       = mes,
                        FechaHoraInicio_Semana    = semana,
                        FechaHoraInicio_DiaMes    = diaMes,
                        IdPEspecifico_Padre       = idProgramaEspecificoPadre,
                        IdCentroCosto_Padre       = idCentroCostoPadre,
                        IdPais                    = idPais,
                        EstadoPEspecifico         = estado
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGraficoPorMesProgramasAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene evolucion mensual de cursos por estado con filtros de fecha
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesCursosDTO>> ObtenerGraficoPorMesCursosAsync(string? anios, int? mes, int? semana, int? diaMes, int? idCentroCostoPadre = null, int? idPais = null, string? estado = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardGraficoPorMesCursosDTO>(
                    "pla.SP_ReporteDashboardObtenerGraficoPorMesCursos",
                    new
                    {
                        FechaHoraInicio_AnioLista = anios,
                        FechaHoraInicio_Mes       = mes,
                        FechaHoraInicio_Semana    = semana,
                        FechaHoraInicio_DiaMes    = diaMes,
                        IdCentroCosto_Padre       = idCentroCostoPadre,
                        IdPais                    = idPais,
                        EstadoPEspecifico         = estado
                    },
                    commandType: CommandType.StoredProcedure
                );
                return resultado.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGraficoPorMesCursosAsync: {ex.Message}");
            }
        }

    }
}
