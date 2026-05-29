namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    /// <summary>
    /// DTO de entrada para asociar/desasociar TipoEncuesta con SubTipoEncuesta
    /// </summary>
    public class TipoSubTipoEncuestaEntradaDTO
    {
        public int? Id { get; set; }
        public int IdTipoEncuesta { get; set; }
        public int IdSubTipoEncuesta { get; set; }
        public string Usuario { get; set; } = null!;
    }

    /// <summary>
    /// DTO de salida: relacion tipo-subtipo con nombres para mostrar en la grilla
    /// </summary>
    public class TipoSubTipoEncuestaDTO
    {
        public int Id { get; set; }
        public int IdTipoEncuesta { get; set; }
        public string? NombreTipoEncuesta { get; set; }
        public int IdSubTipoEncuesta { get; set; }
        public string? NombreSubTipoEncuesta { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
