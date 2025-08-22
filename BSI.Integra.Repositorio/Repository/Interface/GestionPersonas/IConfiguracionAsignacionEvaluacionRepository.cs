using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IConfiguracionAsignacionEvaluacionRepository : IGenericRepository<TConfiguracionAsignacionEvaluacion>
    {
        #region Metodos Base
        TConfiguracionAsignacionEvaluacion Add(ConfiguracionAsignacionEvaluacion entidad);
        TConfiguracionAsignacionEvaluacion Update(ConfiguracionAsignacionEvaluacion entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionAsignacionEvaluacion> Add(IEnumerable<ConfiguracionAsignacionEvaluacion> listadoEntidad);
        IEnumerable<TConfiguracionAsignacionEvaluacion> Update(IEnumerable<ConfiguracionAsignacionEvaluacion> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionAsignacionEvaluacion? ObtenerPorId(int id);
        ConfiguracionAsignacionEvaluacion? ObtenerPorIdProcesoSeleccionIdEvaluacion(int idProcesoSeleccion, int idEvaluacion);
        List<ConfiguracionAsignacionEvaluacion>? ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion);
    }
}
