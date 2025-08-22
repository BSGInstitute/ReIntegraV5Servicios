using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PespecificoPadrePespecificoHijo : BaseIntegraEntity
    {

        public int Id { get; set; }
       
        public int PespecificoPadreId { get; set; }
  
        public int PespecificoHijoId { get; set; }
        
        public Guid? IdMigracion { get; set; }
    }
}
