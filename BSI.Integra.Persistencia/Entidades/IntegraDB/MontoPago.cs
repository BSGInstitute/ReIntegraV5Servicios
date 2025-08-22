using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MontoPago : BaseIntegraEntity
    {
        public decimal Precio { get; set; }
        [StringLength(250)]
        public string PrecioLetras { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal? Matricula { get; set; }
        public decimal? Cuotas { get; set; }
        public int? NroCuotas { get; set; }
        public int? IdTipoDescuento { get; set; }
        public int? IdPrograma { get; set; }
        public int? IdTipoPago { get; set; }
        public int? IdPais { get; set; }
        [StringLength(100)]
        public string? Vencimiento { get; set; }
        [StringLength(14)]
        public string? PrimeraCuota { get; set; }
        public bool? CuotaDoble { get; set; }
        [StringLength(200)]
        public string? Descripcion { get; set; }
        public bool? VisibleWeb { get; set; }
        public int? Paquete { get; set; }
        public bool? PorDefecto { get; set; }
        public decimal? MontoDescontado { get; set; }

        public List<MontoPagoPlataforma> MontoPagoPlataforma { get; set; }
        public List<MontoPagoSuscripcion> MontoPagoSuscripcion { get; set; }


    }
}
