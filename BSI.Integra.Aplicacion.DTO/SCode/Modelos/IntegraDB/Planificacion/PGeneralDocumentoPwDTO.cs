namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class PGeneralDocumentoPwDTO
    {
        public int? Id { get; set; }
        public int IdDocumento { get; set; }
    }
    public class PreEstructuraProgramaDTO
    {
        public int Id { get; set; }
        public int IdConfigurarVideoPrograma { get; set; }
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public string NombreTitulo { get; set; }
        public int IdSeccionTipoDetallePw { get; set; }
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
    public class CapituloSesionProgramaCapacitacionDTO
    {
        public int IdPGeneral { get; set; }
        public int IdCapituloProgramaCapacitacion { get; set; }
        public string CapituloProgramaCapacitacion { get; set; }
        public List<SesionSubSeccionProgramaCapacitacionDTO> ListaSesionesProgramaCapacitacion { get; set; }
    }

    public class SesionSubSeccionProgramaCapacitacionDTO
    {
        public int IdSesionProgramaCapacitacion { get; set; }
        public string SesionProgramaCapacitacion { get; set; }
        public List<SubSeccionProgramaCapacitacionDTO> ListaSubSeccionProgramaCapacitacion { get; set; }
    }

    public class SubSeccionProgramaCapacitacionDTO
    {
        public int IdSesionProgramaCapacitacion { get; set; }
        public string SubSeccionProgramaCapacitacion { get; set; }
    }
}
