using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PespecificoFrecuenciaDetalle : BaseIntegraEntity
    {
        public int? IdPespecificoFrecuencia { get; set; }
        public byte DiaSemana { get; set; }
        public TimeSpan HoraDia { get; set; }
        public decimal Duracion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
