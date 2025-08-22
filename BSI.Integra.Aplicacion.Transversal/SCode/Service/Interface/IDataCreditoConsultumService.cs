using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoConsultumService
    {
        #region Metodos Base
        DataCreditoConsultum Add(DataCreditoConsultum entidad);
        DataCreditoConsultum Update(DataCreditoConsultum entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoConsultum> Add(List<DataCreditoConsultum> listadoEntidad);
        List<DataCreditoConsultum> Update(List<DataCreditoConsultum> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
