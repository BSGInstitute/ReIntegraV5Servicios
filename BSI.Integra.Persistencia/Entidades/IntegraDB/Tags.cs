using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Tags : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Texto { get; set; }
        public string? NombreTipo { get; set; }

    }
}
