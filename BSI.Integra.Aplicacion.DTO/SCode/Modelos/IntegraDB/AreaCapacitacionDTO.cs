using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AreaCapacitacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? ImgPortada { get; set; }
        public string? ImgSecundaria { get; set; }
        public string? ImgPortadaAlt { get; set; }
        public string? ImgSecundariaAlt { get; set; }
        public bool EsVisibleWeb { get; set; }
        public int? IdArea { get; set; }
        public bool EsWeb { get; set; }
        public string? DescripcionHtml { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
        public string? ColorArea { get; set; }
        public string? UrlIconoArea { get; set; }
    }
    public class AreaCapacitacionFiltroDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? IdAreaCapacitacionFacebook { get; set; }
    }

    public class ParametroContenidoDatosDTO
    {
        public int Id { get; set; }
        public string NombreParametroSeo { get; set; }
        public int NumeroCaracteresParametrosSeo { get; set; }
        public string ContenidoParametroSeo { get; set; }
    }

    public class CompuestoAreaDTO
    {
        public AreaCapacitacionDTO AreaCapacitacion { get; set; }
        public List<AreaParametrosSeoPorIdAreaDTO> ListaParametro { get; set; }
    }
}
