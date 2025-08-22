using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialAccionRepository : IGenericRepository<TMaterialAccion>
    {
        #region Metodos Base
        TMaterialAccion Add(MaterialAccion entidad);
        TMaterialAccion Update(MaterialAccion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialAccion> Add(IEnumerable<MaterialAccion> listadoEntidad);
        IEnumerable<TMaterialAccion> Update(IEnumerable<MaterialAccion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialAccion> ObtenerTodo();
        MaterialAccion ObtenerPorId(int id);
        List<MaterialAccion> ObtenerPorIds(List<int> id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
