using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaPagoDetalle : BaseIntegraEntity
    {
       
     
        public int? IdMatriculaCabecera { get; set; }
       
        public int? NroCuota { get; set; }
        
        public DateTime? FechaVencimiento { get; set; }
  
        public decimal? TotalPagar { get; set; }
     
        public decimal? Cuota { get; set; }
      
        public decimal? Saldo { get; set; }
       
        public bool? Cancelado { get; set; }
    
        public string? TipoCuota { get; set; }
    
        public string? Moneda { get; set; }
   
       
        public int? IdMigracion { get; set; }
    }
}
