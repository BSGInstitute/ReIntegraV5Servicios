namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class TipoEncuestumDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
    }

    public class TipoEncuestumEntradaDTO
    {
        public int? Id { get; set; }
        public string? Nombre { get; set; }
        public string Usuario { get; set; }
    }

}
