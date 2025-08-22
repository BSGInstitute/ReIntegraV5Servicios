using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICoordinadoraRepository : IGenericRepository<TCoordinadora>
    {
        #region Metodos Base
        TCoordinadora Add(Coordinadora entidad);
        TCoordinadora Update(Coordinadora entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCoordinadora> Add(IEnumerable<Coordinadora> listadoEntidad);
        IEnumerable<TCoordinadora> Update(IEnumerable<Coordinadora> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        Coordinadora? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerCoordinadoresDocentes();
        Task<IEnumerable<ComboDTO>> ObtenerCoordinadoresDocentesAsync();
        bool ExistePorNombreUsuario(string usuario);
    }
}
