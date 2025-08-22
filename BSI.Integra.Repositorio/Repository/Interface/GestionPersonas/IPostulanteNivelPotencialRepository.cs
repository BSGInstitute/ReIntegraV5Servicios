using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IPostulanteNivelPotencialRepository : IGenericRepository<TPostulanteNivelPotencial>
    {
        #region Metodos Base
        TPostulanteNivelPotencial Add(PostulanteNivelPotencial entidad);
        TPostulanteNivelPotencial Update(PostulanteNivelPotencial entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TPostulanteNivelPotencial> Add(IEnumerable<PostulanteNivelPotencial> listadoEntidad);
        IEnumerable<TPostulanteNivelPotencial> Update(IEnumerable<PostulanteNivelPotencial> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<PostulanteNivelPotencialDTO> Obtener();
        PostulanteNivelPotencial? ObtenerPorId(int id);
    }
}
