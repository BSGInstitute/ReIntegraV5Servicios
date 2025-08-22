using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FuentesPortalWeb : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? Url { get; set; }
        public string? Uauario { get; set; }

    }
}
