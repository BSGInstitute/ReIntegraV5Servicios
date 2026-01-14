using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO de entrada para crear una solicitud de descuento
    /// </summary>
    public class TipoDescuentoSolicitudEntradaDTO
    {
        public int IdTipoDescuento { get; set; }
        public int IdOportunidad { get; set; }
        public int IdPersonalSolicitante { get; set; }
        public string? ComentarioSolicitud { get; set; }
        public IList<IFormFile>? Files { get; set; }
        public string Usuario { get; set; } = null!;
    }

    /// <summary>
    /// DTO de entrada para responder (aprobar/rechazar) una solicitud - Coordinador o Gerencia
    /// </summary>
    public class TipoDescuentoSolicitudRespuestaEntradaDTO
    {
        public int IdSolicitud { get; set; }
        public string? ComentarioRespuesta { get; set; }
        public IList<IFormFile>? Files { get; set; }
        public string Usuario { get; set; } = null!;
    }

    /// <summary>
    /// DTO para listado de solicitudes
    /// </summary>
    public class TipoDescuentoSolicitudListadoDTO
    {
        public int IdTipoDescuentoSolicitud { get; set; }
        public int IdTipoDescuento { get; set; }
        public string CodigoDescuento { get; set; } = null!;
        public string DescripcionDescuento { get; set; } = null!;
        public int IdOportunidad { get; set; }
        public string? CodigoOportunidad { get; set; }
        public int IdTipoDescuentoSolicitudEstado { get; set; }
        public string NombreEstado { get; set; } = null!;
        public int IdPersonal_Solicitante { get; set; }
        public string NombreSolicitante { get; set; } = null!;
        public string? ComentarioSolicitud { get; set; }
        public string? NombreArchivoSolicitud { get; set; }
        public string? ContentTypeSolicitud { get; set; }
        public string? ComentarioAprobacion { get; set; }
        public DateTime? FechaAprobacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public bool Estado { get; set; }
    }

    /// <summary>
    /// DTO de filtros para listar solicitudes con paginación
    /// </summary>
    public class TipoDescuentoSolicitudFiltroDTO
    {
        public int? IdTipoDescuentoSolicitudEstado { get; set; }
        public int? IdPersonal_Asignado { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int RegistrosPorPagina { get; set; } = 10;
    }

    /// <summary>
    /// DTO de item para listado de solicitudes paginado
    /// </summary>
    public class TipoDescuentoSolicitudItemDTO
    {
        public int IdTipoDescuentoSolicitud { get; set; }
        public string NombreAlumno { get; set; } = null!;
        public string NombrePrograma { get; set; } = null!;
        public string TipoDescuento { get; set; } = null!;
        public int NivelAprobacion { get; set; }
        public int SolicitudEstado { get; set; }
        public DateTime Fecha { get; set; }
        public int TotalRegistros { get; set; }
    }

    /// <summary>
    /// DTO de respuesta paginada para listado de solicitudes
    /// </summary>
    public class TipoDescuentoSolicitudPaginadoDTO
    {
        public List<TipoDescuentoSolicitudItemDTO> Items { get; set; } = new();
        public int TotalRegistros { get; set; }
        public int NumeroPagina { get; set; }
        public int RegistrosPorPagina { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / RegistrosPorPagina);
    }
}
