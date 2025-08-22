using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteIngresoCongelamientoRepository : IGenericRepository<TReporteIngresoCongelamiento>
    {
        #region Metodos Base
        TReporteIngresoCongelamiento Add(ReporteIngresoCongelamiento entidad);
        TReporteIngresoCongelamiento Update(ReporteIngresoCongelamiento entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TReporteIngresoCongelamiento> Add(IEnumerable<ReporteIngresoCongelamiento> listadoEntidad);
        IEnumerable<TReporteIngresoCongelamiento> Update(IEnumerable<ReporteIngresoCongelamiento> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ReporteIngresoCongelamientoDTO> ObtenerReporteIngresoCongelamiento();
    }
}
