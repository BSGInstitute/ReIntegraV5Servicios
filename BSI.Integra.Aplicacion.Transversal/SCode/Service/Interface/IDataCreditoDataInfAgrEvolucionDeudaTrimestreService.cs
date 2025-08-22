using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IDataCreditoDataInfAgrEvolucionDeudaTrimestreService
    {
        #region Metodos Base
        DataCreditoDataInfAgrEvolucionDeudaTrimestre Add(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad);
        DataCreditoDataInfAgrEvolucionDeudaTrimestre Update(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad);
        bool Delete(int id, string usuario);
        List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> Add(List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad);
        List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> Update(List<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
    }
}
