using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class DiaSemana : BaseIntegraEntity
    {
     
        public string Nombre { get; set; } = null!;
      
        public int OrdenInicio0 { get; set; }
     
        public Guid? IdMigracion { get; set; }
    }
}
