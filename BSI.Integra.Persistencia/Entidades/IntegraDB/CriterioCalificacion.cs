using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CriterioCalificacion : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        [StringLength(10)]
        public string Sigla { get; set; } = null!;
        public bool EstadoDocumento { get; set; }
        public bool DocOriginal { get; set; }
        public bool DocPasarela { get; set; }
        public bool DocPasCancelados { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }
}
