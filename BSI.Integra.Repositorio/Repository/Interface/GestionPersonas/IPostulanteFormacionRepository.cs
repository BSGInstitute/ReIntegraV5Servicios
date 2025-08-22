using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteFormacionRepository : IGenericRepository<TPostulanteFormacion>
    {
        #region Metodos Base
        TPostulanteFormacion Add(PostulanteFormacion entidad);
        TPostulanteFormacion Update(PostulanteFormacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteFormacion> Add(IEnumerable<PostulanteFormacion> listadoEntidad);
        IEnumerable<TPostulanteFormacion> Update(IEnumerable<PostulanteFormacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteFormacion? ObtenerPorId(int id);
        IEnumerable<PostulanteFormacionDTO> ObtenerPostulanteFormacion(int idPostulante);
    }
}
