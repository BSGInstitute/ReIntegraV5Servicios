using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppConfiguracionService
    {
        WhatsAppHostDatosDTO ObtenerCredencialHost(int idPais);
        List<WhatsAppHostDatosDTO> ObtenerCredencialHostGeneral();
    }
}
