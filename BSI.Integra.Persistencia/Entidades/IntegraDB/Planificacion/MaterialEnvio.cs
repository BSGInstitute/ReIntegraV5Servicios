using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialEnvio : BaseIntegraEntity
    {
        public int IdSedeTrabajo { get; set; }
        public int IdPersonalRemitente { get; set; }
        public int IdProveedorEnvio { get; set; }
        public DateTime FechaEnvio { get; set; }
        public int? IdMigracion { get; set; }
        public ICollection<MaterialEnvioDetalle> MaterialEnvioDetalles { get; set; }
    }
}
