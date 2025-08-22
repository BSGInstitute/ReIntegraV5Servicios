using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Planificacion
{
    public interface IEscalaCalificacionDetalleRepository : IGenericRepository<TEscalaCalificacionDetalle>
    {
        bool Delete(int id, string usuario);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        IEnumerable<EscalaCalificacionDetalle> ObtenerTodo();
        EscalaCalificacionDetalle ObtenerPorId(int id);
        IEnumerable<EscalaCalificacionDetalle> ObtenerPorIdEscalaCalificacion(int idEscalaCalificacion);
        List<EscalaCalificacionDetalle> ObtenerPorIds(List<int> id);
    }
}
