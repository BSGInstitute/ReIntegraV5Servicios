using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class Reclamo : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public string Descripcion { get; set; } = null!;
        public int IdReclamoEstado { get; set; }
        public int IdOrigen { get; set; }
        public int? IdTipoReclamoAlumno { get; set; }
        public int? NroDiasSolucion { get; set; }
        public int? IdEstadoMatriculaPrevio { get; set; }
        public DateTime? FechaReclamoRealizadoFin { get; set; }
        public int IdCategoriaTicket { get; set; }
    }
}
