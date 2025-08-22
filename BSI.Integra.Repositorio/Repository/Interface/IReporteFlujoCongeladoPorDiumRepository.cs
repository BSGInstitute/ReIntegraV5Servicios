using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IReporteFlujoCongeladoPorDiumRepository : IGenericRepository<TReporteFlujoCongeladoPorDium>
    {
        #region Metodos Base
        TReporteFlujoCongeladoPorDium Add(ReporteFlujoCongeladoPorDium entidad);
        TReporteFlujoCongeladoPorDium Update(ReporteFlujoCongeladoPorDium entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TReporteFlujoCongeladoPorDium> Add(IEnumerable<ReporteFlujoCongeladoPorDium> listadoEntidad);
        IEnumerable<TReporteFlujoCongeladoPorDium> Update(IEnumerable<ReporteFlujoCongeladoPorDium> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
     
    }
}
