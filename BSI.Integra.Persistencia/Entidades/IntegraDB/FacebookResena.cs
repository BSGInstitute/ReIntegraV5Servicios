using BSI.Integra.Aplicacion.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookResena : BaseIntegraEntity
    {
        public int IdFacebookConfiguracion { get; set; }

        [StringLength(100)]
        public string? IdentificadorHistoria { get; set; }

        public bool? Recomienda { get; set; }

        public bool? TieneTexto { get; set; }

        public string? TextoResena { get; set; }

        public DateTime? FechaResena { get; set; }

        public bool? Mostrar { get; set; }
    }
}
