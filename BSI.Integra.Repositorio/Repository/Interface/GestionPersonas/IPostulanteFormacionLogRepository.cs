using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteFormacionLogRepository : IGenericRepository<TPostulanteFormacionLog>
    {
        #region Metodos Base
        TPostulanteFormacionLog Add(PostulanteFormacionLog entidad);
        TPostulanteFormacionLog Update(PostulanteFormacionLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteFormacionLog> Add(IEnumerable<PostulanteFormacionLog> listadoEntidad);
        IEnumerable<TPostulanteFormacionLog> Update(IEnumerable<PostulanteFormacionLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteFormacionLog? ObtenerPorId(int id);
        IEnumerable<PostulanteFormacionLogDTO> ObtenerHistorialPostulanteFormacion(int idPostulante);
    }
}
