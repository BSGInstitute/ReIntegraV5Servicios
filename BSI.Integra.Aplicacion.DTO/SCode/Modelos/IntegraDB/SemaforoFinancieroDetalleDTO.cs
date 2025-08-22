namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SemaforoFinancieroDetalleDTO
    {
        public int Id { get; set; }
        public int IdSemaforoFinanciero { get; set; }
        public string Nombre { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public string Color { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; }
        public bool Estado { get; set; }
    }
    public class SemaforoFinancieroDetalleComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
