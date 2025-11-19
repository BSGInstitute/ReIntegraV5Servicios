using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger
{
    public interface IMessengerFacebookChatService
    {
        List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo);
        List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request);
        List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> ObtenerDatosGeneralesAlumnosPorPSID(ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request);
        Task<EnviarMensajeResponse> EnviarMensajeTexto(EnviarMensajeTextoRequest request);
        bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, int idAlumno, string usuario);
    }
}
