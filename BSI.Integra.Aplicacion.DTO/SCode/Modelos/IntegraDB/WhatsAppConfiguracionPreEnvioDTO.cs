using BSI.Integra.Aplicacion.Base;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppConfiguracionPreEnvioDTO : BaseIntegraEntity
    {
        public int Id { get; set; }
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdCodigoPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public string objetoplantilla { get; set; }
        public bool Procesado { get; set; }
    }
    public class WhatsAppConfiguracionPreEnvioBO : BaseIntegraEntity
    {
        public int IdWhatsappMensajePublicidad { get; set; }
        public int IdConjuntoListaResultado { get; set; }
        public int IdAlumno { get; set; }
        public string Celular { get; set; }
        public int IdPais { get; set; }
        public int NroEjecucion { get; set; }
        public bool Validado { get; set; }
        public string Plantilla { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPGeneral { get; set; }
        public int? IdPlantilla { get; set; }
        public int? IdWhatsAppEstadoValidacion { get; set; }
        public string objetoplantilla { get; set; }
        public bool Procesado { get; set; }
        public string MensajeProceso { get; set; }
        public int IdConjuntoListaDetalle { get; set; }
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
    }

    public class PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO
    {
        public int Id { get; set; }
        public string CelularWhatsApp { get; set; }
        public int IdAlumno { get; set; }
        public int WhatsAppEmpresaIdPais { get; set; }
        public string MensajePlantillaHtml { get; set; }
        public string ObjetoPlantilla { get; set; }
        public int IdPlantilla { get; set; }
        public int IdCampaniaGeneralDetalleResponsableWhatsApp { get; set; }
        public int IdPersonal { get; set; }
        public string Descripcion { get; set; }
        public string WaId { get; set; } = null;
        public int Dias { get; set; }

    }

    public class MensajeEnviadoErroneoWhatsappLogDTO
    {
        public int Id { get; set; }
        public string? CelularWhatsapp { get; set; } 
        public int? IdAlumno { get; set; }
        public int? IdCampaniaGeneralDetalleResponsableWhatsapp { get; set; } 
        public int? IdPlantilla { get; set; }
        public string? MensajePlantillaHtml { get; set; }
        public string? ObjetoPlantilla { get; set; }
        public int? IdPais { get; set; }
        public string? NumeroEnviado { get; set; } 
        public string? MensajeErroneo { get; set; } 
        public string? WaId { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }


    }
}
