using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProveedorTipoServicio : BaseIntegraEntity
    {

        public int IdProveedor { get; set; }
        public int IdTipoServicio { get; set; }
        public int IdMigracion { get; set; }
        //public virtual Proveedor Proveedor { get; set; }
        public virtual TipoServicio TipoServicio { get; set; }

    }
}
