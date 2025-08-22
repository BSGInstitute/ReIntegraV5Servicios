using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteExperienciaRepository : IGenericRepository<TPostulanteExperiencium>
    {
        #region Metodos Base
        TPostulanteExperiencium Add(PostulanteExperiencia entidad);
        TPostulanteExperiencium Update(PostulanteExperiencia entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TPostulanteExperiencium> Add(IEnumerable<PostulanteExperiencia> listadoEntidad);
        IEnumerable<TPostulanteExperiencium> Update(IEnumerable<PostulanteExperiencia> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        PostulanteExperiencia? ObtenerPorId(int id);

        IEnumerable<PostulanteExperienciaDTO> ObtenerPostulanteExperiencia(int idPostulante);
    }
}
