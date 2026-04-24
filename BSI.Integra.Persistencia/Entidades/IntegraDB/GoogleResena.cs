using BSI.Integra.Aplicacion.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GoogleResena : BaseIntegraEntity
    {
        public int IdGooglePlacesConfiguracion { get; set; }

        [StringLength(255)]
        public string? IdentificadorResena { get; set; }

        [StringLength(200)]
        public string NombreAutor { get; set; } = null!;

        [StringLength(500)]
        public string? FotoAutor { get; set; }

        [StringLength(500)]
        public string? UriAutor { get; set; }

        public int Valoracion { get; set; }

        public string? TextoResena { get; set; }

        [StringLength(10)]
        public string? IdiomaResena { get; set; }

        public DateTime? FechaResena { get; set; }

        [StringLength(100)]
        public string? DescripcionTiempoRelativo { get; set; }

        [StringLength(500)]
        public string? UriGoogleMaps { get; set; }

        public bool Mostrar { get; set; }
    }
}
