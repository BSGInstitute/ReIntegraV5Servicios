using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CabeceraConfiguracionLlamadaAutomatica : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public int IdIvrPlantilla { get; set; }
        public int IdIvrTipoConfiguracion { get; set; }
        public int? IdPespecifico { get; set; }
        public int IdIvrEjecucion { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        [StringLength(50)]
        public string EstadoProceso { get; set; } = null!;
        public string CongelamientoConfiguracion { get; set; } = null!;
    }
}
