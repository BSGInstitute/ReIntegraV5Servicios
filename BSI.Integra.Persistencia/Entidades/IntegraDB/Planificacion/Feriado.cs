using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class Feriado : BaseIntegraEntity
    {
        public int? Tipo { get; set; }
        public DateTime Dia { get; set; }
        public string Motivo { get; set; } = null!;
        public int Frecuencia { get; set; }
        public int IdTroncalCiudad { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
