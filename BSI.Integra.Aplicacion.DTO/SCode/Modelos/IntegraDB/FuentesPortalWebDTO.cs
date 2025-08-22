using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class FuentesPortalWebDTO
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? Url { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        public Guid? IdMigracion { get; set; }
    }

    public class comboFuentes
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
    }

    public class FuentesPortalWebEnvio
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? Url { get; set; }
        public string? Usuario { get; set; } = null!;
    }

    public class FuentesPortalWEBSubirArchivoDTO
    {
        public int Id { get; set; }
        public string? NombreArchivo { get; set; }
        public string? Url { get; set; }
        public IList<IFormFile> Archivos { get; set; } = null!;
        public string? Usuario { get; set; }
    }

}
