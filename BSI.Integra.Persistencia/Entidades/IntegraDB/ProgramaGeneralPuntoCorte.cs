using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralPuntoCorte : BaseIntegraEntity
    {
        public int? IdProgramaGeneral { get; set; }
        public decimal? PuntoCorteMedia { get; set; }
        public decimal? PuntoCorteAlta { get; set; }
        public decimal? PuntoCorteMuyAlta { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdPais { get; set; }
    }
}
