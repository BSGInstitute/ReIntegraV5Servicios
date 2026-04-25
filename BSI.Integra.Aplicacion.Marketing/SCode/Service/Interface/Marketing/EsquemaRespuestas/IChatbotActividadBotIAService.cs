using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas
{
    public interface IChatbotActividadBotIAService
    {
        List<ChatbotActividadBotIAListadoDTO> ObtenerListadoChatbotActividadBotIA();
        List<MedioComunicacionDTO>            ObtenerListadoMedioComunicacion();
        void InsertarChatbotActividadBotIA(InsertarChatbotActividadBotIADTO request, string usuario);
        void ActualizarChatbotActividadBotIA(ActualizarChatbotActividadBotIADTO request, string usuario);
        void EliminarChatbotActividadBotIA(EliminarChatbotActividadBotIADTO request, string usuario);
    }
}
