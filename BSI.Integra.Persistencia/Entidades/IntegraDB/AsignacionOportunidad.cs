using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionOportunidad : BaseIntegraEntity
    {
        public int? IdOportunidad { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public ICollection<AsignacionOportunidadLog> AsignacionOportunidadLogs { get; set; } = new List<AsignacionOportunidadLog>();
    }
}
