using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IVersionProgramaRepository : IGenericRepository<TVersionPrograma>
    {

        #region Metodos Base
        TVersionPrograma Add(VersionPrograma entidad);
        TVersionPrograma Update(VersionPrograma entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TVersionPrograma> Add(IEnumerable<VersionPrograma> listadoEntidad);
        IEnumerable<TVersionPrograma> Update(IEnumerable<VersionPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        #endregion
        IEnumerable<VersionProgramaDTO> ObtenerVersionPrograma();
        Task<IEnumerable<VersionProgramaDTO>> ObtenerVersionProgramaAsync();
        List<VersionProgramaNombreUsuarioDTO> ObtenerTodo();
        VersionPrograma ObtenerPorId(int id);   
        IEnumerable<VersionProgramaDTO> ObtenerVersionProgramaByBeneficios();
    }
}