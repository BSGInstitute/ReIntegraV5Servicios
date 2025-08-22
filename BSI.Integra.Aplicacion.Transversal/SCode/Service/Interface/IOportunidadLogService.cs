using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadLogService
    {
        IEnumerable<OportunidadLogHistorialComentariosDTO> ObtenerHistorialComentariosPorIdOportunidad(int idOportunidad);
        IEnumerable<ReporteSeguimientoNWActividadDTO> ObtenerReporteSeguimientoNWActividadesPorIdOportunidad(int idOportunidad);
        List<ReporteSeguimientoNWActividadAlternoDTO> ObtenerReporteSeguimientoNWActividadesPorIdOportunidad3cx(int idOportunidad);
        string ObtenerEstadoFaseOportunidadLog(EstadoFaseOportunidadLogDTO fasesOportunidad);
        string? ObtenerEstadoActividadOportunidadLog(int? idEstadoOcurrencia, int? idOcurrencia);
        public List<ObtenerDetalleOportunidadDTO> ObtenerDetalleOportunidad(int idOportunidad);
        List<ReporteSeguimientoNWActividadAlternoDTO> ObtenerReporteSeguimientoActividadesPorIdOportunidad(int idOportunidad);
    }
}
