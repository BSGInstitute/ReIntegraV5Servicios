using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppMensajePublicidad : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int? IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        [StringLength(50)]
        public string Celular { get; set; } = null!;
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        public int IdPais { get; set; }
        public bool EsValido { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }
}
