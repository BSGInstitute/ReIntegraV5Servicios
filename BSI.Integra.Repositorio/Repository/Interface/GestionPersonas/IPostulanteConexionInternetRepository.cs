using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteConexionInternetRepository : IGenericRepository<TPostulanteConexionInternet>
    {
        #region Metodos Base
        TPostulanteConexionInternet Add(PostulanteConexionInternet entidad);
        TPostulanteConexionInternet Update(PostulanteConexionInternet entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteConexionInternet> Add(IEnumerable<PostulanteConexionInternet> listadoEntidad);
        IEnumerable<TPostulanteConexionInternet> Update(IEnumerable<PostulanteConexionInternet> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteConexionInternet? ObtenerPorId(int id);
    }
}
