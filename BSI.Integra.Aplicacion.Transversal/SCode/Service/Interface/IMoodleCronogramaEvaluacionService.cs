using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    public interface IMoodleCronogramaEvaluacionService
    {
        List<CronogramaAutoEvaluacionV2DTO> ObtenerCronogramaAutoEvaluacion_UltimaVersion(int idMatriculaCabecera);
        RespuestaWebDTO CongelarCronograma(int idMatriculaCabecera);
        List<IdentificadorCursoMoodlePorMatriculaComboDTO> ObtenerComboCursosMoodlePorMatricula(int idMatriculaCabecera);
        int? ObtenerIdCursoMoodlePrimeraActividadPendiente(int idMatriculaCabecera);
        bool ExistePorId(int idMatriculaCabecera);
        RespuestaWebDTO ReprogramarCronograma(int idMatriculaCabecera, int idEvaluacionMoodle, int diasRecorrer, bool recorreCronograma, string usuario);
    }
}
