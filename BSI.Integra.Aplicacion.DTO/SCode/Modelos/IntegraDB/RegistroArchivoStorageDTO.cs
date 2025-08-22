using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RegistroArchivoStorageDTO
    {
        public int Id { get; set; }
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string Ruta { get; set; } = null!;
        public bool Estado { get; set; }
        //public int IdMigracion { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }


    }

    public class RegistroArchivoStorageComboDTO
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; } = null!;
    }
    public class RegistroArchivoStoragePermisosDTO
    {
        public int Id { get; set; }
        public string Contenedor { get; set; }
        public string NombreArchivo { get; set; }
        public string Ruta { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int IdUrlBlockStorage { get; set; }
    }
    public class RegistroArchivoMostrarFiltroDTO
    {
        public int IdPersonal { get; set; }
        public int IdUrlBlockStorage { get; set; }
        public string NombreArchivo { get; set; }
    }

    public class RegistroArchivoStorageRepositorio
    {
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreUsuario { get; set; }
        public string Ruta { get; set; }

    }
    public class RegistroArchivoStorage_RegistrarDTO
    {
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; }
        public string NombreUsuario { get; set; }
        public string Ruta { get; set; }
    }

    public class RegistroArchivoStorageSubirArchivoDTO

    {
        public IList<IFormFile> Archivos { get; set; } = null!;
        public int IdUrlSubContenedor { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string RutaCompleta { get; set; } = null!;
        public string RutaBlob { get; set; } = null!;
        public IList<IFormFile>? ArchivoBol { get; set; } = null!;
        public IList<IFormFile>? ArchivoCol { get; set; } = null!;
        public IList<IFormFile>? ArchivoInt { get; set; } = null!;
        public IList<IFormFile>? ArchivoPeLima { get; set; } = null!;
        public IList<IFormFile>? ArchivoPeAqp { get; set; } = null!;

    }

    public class RegistroArchivoObtenerUrlComboDTO
    {
        public int Id { get; set; }
        public string Ruta { get; set; }
        public string NombreArchivo { get; set; }
    }
}




