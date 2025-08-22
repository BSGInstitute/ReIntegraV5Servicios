namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EntidadFinancieraDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public bool Estado { get; set; }
        public string UsuarioModificacion { get; set; } = null!;
        public string UsuarioCreacion { get; set; } = null!;
    }

    public class EntidadFinancieraRecibidoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public int IdMoneda { get; set; }
    }

    public class EntidadFinancieraComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
