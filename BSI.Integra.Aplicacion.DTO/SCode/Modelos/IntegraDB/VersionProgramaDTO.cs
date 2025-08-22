using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class VersionProgramaDTO
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string? Nombre { get; set; } = null!;

    }
    public class VersionProgramaNombreUsuarioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Usuario { get; set; } = null!;
    }
}
