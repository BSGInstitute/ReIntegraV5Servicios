using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProveedorCalificacion : BaseIntegraEntity
    {
        public int IdProveedor { get; set; }
        public int IdProveedorSubCriterioCalificacion { get; set; }
    }
}
