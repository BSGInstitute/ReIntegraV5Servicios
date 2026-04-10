using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using System.Collections.Generic;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas
{
    public interface IChatbotActividadBotIARepository
    {
        List<ChatbotActividadBotIAListadoDTO> ObtenerListadoChatbotActividadBotIA();
        List<ChatbotActividadBotIANumeroDTO>  ObtenerNumerosPorActividades();
        List<MedioComunicacionDTO>            ObtenerListadoMedioComunicacion();
        int  InsertarChatbotActividadBotIA(InsertarChatbotActividadBotIADTO request, string usuario);
        void InsertarChatbotActividadBotIANumero(int idChatbotActividadBotIA, int idAsistenteMarketingWhatsAppAsignacion, bool estado, string usuario);
        void EliminarChatbotActividadBotIANumeros(int idChatbotActividadBotIA, string usuario);
        void DesactivarNumerosWhatsAppDeActividad(int idChatbotActividadBotIA, string usuario);
        void ReactivarNumerosWhatsAppDeActividad(int idChatbotActividadBotIA, string usuario);
        void ActualizarChatbotActividadBotIA(ActualizarChatbotActividadBotIADTO request, string usuario);
        void EliminarChatbotActividadBotIA(int idChatbotActividadBotIA, string usuario);
    }
}
