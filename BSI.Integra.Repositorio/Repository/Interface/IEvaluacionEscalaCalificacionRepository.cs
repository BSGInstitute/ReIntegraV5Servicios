using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IEvaluacionEscalaCalificacionRepository : IGenericRepository<TEvaluacionEscalaCalificacion>
    {
        List<EvaluacionEscalaCalificacion> ObtenerPorModalidadCurso(int idModalidadCurso);
    }
}
