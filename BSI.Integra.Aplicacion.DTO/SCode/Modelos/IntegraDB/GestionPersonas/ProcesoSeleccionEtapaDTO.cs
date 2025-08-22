
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ProcesoSeleccionEtapaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdProcesoSeleccion { get; set; }
        public string NombreProcesoSeleccion { get; set; }
        public int? NroOrden { get; set; }
    }

    public class ProcesosSeleccionEtapaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdProcesoSeleccion { get; set; }

    }
}
