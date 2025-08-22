using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class WhatsAppConfiguracionEnvioDetalleDTO
    {

        public int Id { get; set; }

        public int IdWhatsAppConfiguracionLogEjecucion { get; set; }
        
        public bool EnviadoCorrectamente { get; set; }

        public string MensajeError { get; set; } = null!;
 
        public int IdConjuntoListaResultado { get; set; }

        public int ConjuntoListaNroEjecucion { get; set; }

        public string? Mensaje { get; set; }
 
        public string? WhatsAppId { get; set; }

        public bool? DescartarCrearOportunidad { get; set; }
 
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public DateTime? FechaEnvio { get; set; }


    }
}
