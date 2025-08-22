using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IOrigenIngresoCajaService
    {
        #region Metodos Base
        OrigenIngresoCaja Add(OrigenIngresoCajaDTO data, string Usuario);
        OrigenIngresoCaja Update(OrigenIngresoCajaDTO data, string Usuario);
        bool Delete(int id, string usuario);

        List<OrigenIngresoCaja> Add(List<OrigenIngresoCaja> listadoEntidad);
        List<OrigenIngresoCaja> Update(List<OrigenIngresoCaja> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<OrigenIngresoCajaComboDTO> ObtenerCombo();
    }
}
