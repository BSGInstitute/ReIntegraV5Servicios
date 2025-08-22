using BSI.Integra.Aplicacion.Base;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class MatriculaCabecera : BaseIntegraEntity
    {
        [StringLength(50)]
        public string CodigoMatricula { get; set; } = null!;
        public int IdAlumno { get; set; }
        public int IdPespecifico { get; set; }
        public int IdEstadoPagoMatricula { get; set; }
        [StringLength(20)]
        public string? EstadoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
        [StringLength(20)]
        public string? EmpresaRuc { get; set; }
        [StringLength(50)]
        public string? EmpresaNombre { get; set; }
        [StringLength(100)]
        public string? EmpresaContacto { get; set; }
        [StringLength(50)]
        public string? EmpresaEmail { get; set; }
        [StringLength(5)]
        public string? EmpresaPaga { get; set; }
        [StringLength(50)]
        public string? EmpresaObservaciones { get; set; }
        public int? IdDocumentoPago { get; set; }
        public int? IdCoordinador { get; set; }
        public int? IdAsesor { get; set; }
        public int? IdEstadoMatricula { get; set; }
        [StringLength(20)]
        public string? FechaSuspendido { get; set; }
        [StringLength(20)]
        public string? UsuarioCoordinadorAcademico { get; set; }
        public string? ObservacionGeneralOperaciones { get; set; }
        [StringLength(20)]
        public string? UsuarioCoordinadorSupervision { get; set; }
        public int? IdCronograma { get; set; }
        public int? IdPeriodo { get; set; }
        [StringLength(20)]
        public string? UsuarioCoordinadorPreAsignacion { get; set; }
        public bool? VerificacionConforme { get; set; }
        public bool? FechaMatriculaValidada { get; set; }
        public bool? FechaPagoValidada { get; set; }
        public DateTime? FechaRetiro { get; set; }
        public int? GrupoCurso { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? IdPaquete { get; set; }
        public DateTime? FechaFinalizacion { get; set; }
        public int? IdEstadoMatriculaCertificado { get; set; }
        public int? IdSubEstadoMatriculaCertificado { get; set; }
        public bool? EsInhouse { get; set; }
        public DateTime? FechaPorMatricularMatriculado { get; set; }
    }
}
