using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class GooglePlacesConfiguracion : BaseIntegraEntity
    {
        [StringLength(150)]
        public string NombreSede { get; set; } = null!;

        [StringLength(100)]
        public string IdentificadorCuenta { get; set; } = null!;

        public decimal Valoracion { get; set; }

        public int ResenaTotal { get; set; }

        public bool Mostrar { get; set; }
    }
}
