using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPuestoTrabajoGrupoComparacionRepository : IGenericRepository<TPuestoTrabajoGrupoComparacion>
    {
        #region Metodos Base
        TPuestoTrabajoGrupoComparacion Add(PuestoTrabajoGrupoComparacion entidad);
        TPuestoTrabajoGrupoComparacion Update(PuestoTrabajoGrupoComparacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPuestoTrabajoGrupoComparacion> Add(IEnumerable<PuestoTrabajoGrupoComparacion> listadoEntidad);
        IEnumerable<TPuestoTrabajoGrupoComparacion> Update(IEnumerable<PuestoTrabajoGrupoComparacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PuestoTrabajoGrupoComparacion? ObtenerPorId(int id);
    }
}
