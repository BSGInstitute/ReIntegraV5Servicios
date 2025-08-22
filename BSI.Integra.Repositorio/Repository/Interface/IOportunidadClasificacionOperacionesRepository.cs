using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadClasificacionOperacionesRepository : IGenericRepository<TOportunidadClasificacionOperacione>
    {
        OportunidadClasificacionOperaciones? ObtenerPorIdOportunidad(int idOportunidad);
        void CalcularPorIdOportunidad(int idOportunidad);
    }
}
