using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO base de la entidad GlassdoorResena.</summary>
    public class GlassdoorResenaDTO : BaseIntegraEntity
    {
        public int IdGlassdoorConfiguracion { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int Valoracion { get; set; }
        public string Cargo { get; set; }
        public string TipoEmpleado { get; set; }
        public string Ventaja { get; set; }
        public string Desventaja { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool Mostrar { get; set; }
    }

    /// <summary>Filtro para la grilla de reseñas de Glassdoor en el panel de administración.</summary>
    public class GlassdoorResenaGrillaFiltroDTO
    {
        public bool? Mostrar { get; set; }
        public List<int> IdPaisLista { get; set; }
        public string TipoEmpleado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 50;
    }

    /// <summary>Respuesta paginada de la grilla de reseñas de Glassdoor.</summary>
    public class GlassdoorResenaGrillaPaginadaDTO
    {
        public List<GlassdoorResenaGrillaItemDTO> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    /// <summary>Fila individual de la grilla de reseñas de Glassdoor.</summary>
    public class GlassdoorResenaGrillaItemDTO
    {
        public int IdGlassdoorResena { get; set; }
        public int IdGlassdoorConfiguracion { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int Valoracion { get; set; }
        public string Cargo { get; set; }
        public string TipoEmpleado { get; set; }
        public string Ventaja { get; set; }
        public string Desventaja { get; set; }
        public int? IdPais { get; set; }
        public string NombrePais { get; set; }
        public string RutaBandera { get; set; }
        public int? IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool Mostrar { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    /// <summary>Combo de países para filtros del frontend.</summary>
    public class GlassdoorResenaPaisComboDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public string RutaBandera { get; set; }
    }

    /// <summary>Combo de ciudades filtrado por país.</summary>
    public class GlassdoorResenaCiudadComboDTO
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    /// <summary>Payload para MarcarResenaVisible / MarcarResenaOculta.</summary>
    public class GlassdoorResenaMarcarMostrarDTO
    {
        public List<int> IdsGlassdoorResena { get; set; } = new();
        public string? Usuario { get; set; }
    }
}
