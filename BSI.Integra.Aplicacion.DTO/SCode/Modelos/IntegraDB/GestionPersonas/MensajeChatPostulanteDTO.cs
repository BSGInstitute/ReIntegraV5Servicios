using System;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    /// <summary>
    /// DTO de un mensaje individual del hilo cronologico crudo entre asesor y postulante.
    /// FR-3 / FR-6: columnas crudas tal cual de las tablas
    /// gp.T_WhatsAppMensajeEnviadoPostulante y gp.T_WhatsAppMensajeRecibidoPostulante.
    /// Tipo: 1 = enviado (asesor), 2 = recibido (postulante).
    /// </summary>
    public class MensajeChatPostulanteDTO
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public string? WaType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaCaption { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdPersonal { get; set; }
        public string? NombrePersonal { get; set; }
        public int? IdPostulante { get; set; }
        public string? WaFrom { get; set; }
        public string? WaTo { get; set; }
        public int IdPais { get; set; }
        public string? WaId { get; set; }
        public string? WaStatus { get; set; }
    }
}
