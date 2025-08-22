using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoFrecuencia : BaseIntegraEntity
    {
        public int? IdPespecifico { get; set; }
        public DateTime FechaInicio { get; set; }
        public int Frecuencia { get; set; }
        public int NroSesiones { get; set; }
        public int? IdFrecuencia { get; set; }
        public Guid? IdMigracion { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
