using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiAtcDTO;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppMensajeEnviadoApiAtcRepository
    {
        Task<bool>            TieneBotAsignado(int idPersonal);
        Task<HiloChatAtcDTO?> ObtenerHiloAbierto(int idAlumno);
        Task<int>             CrearHiloChat(int? idAlumno, string numeroWhatsApp, string usuario);
        Task                  ActualizarHiloAsesor(int idHilo, string usuario);
        Task<long>            InsertarMensajeChatbotCompleto(long idWhatsAppMensaje, int idHilo, int idActor, string usuario);
        Task<bool>            FinalizarConversacion(int idAlumno, string waTo, string usuario);
    }
}
