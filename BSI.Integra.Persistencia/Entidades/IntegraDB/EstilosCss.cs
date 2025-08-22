using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class EstilosCss : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoCss { get; set; }
        public string? NombreTipo { get; set; }
        public string? Uauario { get; set; }

    }
}
