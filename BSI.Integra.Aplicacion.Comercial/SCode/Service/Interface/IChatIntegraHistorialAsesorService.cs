using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IChatIntegraHistorialAsesorService
    {
        List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatPorAsesor(int idPersonal);
        List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatsPorAlumno(int idAlumno);
    }
}
