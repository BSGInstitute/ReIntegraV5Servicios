using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class ResumenMensajesWhatsAppRespondidosDTO
    {
        public int IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public int? IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string NombrePrograma { get; set; }
        public int? IdCentroCosto { get; set; }
        public string NombreCentroCosto { get; set; }
        public string Celular { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public int IdWhatsAppConfiguracionEnvioDetalle { get; set; }
        public DateTime FechaEnvio { get; set; }
        public bool? EsDesucrito { get; set; }
        public string UltimoMensajeRecibido { get; set; }
        public int OportunidadesSeguimiento { get; set; }
        public DateTime FechaUltimoMensaje { get; set; }
        public int? IdWhatsAppConfiguracionEnvioDetalleOportunidad { get; set; }
        public int? IdOportunidad { get; set; }
        public bool EsRespondido { get; set; }
        public DateTime? FechaCreacionOportunidad { get; set; }

    }
}
