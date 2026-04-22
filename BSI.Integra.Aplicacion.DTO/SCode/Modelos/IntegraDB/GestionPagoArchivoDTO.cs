using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para lectura de archivo adjunto de gestión de pago
    /// </summary>
    public class GestionPagoArchivoDTO
    {
        public int Id { get; set; }
        public int IdGestionPago { get; set; }
        public int? IdGestionPagoCronograma { get; set; }
        public string NombreArchivo { get; set; } = null!;
        public string ContentTypeArchivo { get; set; } = null!;
    }

    /// <summary>
    /// DTO para inserción de archivo adjunto
    /// </summary>
    public class GestionPagoArchivoInsertarDTO
    {
        public IFormFile? Archivo { get; set; }
        public int? Id { get; set; }
        public int? IdGestionPagoCronograma { get; set; }
        public string? NombreArchivo { get; set; }
        public string? ContentTypeArchivo { get; set; }
        public string? Usuario { get; set; }
    }
}
