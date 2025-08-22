namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ClasificacionPersonaDTO
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ClasificacionPersonaComboDTO
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoPersona { get; set; }
        public int IdTablaOriginal { get; set; }
    }
}
