
using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiComercialDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IWhatsAppMensajeEnviadoApiComercialService
    {
        bool EnvioMensajePorTexto(WhatsAppMensajeTextoComDTO json, string usuario, int idPersonal);
    }
}
