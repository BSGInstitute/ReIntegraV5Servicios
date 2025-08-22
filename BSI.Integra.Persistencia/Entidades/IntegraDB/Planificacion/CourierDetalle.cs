using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CourierDetalle : BaseIntegraEntity
    {
        public int IdCourier { get; set; }
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int TiempoEnvio { get; set; }

    }
}
