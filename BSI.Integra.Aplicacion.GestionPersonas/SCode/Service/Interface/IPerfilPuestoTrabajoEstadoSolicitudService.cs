using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IPerfilPuestoTrabajoEstadoSolicitudService
    {
        IEnumerable<PerfilPuestoTrabajoEstadoSolicitudDTO> Obtener();
        PerfilPuestoTrabajoEstadoSolicitudDTO Insertar(PerfilPuestoTrabajoEstadoSolicitudDTO dto, string usuario);
        PerfilPuestoTrabajoEstadoSolicitudDTO Actualizar(PerfilPuestoTrabajoEstadoSolicitudDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
