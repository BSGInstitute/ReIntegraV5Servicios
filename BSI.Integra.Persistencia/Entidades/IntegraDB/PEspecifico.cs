using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PEspecifico : BaseIntegraEntity
    {
        [StringLength(300)]
        public string Nombre { get; set; } = null!;
        [StringLength(100)]
        public string? Codigo { get; set; }
        public int? IdCentroCosto { get; set; }
        [StringLength(100)]
        public string? Frecuencia { get; set; }
        [StringLength(100)]
        public string EstadoP { get; set; } = null!;
        [StringLength(100)]
        public string Tipo { get; set; } = null!;
        [StringLength(50)]
        public string? TipoAmbiente { get; set; }
        [StringLength(50)]
        public string Categoria { get; set; } = null!;
        public int? IdProgramaGeneral { get; set; }
        [StringLength(50)]
        public string Ciudad { get; set; } = null!;
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaTermino { get; set; }
        [StringLength(50)]
        public string? FechaInicioV { get; set; }
        [StringLength(50)]
        public string? FechaTerminoV { get; set; }
        [StringLength(20)]
        public string? CodigoBanco { get; set; }
        [StringLength(50)]
        public string? FechaInicioP { get; set; }
        [StringLength(50)]
        public string? FechaTerminoP { get; set; }
        public int? FrecuenciaId { get; set; }
        public int? EstadoPid { get; set; }
        public int? TipoId { get; set; }
        public int? CategoriaId { get; set; }
        public short? OrigenPrograma { get; set; }
        public int? IdCiudad { get; set; }
        [StringLength(20)]
        public string? CoordinadoraAcademica { get; set; }
        [StringLength(20)]
        public string? CoordinadoraCobranza { get; set; }
        [StringLength(10)]
        public string? Duracion { get; set; }
        [StringLength(1)]
        public string? ActualizacionAutomatica { get; set; }
        public int? IdCursoMoodle { get; set; }
        public bool? CursoIndividual { get; set; }
        public int? IdSesionInicio { get; set; }
        public int? IdExpositorReferencia { get; set; }
        public int? IdAmbiente { get; set; }
        [StringLength(255)]
        public string? UrlDocumentoCronograma { get; set; }
        public int? IdEstadoPespecifico { get; set; }
        [StringLength(255)]
        public string? UrlDocumentoCronogramaGrupos { get; set; }
        public int? IdTroncalPartner { get; set; }
        public int? IdCursoMoodlePrueba { get; set; }
        public int? IdCursoRa { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdProveedorCalificaProyecto { get; set; }
        public string? ObservacionCursoFinalizado { get; set; }
        public bool? EsEspecial { get; set; }
        public int? IdCiclo { get; set; }
        public int? IdPeriodoLectivo { get; set; }
        public int? IdTipoProgramaCarrera { get; set; }
        public virtual ICollection<ConfigurarWebinar> ConfigurarWebinars { get; set; }
        public virtual ICollection<CursoPespecifico> CursoPespecificos { get; set; }
        public virtual ICollection<FeedbackGrupoPreguntaProgramaEspecifico> FeedbackGrupoPreguntaProgramaEspecificos { get; set; }
        public virtual ICollection<SolicitudAlumno> SolicitudAlumnos { get; set; }
        public virtual ICollection<SolicitudOperacionesAccesoTemporalDetalle> SolicitudOperacionesAccesoTemporalDetalles { get; set; }
        public bool? ResumenClaseActivo {  get; set; }
        public bool? TutorVirtualActivo {  get; set; }
        public int? IdEstadoCupos { get; set; }
    }
}
