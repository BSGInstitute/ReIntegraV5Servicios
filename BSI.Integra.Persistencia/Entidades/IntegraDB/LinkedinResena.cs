using BSI.Integra.Aplicacion.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LinkedinResena : BaseIntegraEntity
    {
        public int IdLinkedinConfiguracion { get; set; }

        [StringLength(200)]
        public string NombreAutor { get; set; } = null!;

        [StringLength(200)]
        public string? Cargo { get; set; }

        [StringLength(200)]
        public string? Empresa { get; set; }

        [StringLength(500)]
        public string? FotoAutor { get; set; }

        [StringLength(500)]
        public string? UrlPublicacion { get; set; }

        public string? TextoResena { get; set; }

        [StringLength(300)]
        public string? Certificacion { get; set; }

        public int? IdPais { get; set; }

        public int? IdCiudad { get; set; }

        public DateTime? FechaResena { get; set; }

        public bool Mostrar { get; set; }
    }
}
