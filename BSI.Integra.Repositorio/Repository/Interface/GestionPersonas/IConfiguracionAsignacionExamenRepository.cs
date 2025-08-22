using BSI.Integra.Aplicacion.DTOs;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IConfiguracionAsignacionExamenRepository : IGenericRepository<TConfiguracionAsignacionExaman>
    {
        #region Metodos Base
        TConfiguracionAsignacionExaman Add(ConfiguracionAsignacionExamen entidad);
        TConfiguracionAsignacionExaman Update(ConfiguracionAsignacionExamen entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TConfiguracionAsignacionExaman> Add(IEnumerable<ConfiguracionAsignacionExamen> listadoEntidad);
        IEnumerable<TConfiguracionAsignacionExaman> Update(IEnumerable<ConfiguracionAsignacionExamen> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ConfiguracionAsignacionExamen? ObtenerPorId(int id);
        List<ConfiguracionAsignacionExamen>? ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion);
        List<int>? ObtenerPorIdsProcesoSeleccion(int idProcesoSeleccion);
        List<NombreEvaluacionAgrupadaComponenteDTO2>? obtenerComponentesEvaluacion();
    }
}
