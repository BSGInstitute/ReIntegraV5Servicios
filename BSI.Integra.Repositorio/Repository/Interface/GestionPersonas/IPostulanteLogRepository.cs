using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteLogRepository : IGenericRepository<TPostulanteLog>
    {
        #region Metodos Base
        TPostulanteLog Add(PostulanteLog entidad);
        TPostulanteLog Update(PostulanteLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteLog> Add(IEnumerable<PostulanteLog> listadoEntidad);
        IEnumerable<TPostulanteLog> Update(IEnumerable<PostulanteLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteLog? ObtenerPorId(int id);
        List<PostulanteLogHistorialDTO> ObtenerHistorialPostulante(int IdPostulante, string Clave);
    }
}
