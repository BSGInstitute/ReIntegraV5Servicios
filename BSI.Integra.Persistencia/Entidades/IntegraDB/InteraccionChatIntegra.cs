using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class InteraccionChatIntegra : BaseIntegraEntity
    {

        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int? IdAlumno { get; set; }
        public Guid? IdContactoPortalSegmento { get; set; }
        public int? IdTipoInteraccion { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdSubAreaCapacitacion { get; set; }
        public int? IdAreaCapacitacion { get; set; }
        public string Ip { get; set; }
        public string Pais { get; set; }
        public string Region { get; set; }
        public string Ciudad { get; set; }
        public int? Duracion { get; set; }
        public int? NroMensajes { get; set; }
        public int? NroPalabrasVisitor { get; set; }
        public int? NroPalabrasAgente { get; set; }
        public int? UsuarioTiempoRespuestaMaximo { get; set; }
        public int? UsuarioTiempoRespuestaPromedio { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Leido { get; set; }
        public string Plataforma { get; set; }
        public string Navegador { get; set; }
        public string UrlFrom { get; set; }
        public string UrlTo { get; set; }
        public int IdEstadoChat { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        public Guid IdChatSession { get; set; }
        public Guid? IdFaseOportunidadPortalWeb { get; set; }
        public Guid? IdMigracion { get; set; }
        public decimal? ClienteTiempoEspera { get; set; }
        public int? ContadorUsuarioPromedioRespuesta { get; set; }
        public decimal? TiempoRespuestaTotal { get; set; }
        public int? NroMensajesSinLeer { get; set; }
    }
}
