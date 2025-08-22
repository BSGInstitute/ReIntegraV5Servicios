namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class SemaforoFinancieroDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Usuario { get; set; }

        public List<SemaforoFinancieroDetalleDTO> Detalle { get; set; }
    }
    public class SemaforoFinancieroComboDTO
    {
        public int Id { get; set; }
        public string NombrePais { get; set; } = null!;
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int? IdMigracion { get; set; }
        public string RowVersion { get; set; }
    }
    public class SemaforoFinancieroNuevoDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class SemaforoFinancieroInsertarNuevoDTO
    {
        public int Id { get; set; }
        public int IdPais { get; set; }
        public bool Activo { get; set; }
        public string Usuario { get; set; }
        public List<SemaforoFinancieroDetalleNuevoDTO> Detalle { get; set; }
    }
    public class SemaforoFinancieroDetalleNuevoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public string Color { get; set; } = null!;
    }
}
