using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IFrecuenciaPuestoTrabajoService
    {
        IEnumerable<FrecuenciaPuestoTrabajoDTO> Obtener();
        FrecuenciaPuestoTrabajoDTO Insertar(FrecuenciaPuestoTrabajoDTO dto, string usuario);
        FrecuenciaPuestoTrabajoDTO Actualizar(FrecuenciaPuestoTrabajoDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
    }
}
