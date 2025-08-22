using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PagoFinal : BaseIntegraEntity
    {
  
        public int IdMatriculaCabecera { get; set; }
     
        public decimal Monto { get; set; }
       
        public string Moneda { get; set; } = null!;
      
        public double? TipoCambio { get; set; }
    
        public string? Concepto { get; set; }
    
        public string? Ruc { get; set; }
        
        public int? IdFormaPago { get; set; }
      
        public string SerieNumero { get; set; } = null!;
  
        public int? IdCuentaCorriente { get; set; }

        public string? NroRefCheque { get; set; }
    
        public DateTime FechaDocumento { get; set; }
      
        public string? NroDeposito { get; set; }
    
        public DateTime FechaPago { get; set; }
    
        public bool EstadoPago { get; set; }
      
        public int? IdDocumentoPago { get; set; }
       
    }
}
