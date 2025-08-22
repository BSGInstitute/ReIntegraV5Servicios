using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Articulo : BaseIntegraEntity
    {
        public int? IdWeb { get; set; }
        public string Nombre { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string? ImgPortada { get; set; }
        public string? ImgPortadaAlt { get; set; }
        public string? ImgSecundaria { get; set; }
        public string? ImgSecundariaAlt { get; set; }
        public string? Autor { get; set; }
        public int? IdTipoArticulo { get; set; }
        public string? Contenido { get; set; }
        public int? IdArea { get; set; }
        public int? IdSubArea { get; set; }
        public int? IdExpositor { get; set; }
        public int? IdCategoria { get; set; }
        public string? UrlWeb { get; set; }
        public string? UrlDocumento { get; set; }
        public string? DescripcionGeneral { get; set; }
    }
}
