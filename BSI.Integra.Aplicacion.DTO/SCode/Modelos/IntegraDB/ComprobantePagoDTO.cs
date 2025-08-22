namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ComprobantePagoDTO
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string Ruc { get; set; }
        public string Proveedor { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public int IdSunatDocumento { get; set; }
        public string SunatDocumento { get; set; }
        public string Comprobante { get; set; }
        public decimal MontoBruto { get; set; }
        public int IdMoneda { get; set; }
        public DateTime FechaEmision { get; set; }
    }


    public class ComprobantePagoInsercionDTO
    {
        public int Id { get; set; }
        public int IdSunatDocumento { get; set; }
        public int IdPais { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdCiudad { get; set; }
        public int? IdProveedor { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoBruto { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? PorcentajeIgv { get; set; }
        public decimal? MontoIgv { get; set; }
        public decimal AjusteMontoBruto { get; set; }
        public decimal MontoNeto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public string Usuario { get; set; }
    }

    public class SunatDocumentoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

    }

    public class ComprobantesNoAsociadosDTO
    {
        public int Id { get; set; }
        public int? IdSede { get; set; }
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int idSunatDocumento { get; set; }
        public string NombreDocumento { get; set; }
        public string Serie { get; set; }
        public string NroComprobante { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int IdMoneda { get; set; }
        public string Moneda { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? MontoBruto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public decimal? MontoNeto { get; set; }
        public decimal? AjusteMontoBruto { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public decimal? MontoIgv { get; set; }
        public int? IdRetencion { get; set; }
        public decimal? valorRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public decimal? valorDetraccion { get; set; }
    }

    public class ComprobantePagoAsociadoDTO
    {
        public int Id { get; set; }
        public int? IdDocumentoPago { get; set; }
        public string NombreDocumento { get; set; }
        public string SerieComprobante { get; set; }
        public string NumeroComprobante { get; set; }
        public decimal Monto { get; set; }
        public int IdMoneda { get; set; }
        public string NombreMoneda { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime FechaProgramacion { get; set; }
        public int IdPais { get; set; }
        public string NombrePais { get; set; }
        public int? IdIgv { get; set; }
        public decimal? ValorIGV { get; set; }
        public int? IdRetencion { get; set; }
        public decimal? ValorRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public decimal? ValorDetraccion { get; set; }
        public int? IdProveedor { get; set; }
        public string NombreProveedor { get; set; }
        public string CodigoFur { get; set; }
        public decimal MontoAfecto { get; set; }
        public decimal? MontoInafecto { get; set; }
        public decimal? OtraTazaContribucion { get; set; }
        public decimal AjusteMontoBruto { get; set; }

    }

    public class RucSerieNumeroComprobanteDTO
    {
        public int Id { get; set; }
        public string Comprobante { get; set; }
        public decimal MontoNeto { get; set; }
        public int IdMoneda { get; set; }
        public int? IdTipoImpuesto { get; set; }
        public int? IdRetencion { get; set; }
        public int? IdDetraccion { get; set; }
        public int? IdPais { get; set; }
    }

    public class ComprobanteMontoUtilizadoDTO
    {
        public int IdComprobantePago { get; set; }
        public decimal MontoUtilizado { get; set; }
    }
}
