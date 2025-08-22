using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FormularioSolicitud : BaseIntegraEntity
    {
        public int? IdFormularioRespuesta { get; set; }
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string Campanha { get; set; } = null!;
        public int? IdConjuntoAnuncio { get; set; }
        public string Proveedor { get; set; } = null!;
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public int TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; } = null!;
        public int TipoEvento { get; set; }
        //public string? UrlbotonInvitacionPagina { get; set; }
    }
}
