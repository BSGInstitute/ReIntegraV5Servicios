using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IGrupoComparacionProcesoSeleccionRepository : IGenericRepository<TGrupoComparacionProcesoSeleccion>
    {
        #region Metodos Base
        TGrupoComparacionProcesoSeleccion Add(GrupoComparacionProcesoSeleccion entidad);
        TGrupoComparacionProcesoSeleccion Update(GrupoComparacionProcesoSeleccion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TGrupoComparacionProcesoSeleccion> Add(IEnumerable<GrupoComparacionProcesoSeleccion> listadoEntidad);
        IEnumerable<TGrupoComparacionProcesoSeleccion> Update(IEnumerable<GrupoComparacionProcesoSeleccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<GrupoComparacionProcesoSeleccionDetalleDTO> ObtenerDetalle();
        GrupoComparacionProcesoSeleccion? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
