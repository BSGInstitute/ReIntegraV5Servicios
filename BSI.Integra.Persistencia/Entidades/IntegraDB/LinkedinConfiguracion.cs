using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LinkedinConfiguracion : BaseIntegraEntity
    {
        [StringLength(200)]
        public string Nombre { get; set; } = null!;

        [StringLength(500)]
        public string? EnlacePagina { get; set; }

        public int ResenaTotal { get; set; }
    }
}
