using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class FacebookCuentaPublicitarium : BaseIntegraEntity
    {
     
        public string FacebookIdNegocio { get; set; } = null!;
     
        public string FacebookIdCuentaPublicitaria { get; set; } = null!;
     
        public string Nombre { get; set; } = null!;
       
        public Guid? IdMigracion { get; set; }
    }
}
