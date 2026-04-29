using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookConfiguracion : BaseIntegraEntity
    {
        [StringLength(100)]
        public string IdentificadorPagina { get; set; } = null!;

        [StringLength(200)]
        public string Nombre { get; set; } = null!;

        [StringLength(500)]
        public string? TokenAccesoPagina { get; set; }

        public int ResenaTotal { get; set; }

        public decimal Valoracion { get; set; }

        public bool Mostrar { get; set; }
    }
}
