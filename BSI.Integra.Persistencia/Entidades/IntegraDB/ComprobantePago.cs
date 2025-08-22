using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ComprobantePago : BaseIntegraEntity
    {
        public int IdSunatDocumento { get; set; }
        public int IdPais { get; set; }
        public string SerieComprobante { get; set; } = null!;
        public string NumeroComprobante { get; set; } = null!;
        public int IdMoneda { get; set; }
        public decimal MontoBruto { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdProveedor { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal AjusteMontoBruto { get; set; }
        public int? IdComprobantePagoEstado { get; set; }
        public DateTime? FechaVencimientoReprogramacion { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? MontoIgv { get; set; }
        public decimal? PorcentajeIgv { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCiudad { get; set; }
    }
}
