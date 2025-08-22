using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IReporteFlujoCongeladoPorDiumService
    {
        #region Metodos Base
        bool Delete(int id, string usuario);

        List<ReporteFlujoCongeladoPorDium> Add(List<ReporteFlujoCongeladoPorDium> listadoEntidad);
        List<ReporteFlujoCongeladoPorDium> Update(List<ReporteFlujoCongeladoPorDium> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

    }
}
