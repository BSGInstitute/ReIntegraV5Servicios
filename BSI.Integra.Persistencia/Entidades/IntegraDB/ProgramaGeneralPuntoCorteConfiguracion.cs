using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralPuntoCorteConfiguracion : BaseIntegraEntity
    {
        public int IdTipoCorte { get; set; }
        public string Tipo { get; set; } = null!;
        public int IdAreaCapacitacion { get; set; }
        public int IdSubAreaCapacitacion { get; set; }
        public int IdPgeneral { get; set; }
        public string? Color { get; set; }
        public string? Texto { get; set; }
    }
}

