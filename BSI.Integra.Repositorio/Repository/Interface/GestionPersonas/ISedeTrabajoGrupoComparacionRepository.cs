using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface ISedeTrabajoGrupoComparacionRepository : IGenericRepository<TSedeTrabajoGrupoComparacion>
    {
        #region Metodos Base
        TSedeTrabajoGrupoComparacion Add(SedeTrabajoGrupoComparacion entidad);
        TSedeTrabajoGrupoComparacion Update(SedeTrabajoGrupoComparacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TSedeTrabajoGrupoComparacion> Add(IEnumerable<SedeTrabajoGrupoComparacion> listadoEntidad);
        IEnumerable<TSedeTrabajoGrupoComparacion> Update(IEnumerable<SedeTrabajoGrupoComparacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        SedeTrabajoGrupoComparacion? ObtenerPorId(int id);
    }
}
