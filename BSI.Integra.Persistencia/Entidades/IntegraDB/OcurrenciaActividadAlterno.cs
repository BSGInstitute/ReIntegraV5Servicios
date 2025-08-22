using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OcurrenciaActividadAlterno : BaseIntegraEntity
    {
        public int IdOcurrencia { get; set; }
        public int IdActividadCabecera { get; set; }
        public bool? PreProgramada { get; set; }
        public int? IdOcurrenciaActividadPadre { get; set; }
        public bool NodoPadre { get; set; }
        public int? IdPlantillaSpeech { get; set; }
        public int? IdFaseOportunidad { get; set; }
        public int? IdActividadCabeceraProgramada { get; set; }
        [StringLength(100)]
        public string? Roles { get; set; }
    }
}
