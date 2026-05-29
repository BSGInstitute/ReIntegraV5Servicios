namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class TipoEncuestaEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; } = null!;
    }

    public class TipoEncuestaDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
