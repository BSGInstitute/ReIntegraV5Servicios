using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO base de la entidad GoogleResena.</summary>
    public class GoogleResenaDTO : BaseIntegraEntity
    {
        public int IdGooglePlacesConfiguracion { get; set; }
        [StringLength(255)]
        public string IdentificadorResena { get; set; }
        [StringLength(200)]
        public string NombreAutor { get; set; }
        public string FotoAutor { get; set; }
        public string UriAutor { get; set; }
        public int Valoracion { get; set; }
        public string TextoResena { get; set; }
        public string IdiomaResena { get; set; }
        public DateTime? FechaResena { get; set; }
        public string DescripcionTiempoRelativo { get; set; }
        public string UriGoogleMaps { get; set; }
        public bool Mostrar { get; set; }
    }

    /// <summary>Filtro para la grilla de reseñas del panel de administración.</summary>
    public class GoogleResenaGrillaFiltroDTO
    {
        public List<int> IdsSede { get; set; } = new();
        public bool? EsVisible { get; set; }
        public int? Valoracion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 50;
    }

    /// <summary>Respuesta paginada de la grilla de reseñas de Google.</summary>
    public class GoogleResenaGrillaPaginadaDTO
    {
        public List<GoogleResenaGrillaItemDTO> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    /// <summary>Fila individual de la grilla de reseñas de Google.</summary>
    public class GoogleResenaGrillaItemDTO
    {
        public int IdGoogleResena { get; set; }
        public int IdGooglePlacesConfiguracion { get; set; }
        public string NombreSede { get; set; }
        public string NombreAutor { get; set; }
        public string FotoAutor { get; set; }
        public int Valoracion { get; set; }
        public string TextoResena { get; set; }
        public DateTime? FechaResena { get; set; }
        public string DescripcionTiempoRelativo { get; set; }
        public bool Mostrar { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    /// <summary>Estadísticas agregadas por sede de Google.</summary>
    public class GoogleResenaSedeItemDTO
    {
        public int IdGooglePlacesConfiguracion { get; set; }
        public string IdentificadorCuenta { get; set; }
        public string NombreSede { get; set; }
        public int TotalResenas { get; set; }
        public int TotalMostrar { get; set; }
        public decimal PromedioValoracion { get; set; }
    }

    /// <summary>Item del combo de sedes de Google para el filtro del frontend.</summary>
    public class GoogleResenaSedeComboDTO
    {
        public int IdGooglePlacesConfiguracion { get; set; }
        public string NombreSede { get; set; }
    }

    /// <summary>
    /// Payload para MarcarResenaVisible / MarcarResenaOculta.
    /// </summary>
    public class GoogleResenaMarcarMostrarDTO
    {
        public List<int> IdsGoogleResena { get; set; } = new();
        public string Usuario { get; set; }
    }

    /// <summary>Resultado completo de una sincronización con Google Places API.</summary>
    public class GoogleResenaSincronizacionDTO
    {
        public GoogleResenaSincronizacionResumenDTO Resumen { get; set; } = new();
        public List<GoogleResenaSedeResultadoDTO> ResultadosPorSede { get; set; } = new();
    }

    /// <summary>Resumen global de la sincronización (totales y duración).</summary>
    public class GoogleResenaSincronizacionResumenDTO
    {
        public DateTime FechaSincronizacion { get; set; }
        public int TotalSedesProcesadas { get; set; }
        public int TotalSedesConError { get; set; }
        public int TotalResenasProcesadas { get; set; }
        public int TotalResenasNuevas { get; set; }
        public int TotalResenasActualizadas { get; set; }
        public int TotalResenasSinCambios { get; set; }
        public int DuracionSegundos { get; set; }
    }

    /// <summary>Resultado de sincronización por sede individual.</summary>
    public class GoogleResenaSedeResultadoDTO
    {
        public int IdGooglePlacesConfiguracion { get; set; }
        public string IdentificadorCuenta { get; set; }
        public string NombreSede { get; set; }
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public int TotalDescargadas { get; set; }
        public int ResenasNuevas { get; set; }
        public int ResenasActualizadas { get; set; }
        public int ResenasSinCambios { get; set; }
        public decimal PromedioValoracion { get; set; }
        public int TotalOpinionesGoogle { get; set; }
    }

    /// <summary>DTO interno para parsear una reseña de la Google Places API.</summary>
    public class GoogleResenaPlacesApiDTO
    {
        public string IdentificadorResena { get; set; }
        public string NombreAutor { get; set; }
        public string FotoAutor { get; set; }
        public string UriAutor { get; set; }
        public int Valoracion { get; set; }
        public string TextoResena { get; set; }
        public string IdiomaResena { get; set; }
        public DateTime FechaResena { get; set; }
        public string DescripcionTiempoRelativo { get; set; }
        public string UriGoogleMaps { get; set; }
    }

    /// <summary>Respuesta pública consolidada para el frontend/homepage.</summary>
    public class GoogleResenaPublicoDTO
    {
        public decimal PromedioGeneral { get; set; }
        public int TotalReviews { get; set; }
        public List<GoogleResenaPublicoItemDTO> Reviews { get; set; } = new();
    }

    /// <summary>Item individual de reseña para mostrar en el homepage.</summary>
    public class GoogleResenaPublicoItemDTO
    {
        public string NombreAutor { get; set; }
        public string FotoAutor { get; set; }
        public int Valoracion { get; set; }
        public string TextoResena { get; set; }
        public DateTime? FechaResena { get; set; }
        public string DescripcionTiempoRelativo { get; set; }
        public string NombreSede { get; set; }
        public string UriGoogleMaps { get; set; }
    }
}
