using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Repositorio.Repository.Interface.Planificacion;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Implementation.Planificacion
{
    /// Repositorio: ReporteConsultasForoAulaVirtualRepository
    /// Autor: Edmundo Llaza
    /// Fecha: 2023-07-31
    /// <summary>
    /// Contiene los controladores necesarios para los filtros y la consulta del reporte de foro aula virtual
    /// </summary>
    public class ReporteConsultasForoAulaVirtualRepository : IReporteConsultasForoAulaVirtualRepository
    {
        private IDapperRepository _dapperRepository;
        public ReporteConsultasForoAulaVirtualRepository(IDapperRepository dapperRepository)
        {
            _dapperRepository = dapperRepository;
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-07-31
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de atención de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool ActualizarEstadoAtencionForo(int idForo, bool estadoAtendido, string usuarioModificacion)
        {
            try
            {
                _dapperRepository.QuerySPDapper("pw.SP_PW_ActualizarEstadoAtencionForo", new { idForo, estadoAtendido, usuarioModificacion });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado para eliminación de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool EliminarForo(int idForo, string usuarioModificacion)
        {
            try
            {
                var subQuery = _dapperRepository.QuerySPDapper("pw.SP_PW_EliminarForo", new { idForo, usuarioModificacion });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-01
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado de abierto o cerrado de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool ActualizarAperturaForo(int idForo, bool estadoForo, string usuarioModificacion)
        {
            try
            {
                _dapperRepository.QuerySPDapper("pw.SP_PW_ActualizarAperturaForo", new { idForo, estadoForo, usuarioModificacion });
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-07
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de consultas del Foro de AulaVirtual, segun los filtros suministrados.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteConsultasForoAulaVirtualDTO> </returns>
        public List<ReporteConsultasForoAulaVirtualDTO> GenerarReporteConsultasForoAulaVirtual(ReporteConsultasForoFiltroDTO filtroReporte)
        {
            try
            {
                string? programa = null, docente = null, curso = null, estadoConsulta = null;
                if (filtroReporte.Programa != null && filtroReporte.Programa.Count() > 0) programa = string.Join(",", filtroReporte.Programa);
                if (filtroReporte.Docente != null && filtroReporte.Docente.Count() > 0) docente = string.Join(",", filtroReporte.Docente);
                if (filtroReporte.Curso != null && filtroReporte.Curso.Count() > 0) curso = string.Join(",", filtroReporte.Curso);
                if (filtroReporte.EstadoConsulta != null) estadoConsulta = string.Join(",", filtroReporte.EstadoConsulta);

                DateTime fechainicio = new DateTime(filtroReporte.FechaInicial.Year, filtroReporte.FechaInicial.Month, filtroReporte.FechaInicial.Day, 0, 0, 0);
                DateTime fechafin = new DateTime(filtroReporte.FechaFin.Year, filtroReporte.FechaFin.Month, filtroReporte.FechaFin.Day, 23, 59, 59);
                List<ReporteConsultasForoAulaVirtualDTO> reporteConsultasForo = new List<ReporteConsultasForoAulaVirtualDTO>();
                var query = _dapperRepository.QuerySPDapper("[mkt].[SP_ReporteConsultasForoAulaVirtual]", new { fechainicio, fechafin, programa, docente, curso, estadoConsulta });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]"))
                {
                    reporteConsultasForo = JsonConvert.DeserializeObject<List<ReporteConsultasForoAulaVirtualDTO>>(query)!;
                }
                return reporteConsultasForo;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Actualiza el encargado de la revisión de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool ActualizarEncargadoForo(int id, int idProveedor)
        {
            try
            {
                _dapperRepository.QuerySPDapper("pw.SP_PW_ActualizarEncargadoForo", new { id, idProveedor });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ex ActualizarEncargadoForo(), {ex.Message}");
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Función que trae la data de consultas del deralle del Foro de AulaVirtual.
        /// </summary>
        /// <returns>Retorma una lista List<ReporteConsultasForoDetalleAulaVirtualDTO> </returns>
        public List<ReporteConsultasForoDetalleAulaVirtualDTO> ObtenerDetalleForo(int idForoCurso)
        {
            try
            {
                List<ReporteConsultasForoDetalleAulaVirtualDTO> reporteConsultasForoDetalle = new();
                var query = _dapperRepository.QuerySPDapper("[pw].[SP_PW_ReporteConsultasForoDetalleAulaVirtual]", new { idForoCurso });
                if (!string.IsNullOrEmpty(query) && !query.Contains("[]") && query != "null")
                {
                    reporteConsultasForoDetalle = JsonConvert.DeserializeObject<List<ReporteConsultasForoDetalleAulaVirtualDTO>>(query)!;
                }
                return reporteConsultasForoDetalle;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ex ObtenerDetalleForo(), {ex.Message}");
            }
        }
        /// Autor: Edmundo Llaza
        /// Fecha: 2023-08-08
        /// Version: 1.0
        /// <summary>
        /// Actualiza el estado para la eliminación de la respuesta de un foro
        /// </summary>
        /// <returns>bool </returns>
        public bool EliminarForoRespuesta(int IdForoRespuesta, string UsuarioModificacion)
        {
            try
            {
                _dapperRepository.QuerySPDapper("pw.SP_PW_EliminarForoRespuesta", new { IdForoRespuesta, UsuarioModificacion });
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error ex EliminarForoRespuesta(), {ex.Message}");
            }
        }

    }
}
