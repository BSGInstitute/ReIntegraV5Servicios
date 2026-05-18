using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas
{
    /// <summary>
    /// DTO de cabecera + hilo cronologico de un postulante.
    /// FR-3: el SP devuelve filas planas; el repo arma este DTO agrupando los mensajes
    /// y tomando IdPostulante, NombreCompleto y WaNumero de la primera fila.
    /// </summary>
    public class HistorialChatPostulanteDTO
    {
        public int? IdPostulante { get; set; }
        public string? NombreCompleto { get; set; }
        public string WaNumero { get; set; } = string.Empty;
        public List<MensajeChatPostulanteDTO> Mensajes { get; set; } = new List<MensajeChatPostulanteDTO>();
    }
}
