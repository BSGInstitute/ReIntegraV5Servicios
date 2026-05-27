using System;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    /// <summary>
    /// DTO de postulante con mensaje pendiente de respuesta para el asesor GP.
    /// FR-1 / FR-5: aparece cuando MAX(FechaCreacion) de recibidos del asesor
    /// > COALESCE(MAX(FechaCreacion) enviados, '1900-01-01') para ese WaFrom.
    /// IdPostulante puede ser NULL (mensaje huerfano sin postulante creado todavia).
    /// </summary>
    public class PendienteWhatsAppPostulanteDTO
    {
        public int? IdPostulante { get; set; }
        public string? NombreCompleto { get; set; }
        public string WaNumero { get; set; } = string.Empty;
        public string UltimoMensaje { get; set; } = string.Empty;
        public DateTime FechaUltimoMensaje { get; set; }
        public int IdPais { get; set; }
    }
}
