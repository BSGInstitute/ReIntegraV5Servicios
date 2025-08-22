using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IPanelIngresoDisponibleService
    {
        #region Metodos Base
        PanelIngresoDisponible Add(PanelIngresoDisponible entidad);
        PanelIngresoDisponible Update(PanelIngresoDisponible entidad);
        bool Delete(int id, string usuario);

        List<PanelIngresoDisponible> Add(List<PanelIngresoDisponible> listadoEntidad);
        List<PanelIngresoDisponible> Update(List<PanelIngresoDisponible> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        public List<PanelDepositoDisponibleDTO> ObtenerPanelDepositoDisponible();
        public object InsertarPanelDepositoDisponible(PanelDepositoDisponibleDTO Json);
    }

}
