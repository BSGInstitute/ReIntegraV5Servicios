using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class Courier : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public int IdCiudad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
}
