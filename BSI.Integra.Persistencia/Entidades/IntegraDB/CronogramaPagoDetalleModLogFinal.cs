using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaPagoDetalleModLogFinal : BaseIntegraEntity
    {
        public int? IdMatriculaCabecera { get; set; }
     
        public DateTime Fecha { get; set; }
       
        public int? NroCuota { get; set; }
      
        public int? NroSubCuota { get; set; }
     
        public DateTime? FechaVencimiento { get; set; }
      
        public decimal? TotalPagar { get; set; }
       
        public decimal? Cuota { get; set; }
     
        public decimal? Mora { get; set; }
  
        public decimal? MontoPagado { get; set; }
     
        public decimal? Saldo { get; set; }
        
        public bool? Cancelado { get; set; }
      
        public string? TipoCuota { get; set; }
      
        public string? Moneda { get; set; }
     
        public DateTime? FechaPago { get; set; }
     
        public int? IdFormaPago { get; set; }
     
        public DateTime? FechaPagoBanco { get; set; }
     
        public bool? Ultimo { get; set; }
   
        public string? Observaciones { get; set; }
     
        public int? IdDocumentoPago { get; set; }
       
        public string? NroDocumento { get; set; }
   
        public string? MonedaPago { get; set; }
      
        public decimal? TipoCambio { get; set; }
       
        public string? MensajeSistema { get; set; }
      
        public DateTime? FechaProcesoPago { get; set; }
      
        public string? EstadoPrimerLog { get; set; }
      
        public int? Version { get; set; }
      
        public bool? Aprobado { get; set; }
      
        public bool? Estado2 { get; set; }
       
        public Guid? IdMigracion { get; set; }
    }
}
