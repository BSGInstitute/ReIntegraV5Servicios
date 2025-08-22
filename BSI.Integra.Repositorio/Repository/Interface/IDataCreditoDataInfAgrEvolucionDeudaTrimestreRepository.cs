using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfAgrEvolucionDeudaTrimestreRepository : IGenericRepository<TDataCreditoDataInfAgrEvolucionDeudaTrimestre>
    {
        #region Metodos Base
        TDataCreditoDataInfAgrEvolucionDeudaTrimestre Add(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad);
        TDataCreditoDataInfAgrEvolucionDeudaTrimestre Update(DataCreditoDataInfAgrEvolucionDeudaTrimestre entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaTrimestre> Add(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad);
        IEnumerable<TDataCreditoDataInfAgrEvolucionDeudaTrimestre> Update(IEnumerable<DataCreditoDataInfAgrEvolucionDeudaTrimestre> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
