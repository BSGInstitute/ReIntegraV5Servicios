using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioService
    {
        #region Metodos Base
        DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Add(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad);
        DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Update(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Add(List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad);
        List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Update(List<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
