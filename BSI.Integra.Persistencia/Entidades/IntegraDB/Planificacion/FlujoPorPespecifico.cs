using BSI.Integra.Aplicacion.Base;
namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FlujoPorPespecifico : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public int IdFlujoActividad { get; set; }
        public int? IdFlujoOcurrencia { get; set; }
        public int IdClasificacionPersona { get; set; }
        public DateTime? FechaEjecucion { get; set; }
        public DateTime? FechaSeguimiento { get; set; }
        public int? IdMigracion { get; set; }
    }
}
