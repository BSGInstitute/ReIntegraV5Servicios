using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class LlamadaWebphoneAsterisk : BaseIntegraEntity
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        [StringLength(30)]
        public string Anexo { get; set; }
        [StringLength(42)]
        public string TelefonoDestino { get; set; }
        public int IdActividadDetalle { get; set; }
        public int? IdLlamadaWebphoneTipo { get; set; }
        public int CdrId { get; set; }
        public int DuracionTimbrado { get; set; }
        public int DuracionContesto { get; set; }
        [StringLength(100)]
        public string NombreGrabacion { get; set; }
        public int? IdProveedorNube { get; set; }
        public string Url { get; set; }
        public bool? EsEliminado { get; set; }
        public int? NroBytes { get; set; }
        public DateTime? FechaSubida { get; set; }
        public DateTime? FechaEliminacion { get; set; }
        public bool? GrabacionContrato { get; set; }
        public int? IdServidorAsterisk { get; set; }
    }
}
