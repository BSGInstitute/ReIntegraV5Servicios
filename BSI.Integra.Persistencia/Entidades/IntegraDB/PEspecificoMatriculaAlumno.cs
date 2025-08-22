using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PEspecificoMatriculaAlumno : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPespecificoTipoMatricula { get; set; }
        public int? IdUsuarioMoodle { get; set; }
        public int? IdCursoMoodle { get; set; }
        public int Grupo { get; set; }
        public int? Duracion { get; set; }
        public bool? EsAonline { get; set; }
        public bool? Regularizado { get; set; }
        public string? ErrorCongelamiento { get; set; }
        public bool? AplicaNuevaAulaVirtual { get; set; }
        public decimal? NotaAulaVirtualAnterior { get; set; }
        public int? IdEscalaCalificacionDetalle { get; set; }
        public bool? RecuperaNuevaAulaVirtual { get; set; }
        public DateTime? FechaFinCronograma { get; set; }
        public DateTime? FechaFinReal { get; set; }
    }
}
