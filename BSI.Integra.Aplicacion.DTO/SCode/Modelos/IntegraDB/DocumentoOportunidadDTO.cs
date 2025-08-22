namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoOportunidadDTO
    {
        public int Id { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdOportunidad { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public string? Comentario { get; set; }
        public int? IdClasificacionPersona { get; set; }
        public int? IdDocumentoOportunidadTipo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoOportunidadComboDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; } = null!;
    }
    public class DocumentoOportunidadInsertadoDTO
    {
        public string Url { get; set; }
        public string Comentario { get; set; }
        public int? Tipo { get; set; }
    }
    public class DatosOportunidadDocumentosCompuestoDTO
    {
        public int Id { get; set; }
        public int? IdCentroCosto { get; set; }
        public int? IdPersonalAsignado { get; set; }
        public int IdTipoDato { get; set; }
        public int IdFaseOportunidad { get; set; }
        public int? IdOrigen { get; set; }
        public int? IdPgeneral { get; set; }
        public int? IdAlumno { get; set; }
        public string UltimoComentario { get; set; }
        public int? IdActividadDetalleUltima { get; set; }
        public int? IdActividadCabeceraUltima { get; set; }
        public int? IdEstadoActividadDetalleUltimoEstado { get; set; }
        public DateTime? UltimaFechaProgramada { get; set; }
        public int IdEstadoOportunidad { get; set; }
        public int? IdEstadoOcurrenciaUltimo { get; set; }
        public int IdFaseOportunidadMaxima { get; set; }
        public int? IdFaseOportunidadInicial { get; set; }
        public int IdCategoriaOrigen { get; set; }
        public string CodigoPagoIC { get; set; }
        public string NombrePatner { get; set; }
        public string EncabezadoCorreoPartner { get; set; }
        public string PrecioContado { get; set; }
        public string NombreProgramaGeneral { get; set; }
        public string CostoTotalConDescuento { get; set; }
        public string pw_duracion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string IdMatricula { get; set; }
        public string Central { get; set; }
        public string UrlVersion { get; set; }
        public string UrlBrochurePrograma { get; set; }
        public string Anexo3CX { get; set; }
        public string UrlFirmaCorreos { get; set; }
        public string Email { get; set; }
        public string urlPartner { get; set; }
        public string NombreCiudad { get; set; }
        public bool? Promocion25 { get; set; }
        public int? IdCodigoPais { get; set; }
    }
    public class ProgramaDocumentosDTO
    {
        public int Id { get; set; }
        public string NombreDocumento { get; set; }
        public bool Habilitado { get; set; }
        public string Url { get; set; }
        public byte[] DocumentoByte { get; set; }
        public string Mensaje { get; set; }
        public string MensajeDetalle { get; set; }
        public List<AlertDTO> ListadoAlertas { get; set; }
    }
    public class AlertDTO
    {
        public int IdDocumento { get; set; }
        public string Mensaje { get; set; }
    }
    public class DocumentoComercialContenidoDTO
    {
        public string Contenido { get; set; }
    }
}
