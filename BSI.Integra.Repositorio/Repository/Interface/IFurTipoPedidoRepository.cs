using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IFurTipoPedidoRepository : IGenericRepository<TFurTipoPedido>
    {
        #region Metodos Base
        TFurTipoPedido Add(FurTipoPedido entidad);
        TFurTipoPedido Update(FurTipoPedido entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TFurTipoPedido> Add(IEnumerable<FurTipoPedido> listadoEntidad);
        IEnumerable<TFurTipoPedido> Update(IEnumerable<FurTipoPedido> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<object> ObtenerTipoPedidoFur();

    }
}
