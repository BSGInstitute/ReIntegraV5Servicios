using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IActividadDetalleService
    {
        #region Metodos Base
        ActividadDetalle Add(ActividadDetalle entidad);
        ActividadDetalle Update(ActividadDetalle entidad);
        bool Delete(int id, string usuario);

        List<ActividadDetalle> Add(List<ActividadDetalle> listadoEntidad);
        List<ActividadDetalle> Update(List<ActividadDetalle> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ActividadDetalleDTO> ObtenerActividadDetalle();
        ActividadDetalle ObtenerPorId(int idActividadDetalle);
        ActividadDetalle MapeoEntidadDesdeDTO(ActividadDetalleDTO dto);
        List<CompuestoActividadEjecutadaDTO> ObtenerAgendaActividades(int idActividadDetalle);
        List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaAgenda(int idOportunidad);
        ActividadDetalle ObtenerEntidadActividadDetallePorId(int idActividadDetalle);
        CompuestoActividadEjecutadaDTO ObtenerAgendaRealizadaRegistroTiempoReal(int idActividadDetalle);
    }
}
