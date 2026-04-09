using System.Text.Json.Serialization;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para lectura de gestión de pago con datos relacionados
    /// </summary>
    public class GestionPagoDTO
    {
        public int IdGestionPago { get; set; }
        public int IdComprobantePago { get; set; }
        public bool? ServicioValidado { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string? ObservacionDocumentacion { get; set; }
        public string? LevantamientoObservacion { get; set; }
        public bool? ConformidadFinanzas { get; set; }
        public string? ObservacionProgramacionPago { get; set; }
        public int? IdModalidadPago { get; set; }
        public string? NombreModalidadPago { get; set; }
        public int? IdPagoEstado { get; set; }
        public string? NombrePagoEstado { get; set; }
        // Datos del comprobante (via JOIN)
        public string? SerieComprobante { get; set; }
        public string? NumeroComprobante { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? MontoBruto { get; set; }
        public decimal? MontoNeto { get; set; }
        public int? IdProveedor { get; set; }
        [JsonPropertyName("NombreProveedor")]
        public string? RazonSocial { get; set; }
        public int? IdMoneda { get; set; }
        public string? NombreMoneda { get; set; }
        public int? IdEmpresa { get; set; }
        public string? NombreEmpresa { get; set; }
    }

    /// <summary>
    /// DTO para inserción de gestión de pago (cabecera completa con cronograma y archivos)
    /// </summary>
    public class GestionPagoInsertarDTO
    {
        public int IdComprobantePago { get; set; }
        public bool? ServicioValidado { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string? ObservacionDocumentacion { get; set; }
        public string? LevantamientoObservacion { get; set; }
        public bool? ConformidadFinanzas { get; set; }
        public string? ObservacionProgramacionPago { get; set; }
        public int? IdModalidadPago { get; set; }
        public int? IdPagoEstado { get; set; }
        public string? Usuario { get; set; }
        public List<GestionPagoCronogramaInsertarDTO>? Cronograma { get; set; }
        public List<GestionPagoArchivoInsertarDTO>? Archivos { get; set; }
    }

    /// <summary>
    /// DTO para filtrar gestión de pagos
    /// </summary>
    public class FiltroGestionPagoDTO
    {
        public int? IdComprobantePago { get; set; }
        public int? IdProveedor { get; set; }
        public int? IdPagoEstado { get; set; }
        public int? IdModalidadPago { get; set; }
        public int? IdEmpresa { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
    }

    /// <summary>
    /// DTO para el flujo de conformidad de Finanzas
    /// </summary>
    public class GestionPagoConformidadDTO
    {
        public int IdGestionPago { get; set; }
        public bool ConformidadFinanzas { get; set; }
        public string? ObservacionDocumentacion { get; set; }
        public string? ObservacionProgramacionPago { get; set; }
        public int IdPagoEstado { get; set; }
        public string? Usuario { get; set; }
    }

    /// <summary>
    /// DTO para el levantamiento de observaciones por Operaciones
    /// </summary>
    public class GestionPagoLevantamientoDTO
    {
        public int IdGestionPago { get; set; }
        public string? LevantamientoObservacion { get; set; }
        public int IdPagoEstado { get; set; }
        public string? Usuario { get; set; }
    }

    /// <summary>
    /// DTO para actualización de gestión de pago
    /// </summary>
    public class GestionPagoActualizarDTO
    {
        public int IdGestionPago { get; set; }
        public int IdComprobantePago { get; set; }
        public bool? ServicioValidado { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string? ObservacionDocumentacion { get; set; }
        public string? LevantamientoObservacion { get; set; }
        public bool? ConformidadFinanzas { get; set; }
        public string? ObservacionProgramacionPago { get; set; }
        public int? IdModalidadPago { get; set; }
        public int? IdPagoEstado { get; set; }
        public string? Usuario { get; set; }
        public List<GestionPagoCronogramaInsertarDTO>? Cronograma { get; set; }
        public List<GestionPagoArchivoInsertarDTO>? Archivos { get; set; }
    }
}
