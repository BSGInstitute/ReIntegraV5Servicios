using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class MaterialEnvioDetalle : BaseIntegraEntity
    {
        public int IdMaterialEnvio { get; set; }
        public int IdMaterialVersion { get; set; }
        public int IdMaterialEstadoRecepcion { get; set; }
        public int IdPersonalReceptor { get; set; }
        public int CantidadEnvio { get; set; }
        public int CantidadRecepcion { get; set; }
        public string ComentarioEnvio { get; set; } = null!;
        public string ComentarioRecepcion { get; set; } = null!;
        public int? IdMigracion { get; set; }
    }
}
