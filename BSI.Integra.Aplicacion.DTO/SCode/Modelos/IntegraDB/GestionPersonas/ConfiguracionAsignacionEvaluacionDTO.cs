
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ConfiguracionAsignacionEvaluacionDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEvaluacion { get; set; }
        public int NroOrden { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
    }
}
