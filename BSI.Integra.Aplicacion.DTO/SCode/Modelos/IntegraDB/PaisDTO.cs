using Microsoft.AspNetCore.Http;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PaisDTO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public Guid? IdMigracion { get; set; }
        public int? CodigoGoogleId { get; set; }
        public string? CodigoPaisMoodle { get; set; }
        public string? RutaBandera { get; set; }
        public string? RutaIcono { get; set; }
        public int? EstadoVisualizacion { get; set; }
    }
    public class PaisComboDTO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string NombrePais { get; set; } = null!;
    }
    public class PaisZonaHorariaDTO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string NombrePais { get; set; } = null!;
        public decimal ZonaHoraria { get; set; }
    }
    public class UrlBlockStoragePais
    {
        public string RutaCompleta { get; set; }
        public string RutaBlob { get; set; }
    }
    public class RegistroPaisDTO
    {
        public int Id { get; set; }
        public int CodigoPais { get; set; }
        public string CodigoIso { get; set; } = null!;
        public string NombrePais { get; set; } = null!;
        public string Moneda { get; set; } = null!;
        public decimal ZonaHoraria { get; set; }
        public int EstadoPublicacion { get; set; }
        public string? RutaCompletaBandera { get; set; }
        public string? RutaBlobBandera { get; set; }
        public string? RutaCompletaIcono { get; set; }
        public string? RutaBlobIcono { get; set; }
        public IFormFile? Bandera { get; set; } = null!;
        public IFormFile? Icono { get; set; } = null!;
    }

    public class PaisMonedaComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Moneda { get; set; }
    }
    public class PaisZonaHorariaComboDTO
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ZonaHoraria { get; set; }
    }
    public class PlantillaPaisFiltroDTO
    {
        public int Id { get; set; }
        public string NombrePais { get; set; }
        public string CodigoISO { get; set; }

    }
    public class PaisDiferenciaHorariaDTO
    {
        public int? Id { get; set; }
        public int? DiferenciaHoraria { get; set; }
    }

}
