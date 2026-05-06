using BSI.Integra.Aplicacion.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ComputrabajoConfiguracion : BaseIntegraEntity
    {
        [StringLength(200)]
        public string NombreEmpresa { get; set; } = null!;

        public decimal Valoracion { get; set; }

        public int ResenaTotal { get; set; }

        [StringLength(500)]
        public string? UrlPerfil { get; set; }

        public DateTime? FechaSincronizacion { get; set; }
    }
}
