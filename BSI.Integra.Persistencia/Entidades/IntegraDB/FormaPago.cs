using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FormaPago : BaseIntegraEntity
    {
       
        public string Descripcion { get; set; } = null!;
       
      
        public int? IdMigracion { get; set; }
    }
}
