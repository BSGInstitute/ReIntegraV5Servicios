using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MontoPagoCronograma : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        public int? IdMontoPago { get; set; }
        public int? IdPersonal { get; set; }
        public double Precio { get; set; }
        public double PrecioDescuento { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool EsAprobado { get; set; }
        [StringLength(100)]
        public string NombrePlural { get; set; } = null!;
        public int Formula { get; set; }
        public int MatriculaEnProceso { get; set; }
        [StringLength(100)]
        public string? CodigoMatricula { get; set; }
    }
}
