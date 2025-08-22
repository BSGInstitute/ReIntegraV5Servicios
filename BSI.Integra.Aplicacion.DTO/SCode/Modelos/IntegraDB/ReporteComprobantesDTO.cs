namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ReporteComprobantesDTO
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Sede { get; set; }
        public string Area { get; set; }
        public string TipoPedido { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDoc { get; set; }
        public string Proveedor { get; set; }
        public string TipoComprobante { get; set; }
        public string NumComprobante { get; set; }
        public string MonedaComprobante { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime? FechaEmision { get; set; }
        public string MesFechaEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string MesFechaProgramacion { get; set; }
        public string CodigoFur { get; set; }
        public decimal? MontoAsociado { get; set; }
        public decimal? MontoPendiente { get; set; }
    }

}
