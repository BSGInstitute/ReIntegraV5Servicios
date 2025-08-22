using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteComparacionRepository : IGenericRepository<TPostulanteComparacion>
    {
        #region Metodos Base
        TPostulanteComparacion Add(PostulanteComparacion entidad);
        TPostulanteComparacion Update(PostulanteComparacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPostulanteComparacion> Add(IEnumerable<PostulanteComparacion> listadoEntidad);
        IEnumerable<TPostulanteComparacion> Update(IEnumerable<PostulanteComparacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteComparacion? ObtenerPorId(int id);
        List<int> ObtenerIdsPostulantesPorIdGrupoComparacion(int idGrupoComparacion);
    }
}
