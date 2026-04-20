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
        public async Task<ReporteDashboardResumenDTO> ObtenerResumenAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPFirstOrDefaultAsync(
                    "pla.SP_ReporteDashboard_ObtenerResumen_V2",
                    new {
                        Anio = anio,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && resultado != "null")
                {
                    return JsonConvert.DeserializeObject<ReporteDashboardResumenDTO>(resultado) ?? new ReporteDashboardResumenDTO();
                }
                return new ReporteDashboardResumenDTO();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por estado
        /// </summary>
        public async Task<List<ReporteDashboardEstadoDTO>> ObtenerResumenPorEstadoAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerResumenPorEstado_V2",
                    new {
                        Anio = anio,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardEstadoDTO>>(resultado) ?? new List<ReporteDashboardEstadoDTO>();
                }
                return new List<ReporteDashboardEstadoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene la distribucion de programas por modalidad
        /// </summary>
        public async Task<List<ReporteDashboardModalidadDTO>> ObtenerResumenPorModalidadAsync(int? anio, string? estado, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerGraficoModalidades_V2",
                    new {
                        Anio = anio,
                        Estado = estado,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardModalidadDTO>>(resultado) ?? new List<ReporteDashboardModalidadDTO>();
                }
                return new List<ReporteDashboardModalidadDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerResumenPorModalidadAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene listado de programas filtrado por estado
        /// </summary>
        public async Task<List<ReporteDashboardProgramaDTO>> ObtenerProgramasPorEstadoAsync(string? estado, int? anio, DateTime? fechaInicio, DateTime? fechaFin, string? modalidad, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerProgramasPorEstado_V2",
                    new
                    {
                        Estado = estado,
                        Anio = anio,
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        Modalidad = modalidad,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardProgramaDTO>>(resultado) ?? new List<ReporteDashboardProgramaDTO>();
                }
                return new List<ReporteDashboardProgramaDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerProgramasPorEstadoAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene detalle de cursos/sesiones
        /// </summary>
        public async Task<List<ReporteDashboardCursoDTO>> ObtenerDetalleCursosAsync(DateTime? fecha, DateTime? fechaInicio, DateTime? fechaFin, int? idProgramaPadre, int? anio, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerDetalleCursos_V2",
                    new
                    {
                        Fecha = fecha,
                        FechaInicio = fechaInicio,
                        FechaFin = fechaFin,
                        IdProgramaPadre = idProgramaPadre,
                        Anio = anio,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardCursoDTO>>(resultado) ?? new List<ReporteDashboardCursoDTO>();
                }
                return new List<ReporteDashboardCursoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDetalleCursosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene listado de docentes con sus asignaciones
        /// </summary>
        public async Task<List<ReporteDashboardDocenteDTO>> ObtenerDocentesAsignadosAsync(int? anio, int? idDocente, string? estado, bool soloActivos = false, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerDocentesAsignados_V2",
                    new
                    {
                        Anio = anio,
                        IdDocente = idDocente,
                        Estado = estado,
                        SoloActivos = soloActivos,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardDocenteDTO>>(resultado) ?? new List<ReporteDashboardDocenteDTO>();
                }
                return new List<ReporteDashboardDocenteDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDocentesAsignadosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene datos para grafico de programas por mes
        /// </summary>
        public async Task<List<ReporteDashboardGraficoPorMesDTO>> ObtenerGraficoPorMesAsync(int? anio, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerGraficoProgramasPorMes_V2",
                    new {
                        Anio = anio,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardGraficoPorMesDTO>>(resultado) ?? new List<ReporteDashboardGraficoPorMesDTO>();
                }
                return new List<ReporteDashboardGraficoPorMesDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerGraficoPorMesAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene los valores disponibles para los filtros del dashboard
        /// Usa QueryMultipleAsync para procesar multiples result sets de forma eficiente
        /// </summary>
        public async Task<ReporteDashboardFiltrosDTO> ObtenerFiltrosAsync()
        {
            try
            {
                var filtros = new ReporteDashboardFiltrosDTO();

                using (var conn = _connectionFactory.GetConnection)
                {
                    using (var multi = await conn.QueryMultipleAsync(
                        "pla.SP_ReporteDashboard_ObtenerFiltros_V3",
                        commandType: CommandType.StoredProcedure))
                    {
                        // Result Set 1: Anios
                        var aniosResult = await multi.ReadAsync<dynamic>();
                        filtros.Anios = aniosResult
                            .Select(x => (int)x.Anio)
                            .Where(x => x > 0)
                            .ToList();

                        // Result Set 2: Estados
                        var estadosResult = await multi.ReadAsync<dynamic>();
                        filtros.Estados = estadosResult
                            .Select(x => (string)x.Estado)
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList();

                        // Result Set 3: Modalidades
                        var modalidadesResult = await multi.ReadAsync<dynamic>();
                        filtros.Modalidades = modalidadesResult
                            .Select(x => (string)x.Modalidad)
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList();

                        // Result Set 4: Programas Especificos
                        var programasResult = await multi.ReadAsync<dynamic>();
                        filtros.ProgramasEspecificos = programasResult
                            .Select(x => new ReporteDashboardProgramaEspecificoItemDTO
                            {
                                Id = (int)x.Id,
                                Nombre = (string)x.Nombre
                            })
                            .Where(x => x.Id > 0)
                            .ToList();

                        // Result Set 5: Centros de Costo
                        var centrosCostoResult = await multi.ReadAsync<dynamic>();
                        filtros.CentrosCosto = centrosCostoResult
                            .Select(x => (string)x.CentroCostoPadre)
                            .Where(x => !string.IsNullOrEmpty(x))
                            .ToList();
                    }
                }

                // Inicializar listas vacias si es necesario
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
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerDatosCompletos_V2",
                    new
                    {
                        Anio = filtro.Anio,
                        Estado = filtro.Estado,
                        Modalidad = filtro.Modalidad,
                        FechaInicio = filtro.FechaInicio,
                        FechaFin = filtro.FechaFin,
                        Area = filtro.Area,
                        Ciudad = filtro.Ciudad,
                        ProgramaPadre = filtro.ProgramaPadre,
                        IdProgramaEspecificoPadre = filtro.IdProgramaEspecificoPadre,
                        CentroCostoPadre = filtro.CentroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardCompletoDTO>>(resultado) ?? new List<ReporteDashboardCompletoDTO>();
                }
                return new List<ReporteDashboardCompletoDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerDatosCompletosAsync: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtiene resumen semanal de sesiones
        /// </summary>
        public async Task<List<ReporteDashboardSemanalDTO>> ObtenerResumenSemanalAsync(int? anio, int? mesInicio, int? mesFin, int? idProgramaEspecificoPadre = null, string? centroCostoPadre = null)
        {
            try
            {
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerResumenSemanal_V2",
                    new
                    {
                        Anio = anio,
                        MesInicio = mesInicio,
                        MesFin = mesFin,
                        IdProgramaEspecificoPadre = idProgramaEspecificoPadre,
                        CentroCostoPadre = centroCostoPadre
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardSemanalDTO>>(resultado) ?? new List<ReporteDashboardSemanalDTO>();
                }
                return new List<ReporteDashboardSemanalDTO>();
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
                var resultado = await _dapperRepository.QuerySPDapperAsync(
                    "pla.SP_ReporteDashboard_ObtenerSesionesCalendario",
                    new
                    {
                        Anio = anio,
                        SemanaInicio = semanaInicio,
                        SemanaFin = semanaFin,
                        Mes = mes
                    }
                );

                if (!string.IsNullOrEmpty(resultado) && !resultado.Contains("[]"))
                {
                    return JsonConvert.DeserializeObject<List<ReporteDashboardCalendarioDTO>>(resultado) ?? new List<ReporteDashboardCalendarioDTO>();
                }
                return new List<ReporteDashboardCalendarioDTO>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en ObtenerSesionesCalendarioAsync: {ex.Message}");
            }
        }
    }
}
