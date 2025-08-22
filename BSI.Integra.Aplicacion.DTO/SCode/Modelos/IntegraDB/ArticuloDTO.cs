namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ArticuloDTO
    {
        public int? Id { get; set; }
        public int? IdWeb { get; set; }
        public string? Nombre { get; set; }
        public string? Titulo { get; set; }
        public string? ImgPortada { get; set; }
        public string? ImgPortadaAlt { get; set; }
        public string? ImgSecundaria { get; set; }
        public string? ImgSecundariaAlt { get; set; }
        public string? Autor { get; set; }
        public int? IdTipoArticulo { get; set; }
        public string? NombreArticulo { get; set; }
        public string? Contenido { get; set; }
        public int? IdArea { get; set; }
        public string? NombreArea { get; set; }
        public int? IdSubArea { get; set; }
        public string? NombreSubArea { get; set; }
        public int? IdExpositor { get; set; }
        public string? NombreExpositor { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoriaPrograma { get; set; }
        public string? UrlWeb { get; set; }

        public string? UrlDocumento { get; set; }
        public string? DescripcionGeneral { get; set; }
        public string Usuario { get; set; }

        public List<DatosInsertarParametroSeoDTO>? parametroSeo { get; set; }


    }

    public class ArticuloCompuestFiltroTotalDTO
    {
        public List<ArticuloCompuestoDTO> data { get; set; }
        public int Total { get; set; }
    }
    public class ArticuloCompuestoDTO
    {
        public int Total { get; set; }
        public int Id { get; set; }
        public int IdWeb { get; set; }
        public string? Nombre { get; set; }
        public string? Titulo { get; set; }
        public string? ImgPortada { get; set; }
        public string? ImgPortadaAlt { get; set; }
        public string? ImgSecundaria { get; set; }
        public string? ImgSecundariaAlt { get; set; }
        public string? Autor { get; set; }
        public int? IdTipoArticulo { get; set; }
        public string? NombreTipo { get; set; }
        public string? NombreArticulo { get; set; }
        public string? Contenido { get; set; }
        public int? IdArea { get; set; }
        public string? NombreArea { get; set; }
        public int? IdSubArea { get; set; }
        public string? NombreSubArea { get; set; }
        public int? IdExpositor { get; set; }
        public string? NombreExpositor { get; set; }
        public int? IdCategoria { get; set; }
        public string? NombreCategoriaPrograma { get; set; }
        public string? UrlWeb { get; set; }

        public string? UrlDocumento { get; set; }
        public string? DescripcionGeneral { get; set; }
    }
    public class ArticulosTagAsociadosDTO
    {
        public int IdArticulo { get; set; }
        public List<int> IdsAsociados { get; set; }
        public string Usuario { get; set; }
    }
    public class ArticulosProgramasAsociadosDTO
    {
        public int IdArticulo { get; set; }
        public List<int> IdsAsociados { get; set; }
        public string Usuario { get; set; }
    }
    public class InsertarArticuloParametroSeoDTO
    {
        public ArticuloDTO Formulario { get; set; }
        public List<DatosInsertarParametroSeoDTO> parametroSeo { get; set; }
    }
}
