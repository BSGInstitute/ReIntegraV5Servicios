using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionIntegraService
    {
        bool ObtenerEstadoValidacionIp();
        List<ClaveValorDTO> ObtenerApisValidacionIp();
    }
}
