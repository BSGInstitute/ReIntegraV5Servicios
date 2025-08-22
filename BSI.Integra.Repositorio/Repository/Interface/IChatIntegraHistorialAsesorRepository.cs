using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IChatIntegraHistorialAsesorRepository
    {
        List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatPorAsesor(int idPersonal);
        List<ChatHistorialAsesorDTO> ObtenerTodoHistorialChatsPorAlumno(int idAlumno);
    }
}
