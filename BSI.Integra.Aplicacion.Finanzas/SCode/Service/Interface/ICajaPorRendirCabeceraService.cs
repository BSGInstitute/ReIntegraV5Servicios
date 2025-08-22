using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface ICajaPorRendirCabeceraService
    {
        #region Metodos Base
        CajaPorRendirCabecera Add(CajaPorRendirCabeceraDTO entidad);
        CajaPorRendirCabecera Update(CajaPorRendirCabecera entidad);
        bool Delete(int id, string usuario);

        List<CajaPorRendirCabecera> Add(List<CajaPorRendirCabecera> listadoEntidad);
        List<CajaPorRendirCabecera> Update(List<CajaPorRendirCabecera> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        IEnumerable<CajaPorRendirCabeceraComboDTO> ObtenerComboCabeceraPR();
    }
}
