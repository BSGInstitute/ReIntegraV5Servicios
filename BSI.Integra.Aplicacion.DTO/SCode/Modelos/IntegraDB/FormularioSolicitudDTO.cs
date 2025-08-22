using BSI.Integra.Aplicacion.DTO.SCode;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FormularioSolicitudDTO


    {
        public int Id { get; set; }

        public int? IdFormularioRespuesta { get; set; }

        public string Nombre { get; set; } = null!;

        public string Codigo { get; set; } = null!;

        public string Campanha { get; set; } = null!;

        public int? IdConjuntoAnuncio { get; set; }

        public string Proveedor { get; set; } = null!;

        public int IdFormularioSolicitudTextoBoton { get; set; }

        public int TipoSegmento { get; set; }

        public string CodigoSegmento { get; set; } = null!;

        public int TipoEvento { get; set; }

        public string? UrlbotonInvitacionPagina { get; set; }

        public bool Estado { get; set; }

        public string UsuarioCreacion { get; set; } = null!;

        public string UsuarioModificacion { get; set; } = null!;

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Guid? IdMigracion { get; set; }
    }

    public class FormularioSolicitudCompuestoDTO
    {
        public int Total { get; set; }
        public int Id { get; set; }
        public int? IdFormularioRespuesta { get; set; }
        public string FormularioRespuesta { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public string NombreCampania { get; set; } = null!;
        public int? IdCampania { get; set; }
        public string Proveedor { get; set; } = null!;
        public int IdFormularioSolicitudTextoBoton { get; set; }
        public int TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; } = null!;
        public int TipoEvento { get; set; }
        public string UrlbotonInvitacionPagina { get; set; } = null!;
    }
    public class FiltroCompuestroGrillaDTO
    {
        public PaginadorDTO paginador { get; set; }
        public GridFiltersDTO? filter { get; set; }
    }
    public class FormularioSolicitudDatoDTO
    {
        public int? Id { get; set; }
        public int? IdFormularioRespuesta { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string NombreCampania { get; set; }
        public int? IdCampania { get; set; }
        public string? Proveedor { get; set; }
        public int? IdFormularioSolicitudTextoBoton { get; set; }
        public int? TipoSegmento { get; set; }
        public string CodigoSegmento { get; set; }
        public int? TipoEvento { get; set; }
        public string Usuario { get; set; }
    }

    public class InsertarFormularioSolicitudCampoDTO
    {
        public FormularioSolicitudDatoDTO Formulario { get; set; }
        public List<datosInsertarCamposDTO> Campo { get; set; }
    }


    public class InsertarFormulario2DTO
    {
        public string Nombre { get; set; }
    }

    public class filtroPrueba
    {
        public PaginadorDTO paginador { get; set; }
        public List<GridFilterDTO>? filter { get; set; }
    }

}




