namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoLegalDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Url { get; set; } = null!;
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string? Roles { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoLegalComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }

    public class DocumentoLegalV2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
        public int Area { get; set; }
        public List<int> Areas { get; set; }
        public List<int> Paises { get; set; }
        public List<DocumentoLegalPaisDTO> PaisesBD { get; set; }
        public string Roles { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string Usuario { get; set; }
        public byte[] DocumentoByte { get; set; }
    }

    public class DocumentoLegalV3DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string Url { get; set; }
        public int Area { get; set; }
        public List<int> Areas { get; set; }
        public List<int> Paises { get; set; }
        public string Roles { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string Usuario { get; set; }
        public byte[] DocumentoByte { get; set; }
    }
    public class DocumentoLegalAgendaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPais { get; set; }
        public string? Pais { get; set; }
        public string Url { get; set; } = null!;
        public int IdPersonalAreaTrabajo { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string? Roles { get; set; }
    }
    public class DocumentoLegalAgendaByteDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public int IdPais { get; set; }
        public string? Pais { get; set; }
        public string Url { get; set; } = null!;
        public int IdPersonalAreaTrabajo { get; set; }
        public bool? VisualizarAgenda { get; set; }
        public bool? DescargarAgenda { get; set; }
        public string? Roles { get; set; }
        public byte[]? DocumentoByte { get; set; }
    }
}
