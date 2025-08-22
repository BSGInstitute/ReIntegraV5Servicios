using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.Configuracion
{
    public interface IConfiguracionIntegraRepository
    {
        bool ObtenerEstadoValidacionIp();
        List<ClaveValorDTO> ObtenerApisValidacionIp();
    }
}
