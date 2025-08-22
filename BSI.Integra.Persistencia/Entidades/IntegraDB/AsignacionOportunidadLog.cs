using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AsignacionOportunidadLog : BaseIntegraEntity
    {
        public int? IdAsignacionOportunidad { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdPersonalAnterior { get; set; }
        public int? IdPersonal { get; set; }
        public int? IdCentroCostoAnt { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdAlumno { get; set; }
        public DateTime FechaLog { get; set; }
        public int? IdTipoDato { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdClasificacionPersona { get; set; }
    }
}
