using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// DTO: LlamadasWhatsApp
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// DTOs para el endpoint de historial de llamadas de WhatsApp Business Calling.
    /// Filtro de entrada y resultado paginado.
    /// </summary>

    public class LlamadasHistorialFiltroDTO
    {
        public int? IdPais { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int? IdPersonal { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public int? TipoLlamada { get; set; }
        public int? EstadoLlamada { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public int Pagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 50;
    }

    public class LlamadasHistorialResultadoDTO
    {
        public List<WhatsAppLlamadaResumenDTO> Llamadas { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
    }

    public class WhatsAppLlamadaResumenDTO
    {
        public int IdWhatsappLlamada { get; set; }
        public string? WaId { get; set; }
        public string? NumeroWhatsApp { get; set; }
        public int TipoLlamada { get; set; }
        public int EstadoLlamada { get; set; }
        public string? MotivoFin { get; set; }
        public DateTime? FechaRinging { get; set; }
        public DateTime? FechaConexion { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? DuracionSegundos { get; set; }
        public string? GrabacionUrl { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdPais { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
        public int TotalRegistros { get; set; }
    }
}
