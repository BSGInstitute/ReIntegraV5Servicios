using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class OrigenPrograma : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Descripcion { get; set; }
        public short? IdMigracion { get; set; }
    }
}
