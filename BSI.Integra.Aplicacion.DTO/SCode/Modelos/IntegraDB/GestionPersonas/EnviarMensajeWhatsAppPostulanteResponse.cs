namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    /// <summary>
    /// Respuesta del passthrough al webhook WhatsAppMensajeApiGraphGp (FR-4).
    /// El webhook responde con el Id insertado y el WaId asignado por Meta.
    /// </summary>
    public class EnviarMensajeWhatsAppPostulanteResponse
    {
        public int IdMensajeEnviado { get; set; }
        public string WaId { get; set; } = string.Empty;
        public bool Exito { get; set; }
        public string? Mensaje { get; set; }
    }
}
