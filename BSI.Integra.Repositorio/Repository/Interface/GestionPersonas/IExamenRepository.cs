using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersona;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IExamenRepository : IGenericRepository<TExaman>

    {
        List<int> ObtenerIdGruposPorEvaluacion(int idEvaluacion);
        List<FactorComponenteDTO> ObtenerComponentePorEvalauacion(int idEvaluacion);
        List<EvaluacionPersonaCompletoDTO> ObtenerEvaluacionPersonaCompleto();
        List<Examen> ObtenerPorIdCriterioEvaluacionProceso(int idCriterioEvaluacionProceso);
        List<DatosExamenPostulanteDTO> ObtenerReporteExamenPostulante(EvaluacionPostulanteFiltroReporteDTO filtro);
        List<ConfiguracionExamenTestDTO> ObtenerConfiguracionPuntaje();
        List<string> ObtenerNombresExamenReportePostulante();
        List<ObtenerCalificacionCentilDTO> ObtenerInformacionCentilPorProcesoSeleccion(List<int> idsProcesoSeleccion);
        List<CentilDTO> ObtenerCentilesAsociados(IEnumerable<int> idsExamen);
        Examen? ObtenerPorId(int id);
        IEnumerable<ComboDTO> ObtenerExamenes();
        public List<ExamenVDTO> ObtenerComponentesAsociadosEvaluacion(int idEvaluacion);
        List<EstructuraBasicaDTO> ObtenerExamenNoAsignadoProcesoSeleccion(int IdProcesoSeleccion);
        List<ExamenAsignadoProcesoDTO> ObtenerExamenAsignadoProcesoSeleccion(int IdProcesoSeleccion);

        IEnumerable<ExamenVDTO> ObtenerEvaluacion();

    }
}
