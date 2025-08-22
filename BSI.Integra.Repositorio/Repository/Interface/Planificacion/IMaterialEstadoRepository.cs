using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialEstadoRepository : IGenericRepository<TMaterialEstado>
    {
        #region Metodos Base
        TMaterialEstado Add(MaterialEstado entidad);
        TMaterialEstado Update(MaterialEstado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialEstado> Add(IEnumerable<MaterialEstado> listadoEntidad);
        IEnumerable<TMaterialEstado> Update(IEnumerable<MaterialEstado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialEstado> ObtenerTodo();
        MaterialEstado ObtenerPorId(int id);
        List<MaterialEstado> ObtenerPorIds(List<int> id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
