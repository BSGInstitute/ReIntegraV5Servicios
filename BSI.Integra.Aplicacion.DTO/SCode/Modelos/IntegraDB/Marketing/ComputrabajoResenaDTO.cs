using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>DTO base de la entidad ComputrabajoResena.</summary>
    public class ComputrabajoResenaDTO : BaseIntegraEntity
    {
        public int IdComputrabajoConfiguracion { get; set; }
        public string Contenido { get; set; }
        public int Valoracion { get; set; }
        public string Cargo { get; set; }
        public string TipoEmpleado { get; set; }
        public string Ventaja { get; set; }
        public string Desventaja { get; set; }
        public int? IdCiudad { get; set; }
        public DateTime? FechaResena { get; set; }
        public bool Mostrar { get; set; }
    }

    /// <summary>Filtro para la grilla de reseñas de Computrabajo en el panel de administración.</summary>
    public class ComputrabajoResenaGrillaFiltroDTO
    {
        public bool? Mostrar { get; set; }
        public List<int> IdPaisLista { get; set; }
        public string TipoEmpleado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Pagina { get; set; } = 1;
        public int TamanoPagina { get; set; } = 50;
    }

    /// <summary>Respuesta paginada de la grilla de reseñas de Computrabajo.</summary>
    public class ComputrabajoResenaGrillaPaginadaDTO
    {
        public List<ComputrabajoResenaGrillaItemDTO> Data { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int Pagina { get; set; }
        public int TamanoPagina { get; set; }
        public int TotalPaginas { get; set; }
    }

    /// <summary>Fila individual de la grilla de reseñas de Computrabajo.</summary>
    public class ComputrabajoResenaGrillaItemDTO
    {
        public int IdComputrabajoResena { get; set; }
        public int IdComputrabajoConfiguracion { get; set; }
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
    public class ComputrabajoResenaPaisComboDTO
    {
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public string RutaBandera { get; set; }
    }

    /// <summary>Combo de ciudades filtrado por país.</summary>
    public class ComputrabajoResenaCiudadComboDTO
    {
        public int IdCiudad { get; set; }
        public string NombreCiudad { get; set; }
    }

    /// <summary>Payload para MarcarResenaVisible / MarcarResenaOculta.</summary>
    public class ComputrabajoResenaMarcarMostrarDTO
    {
        public List<int> IdsComputrabajoResena { get; set; } = new();
        public string? Usuario { get; set; }
    }
}
