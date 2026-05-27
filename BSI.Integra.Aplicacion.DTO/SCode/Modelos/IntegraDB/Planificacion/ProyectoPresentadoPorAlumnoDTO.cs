using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ProyectoPresentadoPorAlumnoDTO
    {
        public string IdEnvio { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCosto { get; set; }
        public string CodigoMatricula { get; set; }
        public string Alumno { get; set; }
        public string NombreArchivo { get; set; }
        public string EnlaceArchivo { get; set; }
        public string FechaEnvio { get; set; }
        public string HoraEnvio { get; set; }
        public string FechaCalificacion { get; set; }
        public string HoraCalificacion { get; set; }
        public string Nota { get; set; }
        public string CoordinadorAcademico { get; set; }
        public string Docente { get; set; }
        public string ResponsableCoordinacion { get; set; }
        public string NroEnvio { get; set; }
        public string Comentarios { get; set; }
        public string DocenteCalificacion { get; set; }
        public string ResponsableCoordinacionDocenteCalificacion { get; set; }
        public string NombreArchivoRetroalimentacion { get; set; } = null;
        public string UrlArchivoSubidoRetroalimentacion { get; set; } = null;
        public bool? EstadoDevuelto { get; set; }
        public string? EstadoCalificacion { get; set; }
        public string? SubEstadoCalificacion { get; set; }
        public string? EstadoEntrega { get; set; }
        public string? SubEstadoEntrega { get; set; }
        public string? EstadoRevisionProyecto { get; set; }
        public string? PlazoCargaRevision { get; set; }
        public DateTime? FechaEnvioOriginal { get; set; }
        public DateTime? FechaCalificacionOriginal { get; set; }
    }
    public class ProyectoPresentadoPorAlumnoFiltroDTO
    {
        public List<int>? ProgramaEspecifico { get; set; }
        public List<int>? CentroCosto { get; set; }
        public List<int>? Docente { get; set; }
        public List<int>? Coordinadora { get; set; }
        public int? CodigoMatricula { get; set; }
        public int? EstadoRevision { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFin { get; set; }
    }
    public class ObtenerDataComboDTO
    {
        public List<ComboDTO> ObtenerCoordinadorasDocente { get; set; }
        public List<ComboDTO> ObtenerNombreProveedorParaHonorario { get; set; }
        public List<ComboDTO> ObtenerCombo { get; set; }
        public List<ComboDTO> ObtenerProgramaEspecifico { get; set; }
    }
}
