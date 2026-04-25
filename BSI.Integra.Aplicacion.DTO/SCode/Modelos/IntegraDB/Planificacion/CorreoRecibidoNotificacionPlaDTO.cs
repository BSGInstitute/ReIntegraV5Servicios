using System;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// Autor: Jose Vega
    /// Fecha: 12/03/2026
    /// Version: 1.0
    /// <summary>
    /// DTO para la notificación de un nuevo correo recibido por SignalR para el área de Planificación.
    /// </summary>
    public class CorreoRecibidoNotificacionPlaDTO
    {
        public int IdCorreo { get; set; }
        public string Asunto { get; set; }
        public string Remitente { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int IdAsesor { get; set; }
        public int? IdProveedor { get; set; }
        public string Folder { get; set; }
        public bool Seen { get; set; }
    }
}
