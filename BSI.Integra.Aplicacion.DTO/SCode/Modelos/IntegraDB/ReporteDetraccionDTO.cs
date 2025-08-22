namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteDetraccionDTO
    {
        public string Sede { get; set; } = null!;
        public string NroDocIdentidad { get; set; } = null!;
        public string NombreProveedor { get; set; } = null!;
        public string NumeroComprobante { get; set; } = null!;
        public string NombreMoneda { get; set; } = null!;
        public decimal MontoBruto { get; set; }
        public decimal MontoIgv { get; set; }
        public decimal MontoNeto { get; set; }
        public int? PorcentajeDetraccion { get; set; } = null!;
        public decimal MontoDetraccion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public DateTime PeriodoTributario { get; set; }
    }
    public class DetraccionComboDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdPais { get; set; }
        public decimal Valor { get; set; }
    }
    public class ReporteDetraccionFiltroDTO
    {
        public string? IdSede { get; set; } 
        public string? FechaInicio { get; set; } 
        public string? FechaFinal { get; set; } 
        public int? IdProveedor { get; set; } 
    }
}
