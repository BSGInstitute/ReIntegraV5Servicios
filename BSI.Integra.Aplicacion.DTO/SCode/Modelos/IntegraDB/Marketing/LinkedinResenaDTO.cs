using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO base de la entidad LinkedinResena.</summary>
    public class LinkedinResenaDTO : BaseIntegraEntity
    {
        public int IdLinkedinConfiguracion { get; set; }
        public string NombreAutor { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string FotoAutor { get; set; }
        public string UrlPublicacion { get; set; }
        public string TextoResena { get; set; }
        public string Certificacion { get; set; }
        public int? IdPais { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool Mostrar { get; set; }
    }

    /// <summary>Filtro para la grilla de testimonios del panel de administración.</summary>
    public class LinkedinResenaGrillaFiltroDTO
    {
        public bool? EsVisible { get; set; }
        public List<int> IdsPais { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 50;
    }

    /// <summary>Respuesta paginada de la grilla de testimonios de LinkedIn.</summary>
    public class LinkedinResenaGrillaPaginadaDTO
    {
        public List<LinkedinResenaGrillaItemDTO> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    /// <summary>Fila individual de la grilla de testimonios de LinkedIn.</summary>
    public class LinkedinResenaGrillaItemDTO
    {
        public int IdLinkedinResena { get; set; }
        public string NombreAutor { get; set; }
        public string Cargo { get; set; }
        public string Empresa { get; set; }
        public string FotoAutor { get; set; }
        public string UrlPublicacion { get; set; }
        public string TextoResena { get; set; }
        public string Certificacion { get; set; }
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
    public class LinkedinResenaPaisComboDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public string RutaBandera { get; set; }
    }

    /// <summary>Combo de ciudades filtrado por país.</summary>
    public class LinkedinResenaCiudadComboDTO
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    /// <summary>Payload para MarcarResenaVisible / MarcarResenaOculta.</summary>
    public class LinkedinResenaMarcarMostrarDTO
    {
        public List<int> IdsLinkedinResena { get; set; } = new();
        public string Usuario { get; set; }
    }
}
