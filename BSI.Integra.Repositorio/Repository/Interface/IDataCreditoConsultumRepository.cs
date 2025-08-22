using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoConsultumRepository : IGenericRepository<TDataCreditoConsultum>
    {
        #region Metodos Base
        TDataCreditoConsultum Add(DataCreditoConsultum entidad);
        TDataCreditoConsultum Update(DataCreditoConsultum entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoConsultum> Add(IEnumerable<DataCreditoConsultum> listadoEntidad);
        IEnumerable<TDataCreditoConsultum> Update(IEnumerable<DataCreditoConsultum> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
