namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class DocumentoLegalAreaTrabajoDTO
    {
        public int Id { get; set; }
        public int IdDocumentoLegal { get; set; }
        public int IdPersonalAreaTrabajo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class DocumentoLegalAreaTrabajoComboDTO
    {
        public int Id { get; set; }
        public string DocumentoLegal { get; set; } = null!;
        public string PersonalAreaTrabajo { get; set; } = null!;
    }
}
