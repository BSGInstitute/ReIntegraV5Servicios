namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AreaFormacionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    } 
}
