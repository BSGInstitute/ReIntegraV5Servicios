using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class CampaniaMailingDetalle : BaseIntegraEntity
    {
        public int IdCampaniaMailing { get; set; }
        public int Prioridad { get; set; }
        public int Tipo { get; set; }
        public int IdRemitenteMailing { get; set; }
        public int IdPersonal { get; set; }
        [StringLength(300)]
        public string Subject { get; set; } = null!;
        public DateTime FechaEnvio { get; set; }
        public int IdHoraEnvio { get; set; }
        public int Proveedor { get; set; }
        public int EstadoEnvio { get; set; }
        public int? IdFiltroSegmento { get; set; }
        public int IdPlantilla { get; set; }
        public int? IdConjuntoAnuncio { get; set; }
        [StringLength(300)]
        public string? Campania { get; set; }
        [StringLength(300)]
        public string? CodMailing { get; set; }
        public int? CantidadContactos { get; set; }
        public int? IdConjuntoListaDetalle { get; set; }
        public int? IdCentroCosto { get; set; }
        public bool? EsSubidaManual { get; set; }
    }
}
