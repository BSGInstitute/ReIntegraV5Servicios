using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ActividadMarcadorLog : BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdActividadDetalle { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public int? TotalIntento { get; set; }
        public int? Contestado { get; set; }
        public int? NoContestado { get; set; }
        public int? IdAgendaTab { get; set; }
        public int? IdMigracion { get; set; }
    }
}
