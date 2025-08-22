using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteExperienciaLogRepository : IGenericRepository<TPostulanteExperienciaLog>
    {
        #region Metodos Base
        TPostulanteExperienciaLog Add(PostulanteExperienciaLog entidad);
        TPostulanteExperienciaLog Update(PostulanteExperienciaLog entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteExperienciaLog> Add(IEnumerable<PostulanteExperienciaLog> listadoEntidad);
        IEnumerable<TPostulanteExperienciaLog> Update(IEnumerable<PostulanteExperienciaLog> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteExperienciaLog? ObtenerPorId(int id);
        IEnumerable<PostulanteExperienciaLogV2DTO> ObtenerHistorialPostulanteExperiencia(int id);


    }
}
