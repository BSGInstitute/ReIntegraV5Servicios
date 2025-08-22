using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ConfigurarVideoProgramaDTO
    {
        public int? Id { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string VideoId { get; set; } = null!;
        public string? TotalMinutos { get; set; }
        public string? Archivo { get; set; }
        public string? NroDiapositivas { get; set; }
        public bool Configurado { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string? ImagenVideoNombre { get; set; }
        public string? ImagenVideoAncho { get; set; }
        public string? ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string? ImagenDiapositivaNombre { get; set; }
        public string? ImagenDiapositivaAncho { get; set; }
        public string? ImagenDiapositivaAlto { get; set; }
        public int? NumeroFila { get; set; }
        public string? Token { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public string? VideoIdBrightcove { get; set; }
        public bool? Activo { get; set; }
        public List<SesionConfigurarVideoDTO> SesionConfigurarVideos { get; set; }
    }
    public class PreEstructuraCapituloProgramaDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string NombreTitulo { get; set; }
        public int IdSeccionTipoDetalle_PW { get; set; }
        public int NumeroFila { get; set; }
        public int TotalSegundos { get; set; }
        public string VideoId { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public int? Minuto { get; set; }
        public int? IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }
        public string VideoIdBrightcove { get; set; }
    }

    public class EstructuraCapituloProgramaAlternoDTO
    {
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string Nombre { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSesion { get; set; }
        public int OrdenFila { get; set; }
        public int OrdenCapitulo { get; set; }
        public int OrdenSeccion { get; set; }
        public int TotalSegundos { get; set; }
    }
    public class EstructuraCapituloProgramaDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPgeneral { get; set; }
        public int IdDocumentoSeccionPw { get; set; }
        public string Nombre { get; set; }
        public string Capitulo { get; set; }
        public string Sesion { get; set; }
        public string SubSesion { get; set; }
        public int OrdenFila { get; set; }
        public int OrdenCapitulo { get; set; }
        public int OrdenSeccion { get; set; }
        public int TotalSegundos { get; set; }
        public string VideoId { get; set; }
        public string Archivo { get; set; }
        public string NroDiapositivas { get; set; }
        public bool? ConImagenVideo { get; set; }
        public string ImagenVideoNombre { get; set; }
        public string ImagenVideoAncho { get; set; }
        public string ImagenVideoAlto { get; set; }
        public bool? ConImagenDiapositiva { get; set; }
        public string ImagenDiapositivaNombre { get; set; }
        public string ImagenDiapositivaAncho { get; set; }
        public string ImagenDiapositivaAlto { get; set; }
        public int? ImagenVideoPosicionX { get; set; }
        public int? ImagenVideoPosicionY { get; set; }
        public int? ImagenDiapositivaPosicionX { get; set; }
        public int? ImagenDiapositivaPosicionY { get; set; }
        public int? Minuto { get; set; }
        public int? IdTipoVista { get; set; }
        public int? NroDiapositiva { get; set; }
        public bool? ConLogoVideo { get; set; }
        public bool? ConLogoDiapositiva { get; set; }
        public string VideoIdBrightcove { get; set; }
    }
    public class ConfigurarVideoProgramaBasicoDTO
    {
        public int Id { get; set; }
        public int IdPGeneral { get; set; }
        public int NumeroFila { get; set; }
        public string NroDiapositivas { get; set; }
        public string TotalMinutos { get; set; }
    }
}
