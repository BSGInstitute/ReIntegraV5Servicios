using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ConfigurarWebinar : BaseIntegraEntity
    {
        public int IdPespecifico { get; set; }
        public string Modalidad { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int IdOperadorComparacionAvance { get; set; }
        public int ValorAvance { get; set; }
        public int? ValorAvanceOpc { get; set; }
        public int IdOperadorComparacionPromedio { get; set; }
        public int ValorPromedio { get; set; }
        public int? ValorPromedioOpc { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPespecificoPadre { get; set; }
    }
}
