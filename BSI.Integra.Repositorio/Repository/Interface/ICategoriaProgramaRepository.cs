using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICategoriaProgramaRepository : IGenericRepository<TCategoriaPrograma>
    {
        #region Metodos Base
        TCategoriaPrograma Add(CategoriaPrograma entidad);
        TCategoriaPrograma Update(CategoriaPrograma entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TCategoriaPrograma> Add(IEnumerable<CategoriaPrograma> listadoEntidad);
        IEnumerable<TCategoriaPrograma> Update(IEnumerable<CategoriaPrograma> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
        Task<IEnumerable<ComboDTO>> ObtenerComboAsync();
    }
}
