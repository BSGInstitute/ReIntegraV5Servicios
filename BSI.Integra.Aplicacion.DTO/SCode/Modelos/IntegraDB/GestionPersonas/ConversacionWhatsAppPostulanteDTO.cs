using System;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    /// <summary>
    /// DTO de ultima conversacion del asesor con un postulante.
    /// FR-2: ultimo mensaje por postulante asignado al asesor (enviado o recibido).
    /// Tipo: 1 = enviado por asesor, 2 = recibido del postulante.
    /// OrigenEnviado se mantiene como bool conveniente para el front (equivalente a Tipo == 1).
    /// </summary>
    public class ConversacionWhatsAppPostulanteDTO
    {
        public int? IdPostulante { get; set; }
        public string? NombreCompleto { get; set; }
        public string WaNumero { get; set; } = string.Empty;
        public string UltimoMensaje { get; set; } = string.Empty;
        public DateTime FechaUltimoMensaje { get; set; }
        public int IdPais { get; set; }
        public int Tipo { get; set; }
        public bool OrigenEnviado { get; set; }
    }
}
