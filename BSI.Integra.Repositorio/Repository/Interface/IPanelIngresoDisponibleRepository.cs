using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IPanelIngresoDisponibleRepository : IGenericRepository<TPanelIngresoDisponible>
    {
        #region Metodos Base
        TPanelIngresoDisponible Add(PanelIngresoDisponible entidad);
        TPanelIngresoDisponible Update(PanelIngresoDisponible entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPanelIngresoDisponible> Add(IEnumerable<PanelIngresoDisponible> listadoEntidad);
        IEnumerable<TPanelIngresoDisponible> Update(IEnumerable<PanelIngresoDisponible> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        public List<PanelDepositoDisponibleDTO> ObtenerPanelDepositoDisponible();
    }
       
}
