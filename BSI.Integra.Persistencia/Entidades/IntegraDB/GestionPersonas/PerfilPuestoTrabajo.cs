using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PerfilPuestoTrabajo : BaseIntegraEntity
    {
        public int IdPuestoTrabajo { get; set; }
		public string Descripcion { get; set; }
		public string Objetivo { get; set; }
		public int Version { get; set; }
		public bool? EsActual { get; set; }
		public int? IdMigracion { get; set; }
		public int? IdPersonalSolicitud { get; set; }
		public DateTime? FechaSolicitud { get; set; }
		public int? IdPersonalAprobacion { get; set; }
		public DateTime? FechaAprobacion { get; set; }
		public string Observacion { get; set; }
		public int? IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }
    }
}
