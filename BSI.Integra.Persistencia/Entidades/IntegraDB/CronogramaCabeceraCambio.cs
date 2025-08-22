using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CronogramaCabeceraCambio : BaseIntegraEntity
    {
    
        public int IdCronogramaTipoModificacion { get; set; }
     
        public int? SolicitadoPor { get; set; }
  
        public int? AprobadoPor { get; set; }
     
        public bool Aprobado { get; set; }
   
        public bool Cancelado { get; set; }
       
        public string? Observacion { get; set; }
        
        public Guid? IdMigracion { get; set; }
    }
}
