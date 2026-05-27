using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    // CONTRATO ESPEJO - sync con WebHookWhatsApp/WhatsAppEnviarMensajePostulanteDTO.cs
    // Cualquier cambio aqui debe replicarse en el repo WebHookWhatsApp (DTOs/WhatsAppEnviarMensajePostulanteDTO.cs).
    // Este es el body que V5 reenvia (passthrough) al webhook
    // POST {WebHookWhatsApp.BaseUrl}/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGp (FR-4).
    public class EnviarMensajeWhatsAppPostulanteRequest
    {
        public int Id { get; set; }
        public string WaTo { get; set; } = string.Empty;
        public string? WaId { get; set; }
        public string WaType { get; set; } = string.Empty;
        public int? WaTypeMensaje { get; set; }
        public string? WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public int? IdMigracion { get; set; }
        public int IdPersonal { get; set; }
        public int? IdPostulante { get; set; }
        public string? usuario { get; set; }
        public List<DatosPlantillaWhatsAppEspejoDTO>? DatosPlantillaWhatsApp { get; set; }
        public List<BotonEspejoDTO>? botones { get; set; }
        public string? imagen { get; set; }
    }

    /// <summary>
    /// Placeholder espejo del tipo DatosPlantillaWhatsAppDTO usado por WebHookWhatsApp.
    /// El contrato exacto no esta exportado a V5; se mantiene como bag flexible para passthrough.
    /// </summary>
    public class DatosPlantillaWhatsAppEspejoDTO
    {
        public string? Nombre { get; set; }
        public string? Valor { get; set; }
        public string? Tipo { get; set; }
    }

    /// <summary>
    /// Placeholder espejo del tipo BotonDTO usado por WebHookWhatsApp.
    /// </summary>
    public class BotonEspejoDTO
    {
        public string? Tipo { get; set; }
        public string? Texto { get; set; }
        public string? Valor { get; set; }
    }
}
