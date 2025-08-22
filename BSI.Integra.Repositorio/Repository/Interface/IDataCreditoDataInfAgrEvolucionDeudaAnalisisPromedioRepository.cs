using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedioRepository : IGenericRepository<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Add(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad);
        TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio Update(DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Add(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> Update(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaAnalisisPromedio> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
