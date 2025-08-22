using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AnuncioFacebookMetrica : BaseIntegraEntity
    {
        
        public int? IdAnuncioFacebook { get; set; }
      
        public decimal? Gasto { get; set; }
   
        public int? IdMoneda { get; set; }
   
        public int? Impresiones { get; set; }
  
        public int? CantidadClicsUnicos { get; set; }
      
        public int? CantidadClics { get; set; }
       
        public int? Alcance { get; set; }
   
       
        public DateTime? FechaConsulta { get; set; }
     
       
        public int? IdMigracion { get; set; }
        
        public int? CantidadClicsEnlace { get; set; }

        public virtual TAnuncioFacebook? IdAnuncioFacebookNavigation { get; set; }
        public virtual TMonedum? IdMonedaNavigation { get; set; }
    }
}
