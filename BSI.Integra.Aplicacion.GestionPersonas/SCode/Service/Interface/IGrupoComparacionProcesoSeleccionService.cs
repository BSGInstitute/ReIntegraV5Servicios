using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IGrupoComparacionProcesoSeleccionService
    {
        IEnumerable<GrupoComparacionProcesoSeleccionDTO> Obtener();
        GrupoComparacionProcesoSeleccionDTO Insertar(GrupoComparacionProcesoSeleccionDTO dto, string usuario);
        GrupoComparacionProcesoSeleccionDTO Actualizar(GrupoComparacionProcesoSeleccionDTO dto, string usuario);
        bool Eliminar(int id, string usuario);
        (IEnumerable<ComboDTO> puestosTrabajo, IEnumerable<ComboDTO> sedesTrabajo) ObtenerCombosModulo();
    }
}
