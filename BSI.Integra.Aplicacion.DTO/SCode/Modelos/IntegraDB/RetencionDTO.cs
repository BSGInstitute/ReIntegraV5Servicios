namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RetencionDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Pais { get; set; }
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
    }
    public class RetencionRecibidoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
    }
    public class RetencionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
    }
}
