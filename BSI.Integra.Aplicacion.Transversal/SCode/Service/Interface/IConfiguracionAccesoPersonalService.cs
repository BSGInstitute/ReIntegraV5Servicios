using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IConfiguracionAccesoPersonalService
    {
        IEnumerable<ConfiguracionAccesoPersonalDTO> ObtenerPorIdPersonal(int idPersonal);
        ConfiguracionAccesoPersonalDTO? ObtenerPorIdPersonalIdModulo(int idPersonal, int idModulo);
        int ObtenerIdPersonalAcceso(int idPersonal, string urlModulo);
    }
}
