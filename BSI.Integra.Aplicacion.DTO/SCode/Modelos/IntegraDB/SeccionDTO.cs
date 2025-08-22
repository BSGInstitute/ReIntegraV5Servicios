namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SeccionDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool EstadoTexto { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }



    public class SeccionEnvio
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool EstadoTexto { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class SeccionCombo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public bool EstadoTexto { get; set; }
    }

}
