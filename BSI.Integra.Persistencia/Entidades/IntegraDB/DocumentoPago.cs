using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DocumentoPago : BaseIntegraEntity
    {
       
        public string Nombre { get; set; } = null!;
       
        public int? IdMigracion { get; set; }
    }
}
