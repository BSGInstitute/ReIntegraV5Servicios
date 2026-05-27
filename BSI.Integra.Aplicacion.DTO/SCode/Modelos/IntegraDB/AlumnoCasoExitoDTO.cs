using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class AlumnoCasoExitoDTO
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string NombreAlumno { get; set; } = null!;
        [StringLength(300)]
        public string NombrePrograma { get; set; } = null!;
        [StringLength(150)]
        public string? FotoPerfil { get; set; }
        [StringLength(150)]
        public string? FotoPerfilAlf { get; set; }
        public string? Testimonio { get; set; }
        public int IdPais { get; set; }
        public string? NombrePais { get; set; }
        public int Posicion { get; set; }
        public bool EstadoVisibilidad { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string? UrlFotoPerfil { get; set; }
        public Guid? IdMigracion { get; set; }
    }

    public class AlumnoCasoExitoEntradaDTO
    {
        public int Id { get; set; }
        [StringLength(150)]
        public string NombreAlumno { get; set; } = null!;
        [StringLength(300)]
        public string NombrePrograma { get; set; } = null!;
        [StringLength(150)]
        public string? FotoPerfil { get; set; }
        [StringLength(150)]
        public string? FotoPerfilAlf { get; set; }
        public string? Testimonio { get; set; }
        public int IdPais { get; set; }
        public int Posicion { get; set; }
        public bool EstadoVisibilidad { get; set; }
        public IFormFile? ArchivoFotoPerfil { get; set; }
    }

    public class AlumnoCasoExitoPosicionDTO
    {
        public int Id { get; set; }
        public int Posicion { get; set; }
    }

    public class AlumnoCasoExitoVisibilidadDTO
    {
        public int Id { get; set; }
        public bool EstadoVisibilidad { get; set; }
    }
}
