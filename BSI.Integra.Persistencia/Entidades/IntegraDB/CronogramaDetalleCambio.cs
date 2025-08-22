using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaDetalleCambio : BaseIntegraEntity
    {
       
        public int? IdMatriculaCabecera { get; set; }
       
        public int IdCronogramaCabeceraCambio { get; set; }
      
        public int NroCuota { get; set; }
     
        public int NroSubcuota { get; set; }
        
        public DateTime FechaVencimiento { get; set; }
      
        public decimal Cuota { get; set; }
     
        public decimal Mora { get; set; }
   
        public decimal TipoCambio { get; set; }

        public string Moneda { get; set; } = null!;
     
        public int Version { get; set; }
     
        public Guid? IdMigracion { get; set; }
    }
}
