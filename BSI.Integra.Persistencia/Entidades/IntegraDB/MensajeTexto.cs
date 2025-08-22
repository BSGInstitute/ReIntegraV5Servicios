using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MensajeTexto : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        [StringLength(50)]
        public string? IdMatriculaCabecera { get; set; }
        [StringLength(512)]
        public string Mensaje { get; set; } = null!;
        [StringLength(20)]
        public string Numero { get; set; } = null!;
        public int CodigoPais { get; set; }
        [StringLength(40)]
        public string IdSeguimientoTwilio { get; set; } = null!;
    }
}
