using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger
{
    public interface IMessengerFacebookChatService
    {
        List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo);
        List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request);
        List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> ObtenerDatosGeneralesAlumnosPorPSID(ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request);
        Task<EnviarMensajeResponse> EnviarMensajeTexto(EnviarMensajeTextoRequest request);
        bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, string usuario);
        Task<DatosExtraccionRegistrosResponseDTO> CapturarRegistrosModeloIA(DatosExtraccionRegistrosMessengerDTO datosExtraccionRegistrosMessenger);
        Task<DesactivarInteraccionResponseDTO> DesactivarInteraccionAutomaticaMessenger(string identificadorAmbitoPagina, string idAlumno);
    }
}
