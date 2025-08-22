using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AutenticacionServicioExterno : BaseIntegraEntity
    {
      
        public int Id { get; set; }
       
        public string Nombre { get; set; } = null!;
       
        public string Descripcion { get; set; } = null!;
        
        public string Valor { get; set; } = null!;
       
      
    }
}
