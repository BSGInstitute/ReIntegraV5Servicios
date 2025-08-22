using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IMaterialCriterioVerificacionRepository : IGenericRepository<TMaterialCriterioVerificacion>
    {
        #region Metodos Base
        TMaterialCriterioVerificacion Add(MaterialCriterioVerificacion entidad);
        TMaterialCriterioVerificacion Update(MaterialCriterioVerificacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TMaterialCriterioVerificacion> Add(IEnumerable<MaterialCriterioVerificacion> listadoEntidad);
        IEnumerable<TMaterialCriterioVerificacion> Update(IEnumerable<MaterialCriterioVerificacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<MaterialCriterioVerificacion> ObtenerTodo();
        MaterialCriterioVerificacion ObtenerPorId(int id);
        List<MaterialCriterioVerificacion> ObtenerPorIds(List<int> id);
        IEnumerable<ComboDTO> ObtenerCombo();
    }
}
