using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IActividadDetalleRepository : IGenericRepository<TActividadDetalle>
    {
        #region Metodos Base
        TActividadDetalle Add(ActividadDetalle entidad);
        TActividadDetalle AddAsync(ActividadDetalle entidad);
        TActividadDetalle Update(ActividadDetalle entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TActividadDetalle> Add(IEnumerable<ActividadDetalle> listadoEntidad);
        IEnumerable<TActividadDetalle> Update(IEnumerable<ActividadDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ActividadDetalleDTO> ObtenerActividadDetalle();
        ActividadDetalle ObtenerPorId(int idActividadDetalle);
        Task<ActividadDetalle> ObtenerPorIdAsync(int idActividadDetalle);
        List<CompuestoActividadEjecutadaDTO> ObtenerAgendaActividades(int idActividadDetalle);
        Task<List<CompuestoActividadEjecutadaDTO>> ObtenerAgendaActividadesAsync(int idActividadDetalle);
        List<ReporteActividadOcurrenciaDTO> ReporteActividadOcurrenciaAgenda(int idOportunidad);
        Task<List<ReporteActividadOcurrenciaDTO>> ReporteActividadOcurrenciaAgendaAsync(int idOportunidad);
        ValorIntDTO ObtenerIdActividadDetalle(int idActividadDetalle);
        ActividadDetalle ObtenerEntidadActividadDetallePorId(int idActividadDetalle);
        string ActualizarOportunidadYClasificacionPersona(int idOportunidad, int idClasificacionPersona, int id);
        TActividadDetalle UpdateAlterno(ActividadDetalle entidad);
        bool Detached(ActividadDetalle entidad);
        ActividadDetalle ObtenerPorIdOportunidad(int idOportunidad);
    }
}