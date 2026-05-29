namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    /// <summary>
    /// DTO de entrada para operaciones CRUD de SubTipoEncuesta
    /// </summary>
    public class SubTipoEncuestaEntradaDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; } = null!;
    }

    /// <summary>
    /// DTO de salida para listar SubTipoEncuesta
    /// </summary>
    public class SubTipoEncuestaDTO
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
