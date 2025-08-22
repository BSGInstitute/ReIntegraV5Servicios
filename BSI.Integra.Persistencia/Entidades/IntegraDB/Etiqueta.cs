using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Etiqueta : BaseIntegraEntity
    {
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        [StringLength(200)]
        public string? Descripcion { get; set; }
        [StringLength(250)]
        public string? CampoDb { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdNodoPadre { get; set; }
        public int? IdTipoEtiqueta { get; set; }

    }
}
