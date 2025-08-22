using BSI.Integra.Aplicacion.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ControlDocAlumno : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdCriterioCalificacion { get; set; }
        [StringLength(20)]
        public string QuienEntrego { get; set; }
        public DateTime? FechaEntregaDocumento { get; set; }
        [StringLength(200)]
        public string Observaciones { get; set; }
        [StringLength(20)]
        public string ComisionableEditable { get; set; }
        [Precision(9, 2)]
        public decimal? MontoComisionable { get; set; }
        [StringLength(200)]
        public string ObservacionesComisionable { get; set; }
        [Precision(9, 2)]
        public decimal? PagadoComisionable { get; set; }
        public int? IdMatriculaObservacion { get; set; }
        [StringLength(50)]
        public string IdMigracion { get; set; }
    }
}
