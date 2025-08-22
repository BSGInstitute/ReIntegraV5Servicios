using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IContadorBicLogDetalleRepository : IGenericRepository<TContadorBicLogDetalle>
    {
        #region Metodos Base
        TContadorBicLogDetalle Add(ContadorBicLogDetalle entidad);
        TContadorBicLogDetalle Update(ContadorBicLogDetalle entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TContadorBicLogDetalle> Add(IEnumerable<ContadorBicLogDetalle> listadoEntidad);
        IEnumerable<TContadorBicLogDetalle> Update(IEnumerable<ContadorBicLogDetalle> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        List<ContadorBicLogDetalle> ObtenerPorIdContadorLog(int idContadorLog);
        List<ContadorBicLogDetalle> ObtenerPorIdsContadorLog(List<int> idsContadorLog);
    }
}
