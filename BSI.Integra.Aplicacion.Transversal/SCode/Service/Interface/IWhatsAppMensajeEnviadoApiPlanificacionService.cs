using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion.WhatsAppMensajeEnviadoApiPlanificacionDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IWhatsAppMensajeEnviadoApiPlanificacionService
    {
        RespuestaMensajeWhatsappPlaDTO EnvioMensajePorPlantilla(WhatsAppMensajePlantillaPlaDTO json, string usuario, int idPersonal);
        bool EnvioMensajePorTexto(WhatsAppMensajeTextoPlaDTO json, string usuario, int idPersonal);
        bool EnvioMensajePorArchivo(WhatsAppMensajeArchivoPlaDTO json, string usuario, int idPersonal);
    }
}
