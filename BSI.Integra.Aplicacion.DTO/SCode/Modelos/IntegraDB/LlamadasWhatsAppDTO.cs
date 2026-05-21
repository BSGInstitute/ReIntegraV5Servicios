using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

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

    /// <summary>
    /// Filas crudas devueltas por el repo para el lookup del último consentimiento.
    /// El service aplica la lógica de vigencia sobre estos campos.
    /// </summary>
    public class WhatsAppConsentimientoRawDTO
    {
        public int       IdWhatsappLlamada    { get; set; }
        public string?   WaId                 { get; set; }
        public string?   ConsentimientoEstado { get; set; }
        public DateTime? ConsentimientoFecha  { get; set; }
        public DateTime? ConsentimientoExpira { get; set; }
    }

    /// <summary>
    /// Respuesta del endpoint GET /api/LlamadasWhatsApp/EstadoConsentimiento.
    /// El frontend la usa para decidir si mostrar "Solicitar llamada", "Esperando respuesta",
    /// "Llamar ahora" (consentimiento vigente) o "Rechazó".
    /// </summary>
    public class LlamadaConsentimientoEstadoDTO
    {
        /// "aceptado" | "pendiente" | "rechazado" | "sin_solicitud"
        public string    Estado               { get; set; } = "sin_solicitud";
        public int?      IdWhatsappLlamada    { get; set; }
        public DateTime? ConsentimientoFecha  { get; set; }
        public DateTime? ConsentimientoExpira { get; set; }
        public bool      PuedeSolicitar       { get; set; }
        public bool      PuedeLlamar          { get; set; }
        public string?   Mensaje              { get; set; }
    }

    /// <summary>
    /// Request del frontend para subir la grabación de una llamada (entrante o saliente)
    /// generada con MediaRecorder del browser. El backend la persiste en Azure Blob y actualiza
    /// las columnas GrabacionUrl + GrabacionBlobNombre de com.T_WhatsappLlamada.
    /// </summary>
    public class SubirGrabacionLlamadaDTO
    {
        public int       IdWhatsappLlamada { get; set; }
        public IFormFile File              { get; set; } = null!;
    }

    /// <summary>Respuesta del endpoint SubirGrabacion — URL pública del blob + nombre persistido.</summary>
    public class SubirGrabacionLlamadaResultadoDTO
    {
        public bool   Ok          { get; set; }
        public string Url         { get; set; } = string.Empty;
        public string BlobNombre  { get; set; } = string.Empty;
        public string? Mensaje    { get; set; }
    }
}
