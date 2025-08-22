using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenAsignadoRepository : IGenericRepository<TExamenAsignado>
    {
        #region Metodos Base
        TExamenAsignado Add(ExamenAsignado entidad);
        TExamenAsignado Update(ExamenAsignado entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TExamenAsignado> Add(IEnumerable<ExamenAsignado> listadoEntidad);
        IEnumerable<TExamenAsignado> Update(IEnumerable<ExamenAsignado> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        ExamenAsignado? ObtenerPorId(int id);
        List<ExamenAsignado> ObtenerPorIdProcesoSeleccion(int idProcesoSeleccion);
        List<int> ObtenerIdsPostulantesPorIdProcesoSeleccion(int idProcesoSeleccion);
        ExamenAsignado? ObtenerPorIdPostulanteIdExamen(int idPostulante, int idExamen);

        ExamenAsignado ObtenerPorIdProcesoSeleccionYPorIdPostulante(int idProcesoSeleccion, int idPostulante);

        List<ConfiguracionAsignacionExamenV2DTO> ObtenerConfiguracionConfiguracionExamenPorProcesoSeleccionV2(int idProcesoSeleccion);
    }
}
