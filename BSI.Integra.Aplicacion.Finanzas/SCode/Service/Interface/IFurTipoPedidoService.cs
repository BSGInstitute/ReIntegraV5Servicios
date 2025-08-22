using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFurTipoPedidoService
    {
        #region Metodos Base
        FurTipoPedido Add(FurTipoPedido entidad);
        FurTipoPedido Update(FurTipoPedido entidad);
        bool Delete(int id, string usuario);

        List<FurTipoPedido> Add(List<FurTipoPedido> listadoEntidad);
        List<FurTipoPedido> Update(List<FurTipoPedido> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<object> ObtenerTipoPedidoFur();
    }
}
