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
        public async Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryFirstOrDefaultAsync<ReporteDashboardResumenDTO>(
                    "pla.SP_ReporteDashboard_ObtenerResumen",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
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
        public async Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardSemanalDTO>(
                    "pla.SP_ReporteDashboard_ObtenerResumenSemanal",
                    new { Anio = anio, MesInicio = mesInicio, MesFin = mesFin, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
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
        public async Task<List<ReporteDashboardEstadoSesionDTO>> ObtenerResumenPorEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEstadoSesionDTO>(
                    "pla.SP_ReporteDashboard_ObtenerResumenPorEstadoSesion",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
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
        public async Task<List<ReporteDashboardSesionDetalleDTO>> ObtenerSesionesPorEstadoAsync(int? anio, int? idEstadoSesion = null, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardSesionDetalleDTO>(
                    "pla.SP_ReporteDashboard_ObtenerSesionesPorEstado",
                    new { Anio = anio, IdEstadoSesion = idEstadoSesion, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
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
        public async Task<ReporteDashboardKPIsEstadoSesionDTO> ObtenerKPIsEstadoSesionAsync(int? anio, int? idProgramaEspecificoPadre = null, int? idCentroCostoPadre = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryFirstOrDefaultAsync<ReporteDashboardKPIsEstadoSesionDTO>(
                    "pla.SP_ReporteDashboard_ObtenerKPIsEstadoSesion",
                    new { Anio = anio, IdProgramaEspecificoPadre = idProgramaEspecificoPadre, IdCentroCostoPadre = idCentroCostoPadre },
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
        public async Task<List<ReporteDashboardCambioEstadoDTO>> ObtenerCambiosEstadoAsync(int? ultimasSemanas = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardCambioEstadoDTO>(
                    "pla.SP_ReporteDashboard_ObtenerCambiosEstado",
                    new { UltimasSemanas = ultimasSemanas ?? 8 },
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
        public async Task<List<ReporteDashboardEstadoPorDiaDTO>> ObtenerEstadosPorDiaAsync(string? idsPEspecificoHijo, string? estados, string? agrupacion, DateTime? fechaInicio, DateTime? fechaFin, int? ultimasSemanas = null)
        {
            try
            {
                using var conn = _connectionFactory.GetConnection;
                var resultado = await conn.QueryAsync<ReporteDashboardEstadoPorDiaDTO>(
                    "pla.SP_ReporteDashboard_ObtenerEstadosPorDia",
                    new
                    {
                        IdsPEspecificoHijo = idsPEspecificoHijo,
                        Estados = estados,
                        Agrupacion = agrupacion ?? "DIA",
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        UltimasSemanas = ultimasSemanas
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
                    "pla.SP_ReporteDashboard_ObtenerSeguimientoClases",
                    new
                    {
                        FechaInicio = filtro.FechaInicio,
                        FechaFin = filtro.FechaFin,
                        EstadoCurso = filtro.EstadoCurso,
                        Anio = filtro.Anio,
                        SemanaInicio = filtro.SemanaInicio,
                        SemanaFin = filtro.SemanaFin
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

    }
}
