using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadClasificacionOperacionesService
    {
        OportunidadClasificacionOperaciones ObtenerPorIdOportunidad(int idOportunidad);
    }
}
