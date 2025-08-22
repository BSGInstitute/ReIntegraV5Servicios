using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IEstadoEtapaProcesoSeleccionService
    {
        IEnumerable<EstadoEtapaProcesoSeleccionDTO> Obtener();
        EstadoEtapaProcesoSeleccionDTO Insertar(EstadoEtapaProcesoSeleccionDTO dto, string usuario);
        EstadoEtapaProcesoSeleccionDTO Actualizar(EstadoEtapaProcesoSeleccionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
