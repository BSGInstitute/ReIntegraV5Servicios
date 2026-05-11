using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsappLlamadum
    {
        public int Id { get; set; }
        public string WaId { get; set; } = null!;
        public string? CallId { get; set; }
        public string NumeroWhatsApp { get; set; } = null!;
        public string IdNumeroWhatsApp { get; set; } = null!;
        public int IdPais { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public int? IdPersonal { get; set; }
        public byte TipoLlamada { get; set; }
        public byte EstadoLlamada { get; set; }
        public string? MotivoFin { get; set; }
        public DateTime? FechaRinging { get; set; }
        public DateTime? FechaConexion { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? DuracionSegundos { get; set; }
        public string? ConsentimientoEstado { get; set; }
        public DateTime? ConsentimientoFecha { get; set; }
        public DateTime? ConsentimientoExpira { get; set; }
        public string? TemplateConsentimientoId { get; set; }
        public string? GrabacionUrl { get; set; }
        public string? GrabacionBlobNombre { get; set; }
        public string? SdpOffer { get; set; }
        public string? PayloadWebhookRaw { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
