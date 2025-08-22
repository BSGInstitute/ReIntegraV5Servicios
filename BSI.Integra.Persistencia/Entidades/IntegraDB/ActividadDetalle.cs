using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ActividadDetalle : BaseIntegraEntity
    {
        public int? IdActividadCabecera { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaReal { get; set; }
        public int? DuracionReal { get; set; }
        public int? IdOcurrencia { get; set; }
        public int IdEstadoActividadDetalle { get; set; }
        [StringLength(500)]
        public string? Comentario { get; set; }
        public int? IdAlumno { get; set; }
        [StringLength(1)]
        public string? Actor { get; set; }
        public int? IdOportunidad { get; set; }
        public int? IdCentralLlamada { get; set; }
        [StringLength(250)]
        public string? RefLlamada { get; set; }
        public int? IdOcurrenciaActividad { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public DateTime? FechaOcultarWhatsapp { get; set; }
        public int? IdOcurrenciaAlterno { get; set; }
        public int? IdOcurrenciaActividadAlterno { get; set; }
    }
}
