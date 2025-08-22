using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PostulanteProcesoSeleccion : BaseIntegraEntity
    {
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdEstadoProcesoSeleccion { get; set; }
        public int? IdPostulanteNivelPotencial { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPersonalOperadorProceso { get; set; }
        public int? IdConvocatoriaPersonal { get; set; }
    }
}
