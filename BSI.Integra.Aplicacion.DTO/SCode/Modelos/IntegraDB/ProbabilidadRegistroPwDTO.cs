namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProbabilidadRegistroPwDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public Guid IdCodigo { get; set; }
        public int Codigo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
