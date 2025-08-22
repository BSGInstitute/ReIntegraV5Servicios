using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICondicionTipoPagoService
    {
        #region Metodos Base
        CondicionTipoPago Add(CondicionTipoPago entidad);
        CondicionTipoPago Update(CondicionTipoPago entidad);
        bool Delete(int id, string usuario);

        List<CondicionTipoPago> Add(List<CondicionTipoPago> listadoEntidad);
        List<CondicionTipoPago> Update(List<CondicionTipoPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
