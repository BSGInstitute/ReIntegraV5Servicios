namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppEstadoMensajeEnviadoDTO
    {
        public int Id { get; set; }
        public string? WaId { get; set; }
        public string? WaRecipientId { get; set; }
        public string? WaStatus { get; set; }
        public string? WaTimeStamp { get; set; }
        public int IdPais { get; set; }
        public bool? EsMigracion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class WhatsAppEstadoMensajeEnviadoComboDTO
    {
        public int Id { get; set; }
        public string? WaId { get; set; }
        public string? WaStatus { get; set; }
    }
    public class WhatsAppMensajeEnviadoAutomaticoDTO
    {
        public int Id { get; set; }
        public string WaTo { get; set; }
        public string WaId { get; set; }
        public string WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string WaRecipientType { get; set; }
        public string WaBody { get; set; }
        public string WaFile { get; set; }
        public string WaFileName { get; set; }
        public string WaMimeType { get; set; }
        public string WaSha256 { get; set; }
        public string WaLink { get; set; }
        public string WaCaption { get; set; }
        public List<DatoPlantillaWhatsAppDTO> datosPlantillaWhatsApp { get; set; }
    }
}
