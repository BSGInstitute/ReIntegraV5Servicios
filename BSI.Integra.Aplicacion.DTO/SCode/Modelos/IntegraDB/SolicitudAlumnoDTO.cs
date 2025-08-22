using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudAlumnoDTO
    {
        public int Id { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public int IdPersonal { get; set; }
        public int IdSolicitud { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspescifico { get; set; }
        public string DetalleSolicitud { get; set; } = null!;
        public string ContentTypeSolicitante { get; set; } = null!;
        public string NombreArchivoSolicitante { get; set; } = null!;
        public string ContentTypeSolucion { get; set; } = null!;
        public string NombreArchivoSolucion { get; set; } = null!;
        public string ComentarioSolucion { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public int? IdMigracion { get; set; }
        public int? IdControlSolicitudOrigen { get; set; }

    }

    public class SolicitudAlumnoEntradaDTO
    {
        public int? Id { get; set; }
        public int? IdEstadoSolicitud { get; set; }
        public int IdPersonal { get; set; }
        public int IdSolicitud { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public string DetalleSolicitud { get; set; } = null!;
        public string? ContentTypeSolicitante { get; set; }
        public string? NombreArchivoSolicitante { get; set; }
        public string? ContentTypeSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }
        public string? ComentarioSolucion { get; set; }
        public IList<IFormFile>? Files { get; set; }
        public string? Usuario { get; set; }
        public int? IdControlSolicitudOrigen { get; set; }
    }

    public class FiltroSolicitudesDTO
    {
        public int IdPersonal { get; set; }
        public int TipoFiltro { get; set; }
        public int? Filtro1 { get; set; }
        public string? Filtro2 { get; set; }
    }
    public class FiltroSolicitudAlumnoReporteDTO
    {
        public int? IdMatriculaCabecera { get; set; }
        public string IdEstadoSolicitud { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }

    }

    public class SolicitudAlumnoFiltradaDTO
    {
        public int id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public int IdPEspecifico { get; set; }
        public string NombrePEspecifico { get; set; }
        public int IdCentroCosto { get; set; }
        public string CentroCosto { get; set; }
        public int IdPGeneral { get; set; }
        public string PGeneral { get; set; }
        public string DetalleSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        public string Prioridad { get; set; }
        //public int IdSolicitud { get; set; }
        public string NombreSolicitud { get; set; }
        public int IdTipoReporte { get; set; }
        public string Tipo { get; set; }
        public int IdSolicitudCategoria { get; set; }
        public string NombreSolicitudCategoria { get; set; }
        public int IdSubCategoria { get; set; }
        public string NombreSubCategoria { get; set; }
        public int IdSolicitante { get; set; }
        public string NombreSolicitante { get; set; }
        public int IdAreaSolicitante { get; set; }
        public string AreaSolicitante { get; set; }
        public int IdAreaRevision { get; set; }
        public string AreaRevision { get; set; }
        public string NombreArchivoSolicitante { get; set; }
        public int IdPersonalRevision { get; set; }
        public string PersonalRevision { get; set; }
        public int IdAreaSolucion { get; set; }
        public string AreaSolucion { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string PersonalSolucion { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaModificacion { get; set; }
        public string ComentarioSolucion { get; set; }
        public string NombreArchivoSolucion { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public string EstadoSolicitud { get; set; }
        public string Email { get; set; }
        public int? IdControlSolicitudOrigen { get; set; }
        public string? ControlSolicitudOrigen { get; set; }
    }
    public class FiltroSolicitudAlumnoDTO
    {
        public int? IdMatriculaCabecera { get; set; }
        public List<int> IdEstadoSolicitud { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? IdUsuario { get; set; }
    }
    public class SolicitudLogDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdSolicitudAlumno { get; set; }
        public string Campo { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }

    public class RevisarSolicitudAlumnoDTO
    {
        public int id { get; set; }
        public string? ContentTypeSolucion { get; set; }
        public string? NombreArchivoSolucion { get; set; }
        public string ComentarioSolucion { get; set; }
        public int IdEstadoSolicitud { get; set; }
        public IList<IFormFile>? Files { get; set; }
        public string Usuario { get; set; }
    }
    public class SolicitudPersonalAlumnoDTO
    {
        public int idPersonal { get; set; }
        public string? Personal { get; set; }
    }
    public class SolicitudPersonalSolucionAlumnoDTO
    {
        public int idPersonal { get; set; }
        public string? Personal { get; set; }
        public int idAreaTrabajo { get; set; }
    }

    public class ReporteSolicitudAlumnoDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string Codigo { get; set; }
        public string NombreAlumno { get; set; }
        public int IdCurso { get; set; }
        public string Curso { get; set; }
        public int IdTipoSolicitud { get; set; }
        public string TipoSolicitud { get; set; }
        public int IdCategoria { get; set; }
        public string Categoria { get; set; }
        public int IdProblema { get; set; }
        public string Problema { get; set; }
        public int? IdControlSolicitudOrigen { get; set; } = 0;
        public string? ControlSolicitudOrigen { get; set; } = null;
        public string DescripcionSolucion { get; set; }
        public string DetalleSolicitud { set; get; }
        public int IdAreaRevision { get; set; }
        public string AreaRevision { get; set; }
        public int IdPersonalRevision { get; set; }
        public string PersonalRevision { get; set; }
        public int IdAreaSolucion { get; set; }
        public string AreaSolucion { get; set; }
        public int IdPersonalSolucion { get; set; }
        public string PersonalSolucion { get; set; }
        public string Prioridad { get; set; }
        public int IdAreaSolicitante { get; set; }
        public string AreaSolicitante { get; set; }
        public int IdSolicitante { get; set; }
        public string Solicitante { get; set; }
        public int IdEstado { get; set; }
        public string Estado { get; set; }
        public string FechaRegistro { get; set; }
        public string FechaSolucion { get; set; }
        public string ComentarioSolucion { get; set; }
        public string TipoSolucion { get; set; }
        public string TiempoAtencion { get; set; }
    }
    public class FiltroReporteSolicitudAlumnoDTO
    {
        public int? IdMatriculaCabecera { get; set; }
        public List<int>? IdEstadoSolicitud { get; set; }
        public List<int>? IdSolicitante { get; set; }
        public List<int>? IdOrigen { get; set; }
        public List<int>? IdAreaSolucion { get; set; }
        public List<int>? IdPersonalSolucion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
    public class FiltroReporteSolicitudAlumnoReporteDTO
    {
        public int? IdMatriculaCabecera { get; set; }
        public string? IdEstadoSolicitud { get; set; }
        public string? IdSolicitante { get; set; }
        public string? IdOrigen { get; set; }
        public string? IdAreaSolucion { get; set; }
        public string? IdPersonalSolucion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }



}
