namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class IntegraAspNetUserDTO
    {
        public string? UsClave { get; set; }
        public int PerId { get; set; }
        public int RolId { get; set; }
        public string? AreaTrabajo { get; set; }
        public string? Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; } = null!;
    }
    public class ModuloFinalDto
    {
        public string Modulo { get; set; } = null!;
        public List<GrupoListaDTO2> GrupoLista { get; set; } = new List<GrupoListaDTO2>();
    }
    public class GrupoListaDTO
    {
        public string SubGrupo { get; set; } = null!;
        public string Url { get; set; } = null!;
    }
    public class GrupoListaDTO2
    {
        public string SubGrupo { get; set; } = null!;
        public List<string> Url { get; set; } = new List<string>();
    }
    public class UserIntegraAspNetDTO
    {
        public string? Guid { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int? PerId { get; set; }
        public int RolId { get; set; }
        public string AreaAbrev { get; set; }
        public string Email { get; set; }
    }
    public class DatoPersonalDTO
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
    }
}
