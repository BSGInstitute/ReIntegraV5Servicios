using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IEvaluacionEscalaCalificacionService
    {
        EvaluacionEscalaCalificacion ObtenerEscalaPorPEspecificoPresencial(int idPespecifico);
        List<EvaluacionEscalaCalificacion> ObtenerPorModalidadCurso(int idModalidadCurso);
    }
}
