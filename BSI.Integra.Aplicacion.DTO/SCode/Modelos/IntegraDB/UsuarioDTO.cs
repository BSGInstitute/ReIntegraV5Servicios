namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class UsuarioDTO
    {
        public int IdPersonal { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public string Clave { get; set; } = null!;
        public int IdUsuarioRol { get; set; }
        public string CodigoAreaTrabajo { get; set; } = null!;
    }
    public class GestionUsuarioDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public string AreaTrabajo { get; set; }
        public int RolId { get; set; }
        public int PerId { get; set; }
        public string UsClave { get; set; }
        public int IdUsuario { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
    public class IntegraUsuarioDTO
    {
        public string? Guid { get; set; }
        public int IdPersonal { get; set; }
        public string NombrePersonal { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
