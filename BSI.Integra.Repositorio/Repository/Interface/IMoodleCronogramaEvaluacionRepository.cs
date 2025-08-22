using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IMoodleCronogramaEvaluacionRepository : IGenericRepository<TMoodleCronogramaEvaluacion>
    {
        List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacionUltimaVersion(int idMatriculaCabecera);
        RespuestaWebDTO CongelarCronograma(int idMatriculaCabecera);
        IEnumerable<MoodleCronogramaEvaluacionDTO> ObtenerPorIdMatriculaCabeceraYVersion(int idMatriculaCabecera, int version);
        List<VersionCronogramaAutoEvaluacionDTO> ObtenerVersionesCronograma(int idMatriculaCabecera);
        List<IdentificadorCursoMoodlePorMatriculaComboDTO> ObtenerComboCursosMoodlePorMatricula(int idMatriculaCabecera);
        int? ObtenerIdCursoMoodlePrimeraActividadPendiente(int idMatriculaCabecera);
        List<CronogramaListaCursosOnlineV2PromedioDTO> ObtenerCronogramaAutoEvaluacionUltimaVersionPromedio(int idMatriculaCabecera);
        RespuestaWebDTO ReprogramarCronograma(int idMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma);
        bool ExistePorIdMatriculaCabecera(int idMatriculaCabecera);
        List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersionPorCurso(int idMatriculaCabecera, int idCursoMoodle);
        List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_PorVersion(int idMatriculaCabecera, int version);

    }
}
