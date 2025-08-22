using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICajaService
    {
        #region Metodos Base
        Caja Add(CajaDatosDTO entidad, string Usuario);
        Caja Update(CajaDatosDTO entidad, string Usuario);
        bool Delete(int id, string usuario);

        List<Caja> Add(List<Caja> listadoEntidad);
        List<Caja> Update(List<Caja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<CajaDTO> ObtenerCaja();
        IEnumerable<CajaComboDTO> ObtenerCombo();
        IEnumerable<CajaResponsableComboDTO> ObtenerListaCajaResponsable();
    }
}
