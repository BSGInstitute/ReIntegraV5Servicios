using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class ConfiguracionAsignacionEvaluacion : BaseIntegraEntity
    {
        public int IdProcesoSeleccion { get; set; }
        public int IdEvaluacion { get; set; }
        public int NroOrden { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdProcesoSeleccionEtapa { get; set; }
    }
}
