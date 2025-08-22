using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Aplicacion.DTO.Modelos
{
    public class WhatsAppMensajePublicidadDTO : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPais { get; set; }
        public bool EsValido { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }
    public class DatosALumnoWhatsappDTO
    {
        public int IdAlumno { set; get; }
        public int IdCodigoPais { set; get; }
        public string Celular { set; get; }
        public int IdPersonal { set; get; }
        public string UserName { set; get; }
        public bool EsMarketing { set; get; }

    }
}
