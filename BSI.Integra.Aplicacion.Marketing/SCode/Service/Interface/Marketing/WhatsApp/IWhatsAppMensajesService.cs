using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp
{
    public interface IWhatsAppMensajesService
    {
        WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensaje(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO);
        WhatsAppMensajeEnviadoRespuestaDTO WhatsAppMensajeAlumnoAccesos(PlantillaWhatsAppEnvioAccesoDTO resultado);
        WhatsAppMensajeEnviadoRespuestaDTO EnviarMensajeWhatsapp(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO, string usuario);
        object WhatsAppMensajeVersionTemplate(WhatsAppEnviarMensajeDTO whatsAppEnviarMensajeDTO);
        object WhatsAppMensajeMultimedia(string waId);
        object AdjuntarArchivoWhatsApp(IFormFile file);
        object WhatsAppUltimoMensajeChat(int idPersonal);
        object WhatsAppUltimoMensajeRecibidosChat(int idPersonal);
        object HistorialMensajeRecibidosChat(int idPersonal, string numero, string area);
        object HistorialMensajeRecibidosChat(int idPersonal, string numero, string area, int idTipoAgenda);
        object WhatsAppUltimoMensajeEnviadosChat(int idPersonal);
        object WhatsAppHistorialMensajeChat(int idPersonal, string numero, string area);
        object WhatsAppObtenerMensajeMultimedia(string waId);
        object WhatsAppObtenerMensajeMultimediaPla(string waId);
        object ObtenerConversacionPorNumero(string numero, int idPais);
        PersonalAlumnoDTO ObtenerConversacionPorOportunidad(string numero, int idPais);
        PersonalAlumnoDTO ObtenerPersonalConfiguracion(string numero, int idCentroCosto, int idPais);
        bool ValidarNumeroLibre(string numero, int idPais, int idCentroCosto, int idPersonal);
        PersonalNumeroMinimoChatDTO ObtenerAsesorConMenorChatsOffLine();
        bool ValidarPlantillasEnviadas(string plantilla, string numero);
        bool ValidarPlantillasEnviadasComercial(string plantilla, string numero, int IdPersonal, int IdCodigoPais, int idPersonalAsignado);
        bool ValidarMesajeRecibidosApiComercial(string numero);
        bool ValidarPlantillasEnviadasNuevoWebHook(string plantilla, string numero);
        bool ValidarMesajesEnviadosEn24Horas(string numero);
        bool ValidarMesajesEnviadosEn24HorasNuevoWebHook(string numero);
        bool ValidarMesajesEnviadosEn24HorasComercial(string numero, int IdPersonal, int idCodigoPais, int idPersonalAsignado);
        object ObtenerOportunidadPorAsesorYAlumno(int idPersonal, int idAlumno, string numero);
        object WhatsAppUltimoMensajeRecibidosPorOportunidad(Dictionary<string, string> filtro);
        object WhatsAppUltimoMensajeRecibidosChatControlMensajes(int idPersonal);
        object WhatsAppHistorialMensajeChatControlMensaje(int idPersonal, string numero, string area);
        void WhatsAppNotificacionesMensaje(WhatsappEnvioDTO envio);
        void ActualizarEstadoMarcador(MarcadorAsesorDTO item);
        KeyValuePair<string, AsesorSignalDTO> VerificarAsesorOnline(int IdPersonal);
    }
}
