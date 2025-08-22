using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class PerfilPuestoTrabajoDTO
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El valor de Id debe ser mayor o igual que 0.")]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El campo Nombre no puede tener más de 50 caracteres.")]
        public string Nombre { get; set; }
    }
    public class PuestoTrabajoRelacionDetalleDTO
    {
        public int Id { get; set; }
        public int IdPuestoTrabajoRelacionDetalle { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public int? IdPuestoTrabajo_Dependencia { get; set; }
        public int? IdPuestoTrabajo_PuestoACargo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }

        public string PuestoTrabajo_Dependencia { get; set; }
        public string PuestoTrabajo_PuestoACargo { get; set; }
        public string PersonalAreaTrabajo { get; set; }
    }
    public class PuestoTrabajoRelacionCompuestoDTO
    {
        public int Id { get; set; }
        public int IdPerfilPuestoTrabajo { get; set; }
        public List<FiltroIdNombrePKDTO> ListaPuestoDependencia { get; set; }
        public List<FiltroIdNombrePKDTO> ListaPuestoRelacionInterna { get; set; }
        public List<FiltroIdNombrePKDTO> ListaPuestoACargo { get; set; }
    }
    public class FiltroIdNombrePKDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int PK { get; set; }

    }

    public class ObtenerExamenDTO
    { 
        public List<PuestoTrabajoRelacionCompuestoDTO>? ListaPuestoTrabajoRelacion { get; set; }
        public List<PuestoTrabajoFuncionDTO>? ListaPuestoTrabajoFuncion { get; set; }
        public List<PuestoTrabajoReporteDTO>? ListaPuestoTrabajoReporte { get; set; }
        public List<PuestoTrabajoCursoComplementarioDTO>? ListaPuestoTrabajoCursoComplementario { get; set; }
        public List<PuestoTrabajoExperienciaPuestoTrabajo>? ListaPuestoTrabajoExperiencia { get; set; }
        public List<PuestoTrabajoCaracteristicaPersonalDTO>? ListaPuestoTrabajoCaracteristicaPersonal { get; set; }
        public List<PuestoTrabajoFormacionAcademicaDTO>? ListaPuestoTrabajoFormacionAcademica { get; set; }
        public List<PuestoTrabajoNombreEvaluacionAgrupadaComponenteDTO>? ListaEvaluacionesPuntajeCalificacion { get; set; }
        public List<PuestoTrabajoNombreEvaluacionesAgrupadaIndependienteDTO>? ListaEvaluaciones { get; set; }

    }
    public class PuestoTrabajoVersionesDTO
    {
        public int Id { get; set; }
        public int IdPuestoTrabajo { get; set; }
        public string PuestoTrabajo { get; set; }
        public int Version { get; set; }
        public string Objetivo { get; set; }
        public string Descripcion { get; set; }
        public string Personal_Solicitud { get; set; }
        public string Personal_Aprobacion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public int IdPerfilPuestoTrabajoEstadoSolicitud { get; set; }
        public string PerfilPuestoTrabajoEstadoSolicitud { get; set; }
        public string Observacion { get; set; }
        public bool EsActual { get; set; }
    }

    public class PersonalAprobacionDTO
    {
        public int IdPuestoTrabajo { get; set; }
    }
}
