using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class Ambiente : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; }
        public int IdLocacion { get; set; }
        public int IdTipoAmbiente { get; set; }
        public int Capacidad { get; set; }
        public bool Virtual { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
