using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAsesorChatService
    {
        PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero);

        bool NotificarError(string Objeto);
        PersonalAlumnoDTO BuscarAlumnoPorWebHook(string Celular);
        List<ChatAsignadoNoAsignadoDTO> ObtenerTodoChatAsignados();
    }
}
