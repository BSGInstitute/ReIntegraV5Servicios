using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface
{
    public interface IPostulanteWhatsAppService
    {
        WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensaje(WhatsAppMensajeEnviadoPostulanteDTO whatsAppEnviarMensajeDTO);
        IEnumerable<WhatsAppMensajesPostulanteDTO> WhatsAppUltimoMensajeRecibidosChat(int IdPersonal);
        PlantillaWhatsAppPostulante GenerarPlantillaGPWhatsapp(GestionPersonasPlantillaWhatsAppDTO Plantilla);
        bool ValidarMensajeRecibido24Horas(string numero);
        bool ValidarUltimaPlantillaEnviada(string plantilla, string numero);
        List<WhatsAppHistorialPostulanteMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area);
        RespuestaMensajeWhatsappDTO EnvioMensajePorPlantilla(WhatsAppMensajePostulantePlantillaComDTO json, string usuario, int idPersonal);
        bool EnvioMensajePorTexto(WhatsAppMensajeTextoPostulanteDTO json, string usuario, int idPersonal);
        public List<RespuestaMensajeWhatsappDTO> EnvioMensajesPorPlantillaMasivo(
            EnvioPlantillaPostulanteDTO DatosEnvio);
        bool EnvioMensajePorArchivo(WhatsAppMensajeArchivoPostulanteDTO json, string usuario, int idPersonal);

    }
}
