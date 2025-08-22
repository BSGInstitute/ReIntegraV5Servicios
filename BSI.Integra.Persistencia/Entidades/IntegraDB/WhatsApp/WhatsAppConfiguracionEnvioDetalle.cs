using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class WhatsAppConfiguracionEnvioDetalle : BaseIntegraEntity
    {

        public int Id { get; set; }

        public int IdWhatsAppConfiguracionLogEjecucion { get; set; }

        public bool EnviadoCorrectamente { get; set; }

        public string MensajeError { get; set; } = null!;

        public int IdConjuntoListaResultado { get; set; }

        public int ConjuntoListaNroEjecucion { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;
     
        public string UsuarioModificacion { get; set; } = null!;
       
        public DateTime FechaCreacion { get; set; }
    
        public DateTime FechaModificacion { get; set; }
   
        public byte[] RowVersion { get; set; } = null!;

        public int? IdMigracion { get; set; }
 
        public string? Mensaje { get; set; }

        public string? WhatsAppId { get; set; }

        public bool? DescartarCrearOportunidad { get; set; }

        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public DateTime? FechaEnvio { get; set; }

    }
}
