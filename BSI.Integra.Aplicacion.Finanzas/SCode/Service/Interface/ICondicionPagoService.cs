using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICondicionPagoService
    {
        #region Metodos Base
        CondicionPago Add(CondicionPago entidad);
        CondicionPago Update(CondicionPago entidad);
        bool Delete(int id, string usuario);

        List<CondicionPago> Add(List<CondicionPago> listadoEntidad);
        List<CondicionPago> Update(List<CondicionPago> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
