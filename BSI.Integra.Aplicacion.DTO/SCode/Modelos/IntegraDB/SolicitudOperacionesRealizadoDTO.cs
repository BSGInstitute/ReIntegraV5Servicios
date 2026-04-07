using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SolicitudOperacionesRealizadoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
    }

    public class DatosSolicitudOperacionesDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string PersonalAprobacion { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public bool Aprobado { get; set; }
        public bool EsCancelado { get; set; }
        public string ComentarioSolicitante { get; set; }
        public string Observacion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string UrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string ObservacionEncargado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Usuario { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
    }
    public class HistorialAsesoraDTO
    {
        public int IdOportunidad { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string? AsesoraAnterior { get; set; }
        public string? AsesoraNueva { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? UsuarioAprobacion { get; set; }
    }

    public class TodoSolicitudOperacionesDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public string TipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string PersonalSolicitante { get; set; }
        public string EmailSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string PersonalAprobacion { get; set; }
        public string EmailAprobador { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public bool Aprobado { get; set; }
        public bool EsCancelado { get; set; }
        public string ComentarioSolicitante { get; set; }
        public string Observacion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string UrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
        public string ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string ObservacionEncargado { get; set; }
        public string NombreCompleto { get; set; }
        public string Direccion { get; set; }
        public string Dni { get; set; }
        public string Correo { get; set; }
        public string CodigoMatricula { get; set; }
        public string CentroCosto { get; set; }
        public string Pespecifico { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Usuario { get; set; }
    }

    public class TipoSolicitudDTO
    {
        public int Id { get; set; }
        public string TipoSolicitud { get; set; }
    }
    public class SolicitudOperacionesDTO
    {
        public int IdOportunidad { get; set; }
        public int IdTipoSolicitudOperaciones { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public int IdPersonalAprobacion { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public bool Aprobado { get; set; }
        public bool EsCancelado { get; set; }
        public string ComentarioSolicitante { get; set; }
        public string? Observacion { get; set; }
        public int? IdUrlBlockStorage { get; set; }
        public string? NombreArchivo { get; set; }
        public string? ContentType { get; set; }
        public bool? Realizado { get; set; }
        public string? ObservacionEncargado { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public string Usuario { get; set; }

        // Para los casos de cambio de estado
        public string? ValorNuevoSubestado { get; set; }
        public int? RelacionEstadoSubEstado { get; set; }
        public int Id { get; set; }

        // Para los casos de accesos temporales
        public List<int>? ListaIdPEspecificos { get; set; }
        public IList<IFormFile>? Files { get; set; }
    }
    public class filtroReporteDTO
    {

        public List<int>? asesores { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaInicio { get; set; }
        public int? tipoSolicitud { get; set; }
        public bool? realizado { get; set; }
        public bool? cancelado { get; set; }
        public int? estadoSolicitud { get; set; }

    }

    public class filtroReportetipo4DTO
    {

        public List<int>? asesores { get; set; }
        public DateTime fechaFin { get; set; }
        public DateTime fechaInicio { get; set; }
        public int? tipoSolicitud { get; set; }
        public int? estadoSolicitud { get; set; }

    }



    public class EstadoCursoDTO
    {

        public int Id { get; set; }
        public int? IdPEspecificoSesionEstado { get; set; }
    }
    public class EstadoCursoObservacionDTO
    {
        public int Id { get; set; }
        public int? IdPEspecificoSesionEstadoObservacionDetalle { get; set; }
    }
}
