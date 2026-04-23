using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// DTOs: FacebookResena
    /// Autor: Max Mantilla.
    /// Fecha: 21/04/2026
    /// <summary>
    /// Objetos de transferencia de datos para el módulo de reseñas de Facebook.
    /// Los nombres de propiedad coinciden 1:1 con las interfaces Angular.
    /// </summary>
    public class FacebookResenaDTO : BaseIntegraEntity
    {
        public int IdFacebookConfiguracion { get; set; }
        [StringLength(100)]
        public string IdentificadorHistoria { get; set; }
        public bool? Recomienda { get; set; }
        public bool? TieneTexto { get; set; }
        public string TextoResena { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool? Mostrar { get; set; }
    }

    // ── Configuración de página ──────────────────────────────────────────────────

    /// <summary>Datos de configuración de una página de Facebook (mkt.T_FacebookConfiguracion).</summary>
    public class FacebookConfiguracionPaginaDTO
    {
        public int Id { get; set; }
        public string IdentificadorPagina { get; set; }
        public string NombrePagina { get; set; }
        public string TokenAccesoPagina { get; set; }
    }

    // ── Filtro de grilla ─────────────────────────────────────────────────────────

    /// <summary>
    /// Filtro para la grilla de reseñas del panel de administración.
    /// Propiedades alineadas con IFacebookResenaFiltro del frontend Angular.
    /// </summary>
    public class FacebookResenaGrillaFiltroDTO
    {
        public List<int> IdsPaginasFacebook { get; set; } = new();
        public bool? EsVisible { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 50;
    }

    // ── Respuesta paginada ───────────────────────────────────────────────────────

    /// <summary>Respuesta paginada de la grilla de reseñas.</summary>
    public class FacebookResenaGrillaPaginadaDTO
    {
        public List<FacebookResenaGrillaItemDTO> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    // ── Item de grilla ───────────────────────────────────────────────────────────

    /// <summary>Fila individual de la grilla de reseñas del panel de administración.</summary>
    public class FacebookResenaGrillaItemDTO
    {
        public int IdFacebookResena { get; set; }
        public int IdFacebookConfiguracion { get; set; }
        public string NombrePagina { get; set; }
        public string IdentificadorHistoria { get; set; }
        public bool Recomienda { get; set; }
        public bool TieneTexto { get; set; }
        public string TextoResena { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool Mostrar { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // ── Estadísticas por página ──────────────────────────────────────────────────

    /// <summary>Estadísticas agregadas de reseñas por página de Facebook.</summary>
    public class FacebookResenaPaginaItemDTO
    {
        public int IdFacebookConfiguracion { get; set; }
        public string IdentificadorPagina { get; set; }
        public string NombrePagina { get; set; }
        public int TotalResenas { get; set; }
        public int TotalMostrar { get; set; }

        public int TotalOpiniones { get; set; }
        public decimal PorcentajeRecomendacion { get; set; }
    }

    // ── Combo de cuentas ─────────────────────────────────────────────────────────

    /// <summary>Item del combo de cuentas de Facebook para el filtro del frontend.</summary>
    public class FacebookResenaCuentaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string IdentificadorPagina { get; set; }
    }

    // ── Acción masiva de visibilidad ─────────────────────────────────────────────

    /// <summary>
    /// Payload para MarcarResenaVisible / MarcarResenaOculta.
    /// Alineado con IMarcarMostrarRequest del frontend Angular.
    /// </summary>
    public class FacebookResenaMarcarMostrarDTO
    {
        public List<int> IdsFacebookResena { get; set; } = new();
        public string Usuario { get; set; }
    }

    // ── Sincronización ───────────────────────────────────────────────────────────

    /// <summary>Resultado completo de una sincronización con la Graph API de Facebook.</summary>
    public class FacebookResenaSincronizacionDTO
    {
        public FacebookResenaSincronizacionResumenDTO Resumen { get; set; } = new();
        public List<FacebookResenaPaginaResultadoDTO> ResultadosPorPagina { get; set; } = new();
    }

    /// <summary>Resumen global de la sincronización (totales y duración).</summary>
    public class FacebookResenaSincronizacionResumenDTO
    {
        public DateTime FechaSincronizacion { get; set; }
        public int TotalPaginasProcesadas { get; set; }
        public int TotalPaginasConError { get; set; }
        public int TotalResenasProcesadas { get; set; }
        public int TotalResenasNuevas { get; set; }
        public int TotalResenasActualizadas { get; set; }
        public int TotalResenasSinCambios { get; set; }
        public int DuracionSegundos { get; set; }
    }

    /// <summary>Resultado de sincronización por página individual.</summary>
    public class FacebookResenaPaginaResultadoDTO
    {
        public string IdentificadorPagina { get; set; }
        public string NombrePagina { get; set; }
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public int TotalDescargadas { get; set; }
        public int TotalResenasProcesadas { get; set; }
        public int ResenasNuevas { get; set; }
        public int ResenasActualizadas { get; set; }
        public int ResenasSinCambios { get; set; }
        public int LimitUtilizado { get; set; }
        public int TotalRequests { get; set; }
        public string EstrategiaUsada { get; set; }

        public int TotalOpiniones { get; set; }
        public int TotalOpinionesFacebook { get; set; }
        public decimal CalificacionGlobal { get; set; }
        public decimal PorcentajeRecomendacion { get; set; }
    }

    // ── DTO interno Graph API ────────────────────────────────────────────────────

    /// <summary>DTO interno para parsear un item de reseña de la Graph API de Facebook.</summary>
    public class FacebookResenaGrafApiDTO
    {
        public string StoryId { get; set; }
        public bool Recomienda { get; set; }
        public bool TieneTexto { get; set; }
        public string TextoResena { get; set; }
        public DateTime FechaResena { get; set; }
    }
}
