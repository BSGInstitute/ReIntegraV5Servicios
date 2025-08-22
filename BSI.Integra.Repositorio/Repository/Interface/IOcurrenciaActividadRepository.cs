using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOcurrenciaActividadRepository : IGenericRepository<TOcurrenciaActividad>
    {
        List<ArbolOcurrenciaDTO> ObtenerArbolOcurrencia(int idActividadCabecera, int idOcurrenciaActividadPadre);
    }
}
