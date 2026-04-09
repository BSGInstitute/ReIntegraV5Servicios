namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para lectura de cuota del cronograma de pago
    /// </summary>
    public class GestionPagoCronogramaDTO
    {
        public int Id { get; set; }
        public int IdGestionPago { get; set; }
        public int NumeroCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaProbablePago { get; set; }
        public DateTime? FechaRealPago { get; set; }
        public List<GestionPagoArchivoDTO>? Archivos { get; set; }
    }

    /// <summary>
    /// DTO para inserción de cuota en el cronograma
    /// </summary>
    public class GestionPagoCronogramaInsertarDTO
    {
        public int? Id { get; set; }
        public int NumeroCuota { get; set; }
        public decimal MontoCuota { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public DateTime? FechaProbablePago { get; set; }
        public DateTime? FechaRealPago { get; set; }
        public string? Usuario { get; set; }
    }

    /// <summary>
    /// DTO para registrar la fecha real de pago de una cuota
    /// </summary>
    public class GestionPagoCronogramaPagoDTO
    {
        public int IdGestionPagoCronograma { get; set; }
        public DateTime FechaRealPago { get; set; }
        public string? Usuario { get; set; }
    }
}
