namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoAgendaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string MensajeDetalle { get; set; } = null!;
        public bool Generado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoAgendaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
    public class DocumentoAgendaSinAuditoriaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string MensajeDetalle { get; set; } = null!;
        public bool Generado { get; set; }
    }
    public class DocumentoAgendaDescargaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Habilitado { get; set; }
        public string Url { get; set; } = null!;
        public byte[] DocumentoByte { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public string MensajeDetalle { get; set; } = null!;
        public bool Generado { get; set; }
    }
    public class DocumentoAgendaDetalleDTO
    {
        public OportunidadCompuestoDTO Oportunidad { get; set; } = null!;
        public PEspecificoPorIdCentroCostoDTO? ProgramaEspecifico { get; set; }
        public List<DocumentoAgendaDescargaDTO> Documentos { get; set; } = new List<DocumentoAgendaDescargaDTO>();
    }
    public class ArchivoNombreContenidoByteDTO
    {
        public string? Nombre { get; set; }
        public byte[]? ArchivoByte { get; set; }
    }
}
