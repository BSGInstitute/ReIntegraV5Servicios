using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteControlTareaAlumnoDTO
    {
        public int Id { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string CentroCosto { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string Curso { get; set; }
        public int? Version { get; set; }
        public int NroTarea { get; set; }
        public string NombreArchivo { get; set; }
        public string DireccionUrl { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime? FechaCalificacion { get; set; }
        public string Nota { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string MatriculaAlumnoResponsableRevision { get; set; }
        public string NombreAlumnoResponsableRevision { get; set; }
        public DateTime? FechaAsignacion { get; set; }
        public string CoordinadorResponsableRevision { get; set; }
        public bool? EsDocente { get; set; }
    }
    public class ReporteControlTareaAlumnoFiltroDTO
    {
        public List<int>? IdsProgramasEspecificos { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public List<int>? IdsAlumnos { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool? EstadoTarea { get; set; }
    }
    public class ReporteControlTareaAlumnoActualizacionDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
    }
    public class ReporteControlTareaAlumnoComboDTO
    {
        public List<ComboDTO> PEspecifico { get; set; }
        public List<ComboDTO> Proveedor { get; set; }
    }
}
