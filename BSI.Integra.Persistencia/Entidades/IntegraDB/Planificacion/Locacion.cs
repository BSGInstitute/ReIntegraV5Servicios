using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class Locacion : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public int IdRegion { get; set; }
        public int IdCiudad { get; set; }
        [StringLength(300)]
        public string Direccion { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
