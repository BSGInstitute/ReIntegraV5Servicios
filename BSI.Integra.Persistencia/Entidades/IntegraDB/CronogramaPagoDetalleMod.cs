using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaPagoDetalleMod : BaseIntegraEntity
    {
       
        public int? IdMatriculaCabecera { get; set; }
      
        public int? NroCuota { get; set; }
      
        public int? NroSubCuota { get; set; }
     
        public DateTime? FechaVencimiento { get; set; }
      
        public decimal? TotalPagar { get; set; }
     
        public decimal? Cuota { get; set; }
    
        public decimal Mora { get; set; }
      
        public decimal MontoPagado { get; set; }
        
        public decimal? Saldo { get; set; }
      
        public bool Cancelado { get; set; }
   
        public string? TipoCuota { get; set; }
       
        public string? Moneda { get; set; }
      
        public DateTime? FechaPago { get; set; }
       
        public int? IdFormaPago { get; set; }
        
        public int? IdCuentaCorriente { get; set; }
     
        public DateTime? FechaPagoBanco { get; set; }
     
        public bool Enviado { get; set; }
       
        public string? Observaciones { get; set; }
        
        public int? IdDocumentoPago { get; set; }
    
        public string? NroDocumento { get; set; }
 
        public string? MonedaPago { get; set; }
        
        public decimal? TipoCambio { get; set; }
        
        public DateTime? FechaProcesoPago { get; set; }
     
  
        public int? IdMigracion { get; set; }

    }
}

