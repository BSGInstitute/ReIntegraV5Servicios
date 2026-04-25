using BSI.Integra.Aplicacion.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ComputrabajoResena : BaseIntegraEntity
    {
        public int IdComputrabajoConfiguracion { get; set; }

        public string? Contenido { get; set; }

        public int Valoracion { get; set; }

        [StringLength(200)]
        public string? Cargo { get; set; }

        [StringLength(50)]
        public string? TipoEmpleado { get; set; }

        public string? Ventaja { get; set; }

        public string? Desventaja { get; set; }

        public int? IdCiudad { get; set; }

        public DateTime? FechaResena { get; set; }

        public bool Mostrar { get; set; }
    }
}
