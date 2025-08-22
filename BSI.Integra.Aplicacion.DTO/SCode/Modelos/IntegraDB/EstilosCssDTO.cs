namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EstilosCssDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoCss { get; set; }
        public string? NombreTipo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class EstilosCssEnvio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? CodigoCss { get; set; }
        public string? NombreTipo { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class EstiloCombo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }

        public string? NombreTipo { get; set; }
        public string? CodigoCss { get; set; }
    }

}
