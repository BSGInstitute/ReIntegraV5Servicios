using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IDataCreditoDataInfMicroPerfilGeneralRepository : IGenericRepository<TDataCreditoDataInfMicroPerfilGeneral>
    {
        #region Metodos Base
        TDataCreditoDataInfMicroPerfilGeneral Add(DataCreditoDataInfMicroPerfilGeneral entidad);
        TDataCreditoDataInfMicroPerfilGeneral Update(DataCreditoDataInfMicroPerfilGeneral entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TDataCreditoDataInfMicroPerfilGeneral> Add(IEnumerable<DataCreditoDataInfMicroPerfilGeneral> listadoEntidad);
        IEnumerable<TDataCreditoDataInfMicroPerfilGeneral> Update(IEnumerable<DataCreditoDataInfMicroPerfilGeneral> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
    }
}
