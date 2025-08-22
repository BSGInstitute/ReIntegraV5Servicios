using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICategoriaAsignacionRepository : IGenericRepository<TCategoriaAsignacion>
    {
        #region Metodos Base
        TCategoriaAsignacion Add(CategoriaAsignacion entidad);
        TCategoriaAsignacion Update(CategoriaAsignacion entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCategoriaAsignacion> Add(IEnumerable<CategoriaAsignacion> listadoEntidad);
        IEnumerable<TCategoriaAsignacion> Update(IEnumerable<CategoriaAsignacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
