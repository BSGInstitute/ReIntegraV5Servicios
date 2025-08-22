using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICorreoGmailService
    {
        List<CorreoDTO> FiltroCorreosPorPersona(int idFolder, string queryFiltro);
        List<CorreoDTO> FiltroCorreosPorPersonaGmailCorreo(string queryFiltro);
        int ContadorCorreosPorPersona(int idPersonal, int idFolder);
        ListaCorreosGrupoDTO ObtenerCorreosGruposSinVersion(int idCentroCosto, List<int> estado, List<int> subEstado);
        ListaCorreosGrupoDTO ObtenerCorreosGruposConVersion(int idCentroCosto, int idPaquete, List<int> estado, List<int> subEstado);
    }
}
