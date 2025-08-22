namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoLegalPaisDTO
    {
        public int Id { get; set; }
        public int IdDocumentoLegal { get; set; }
        public int IdPais { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoLegalPaisComboDTO
    {
        public int Id { get; set; }
        public string DocumentoLegal { get; set; } = null!;
        public string Pais { get; set; } = null!;
    }
}
