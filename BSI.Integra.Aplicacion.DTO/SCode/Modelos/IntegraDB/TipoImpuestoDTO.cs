namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TipoImpuestoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Valor { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string NombrePais { get; set; }
        public int IdPais { get; set; }
        public bool Activo { get; set; }
    }
    public class TipoImpuestoRecibidoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; }
        public int IdPais { get; set; }
        public int Valor { get; set; }
        public bool Activo { get; set; }
    }
    public class TipoImpuestoComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
    }
    public class TipoImpuestoValorIgvDTO
    {
        public int Id { get; set; }
        public string Valor { get; set; } = null!;
    }

}
