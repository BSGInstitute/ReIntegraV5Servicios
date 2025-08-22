using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteIdiomaRepository : IGenericRepository<TPostulanteIdioma>
    {
        #region Metodos Base
        TPostulanteIdioma Add(PostulanteIdioma entidad);
        TPostulanteIdioma Update(PostulanteIdioma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteIdioma> Add(IEnumerable<PostulanteIdioma> listadoEntidad);
        IEnumerable<TPostulanteIdioma> Update(IEnumerable<PostulanteIdioma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteIdioma? ObtenerPorId(int id);
    }
}
