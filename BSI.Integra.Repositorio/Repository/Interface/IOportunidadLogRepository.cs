using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadLogRepository : IGenericRepository<TOportunidadLog>
    {
        #region Metodos Base
        TOportunidadLog Add(OportunidadLog entidad);
        TOportunidadLog Update(OportunidadLog entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidadLog> Add(IEnumerable<OportunidadLog> listadoEntidad);
        IEnumerable<TOportunidadLog> Update(IEnumerable<OportunidadLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<FechaLogDatetimeDTO> ObtenerFechasSinContactoPorIdOportunidad(int idOportunidad);
        IEnumerable<OportunidadLogReporteSeguimientoYPersonalDTO> ObtenerReporteSeguimientoYPersonalPorIdOportunidad(int idOportunidad);
        IEnumerable<OportunidadLogReporteSeguimientoNWDTO> ObtenerOportunidadLogReporteSeguimientoNWPorIdOportunidad(int idOportunidad);
        IEnumerable<OportunidadLogReporteSeguimientoAlternoDTO> ObtenerOportunidadLogReporteSeguimientoNWAlterno3cx(int idOportunidad);
        List<ReporteActividadOcurrenciaDTO> ObtenerReporteActividadOcurrenciaPorIdOportunidad(int idOportunidad);
        IEnumerable<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrencia(int idOportunidad);
        OportunidadLog ObtenerUltimoOportunidadLog2(int idOportunidad);
        OportunidadLog ObtenerOportunidadLogPorIdOportunidad(int idOportunidad);
        List<OportunidadLog> ObtenerOportunidadLogsPorIdOportunidad(int idOportunidad);
        OportunidadLog ObtenerUltimoOportunidadLog(int idOportunidad);
        Task<OportunidadLog> ObtenerUltimoOportunidadLogAsync(int idOportunidad);
        OportunidadLog ObtenerPorId(int idOportunidadLog);
        OportunidadLogRevertirDTO RevertirFaseOportunidad(int idOportunidad, DateTime? FechaProgramada, string usuario);
        List<ObtenerDetalleOportunidadDTO> ObtenerDetalleOportunidad(int idOportunidad);
        IEnumerable<OportunidadLogReporteDTO> ObtenerOportunidadLogReporteSeguimientoV5(int idOportunidad, int diferenciaHoraria);
        List<IntDTO> ObtenerDetallePersonalAsignado(int idOportunidad);
    }
}