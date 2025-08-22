
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ExamenAsignadoDTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int IdPostulante { get; set; }
        public bool? EstadoExamen { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdMigracion { get; set; }
        public bool? EstadoAcceso { get; set; }
        public int? VersionCentil { get; set; }
    }

    public class ConfiguracionAsignacionExamenV2DTO
    {
        public int Id { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdEvaluacion { get; set; }
        public int IdExamen { get; set; }
        public int NroOrden { get; set; }

    }

}
