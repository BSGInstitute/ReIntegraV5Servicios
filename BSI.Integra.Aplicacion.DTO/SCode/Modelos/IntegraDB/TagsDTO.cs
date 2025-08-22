namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class TagsDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Texto { get; set; }
        public string? NombreTipo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class TagsEnvio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Texto { get; set; }
        public string? NombreTipo { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class ComboTag
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string? NombreTipo { get; set; }
    }



}
