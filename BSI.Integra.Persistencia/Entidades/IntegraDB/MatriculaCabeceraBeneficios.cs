using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MatriculaCabeceraBeneficios : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdSuscripcionProgramaGeneral { get; set; }
        public int? IdConfiguracionBeneficioProgramaGeneral { get; set; }
        public int? IdEstadoMatriculaCabeceraBeneficio { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public int? IdEstadoSolicitudBeneficio { get; set; }
        public int? Duracion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        [StringLength(50)]
        public string? UsuarioAprobacion { get; set; }
        [StringLength(50)]
        public string? UsuarioEntregoBeneficio { get; set; }
    }
}
