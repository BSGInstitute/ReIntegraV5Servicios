using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Repositorio.Repository.Implementation.Marketing.Messenger;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger
{
    public interface IMessengerFacebookChatRepository
    {
        List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo);
        List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request);
        List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> ObtenerDatosGeneralesAlumnosPorPSID(ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request);
        bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, int idAlumno, string usuario);
    }
}
