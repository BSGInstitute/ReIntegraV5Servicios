
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{


    public class FacturamaFacturaClienteDTO
    {
        public FacturamaFacturaDTO factura { get; set; }
        public FacturamaClienteDTO cliente { get; set; }
    
    }


    public class FacturamaFacturaDTO
    {
        public string CfdiType { get; set; }
        public string Currency { get; set; }
        public string PaymentForm { get; set; }
        public string PaymentMethod { get; set; }
        public string ExpeditionPlace { get; set; }
        public GlobalInformationDTO? GlobalInformation { get; set; }
        public FacturamaReceiverDTO? Receiver { get; set; }
        public List<FacturamaItemDTO>? Items { get; set; }
    }


    public class FacturamaReceiverDTO
    {
        public string Rfc { get; set; }
        public string CfdiUse { get; set; }
        public string Name { get; set; }
        public string FiscalRegime { get; set; }
        public string TaxZipCode { get; set; }
    }

    public class FacturamaItemDTO
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string UnitCode { get; set; }
        public double? Quantity { get; set; }
        public string? Unit { get; set; }
        public double? UnitPrice { get; set; }
        public double? Subtotal { get; set; }
        public string? TaxObject { get; set; }
        public List<FacturamaTaxDTO> Taxes { get; set; }  // Lista de impuestos aplicables
        public double? Total { get; set; }
    }

    public class FacturamaTaxDTO
    {
        public string Name { get; set; }
        public double? Base { get; set; }
        public double? Rate { get; set; }
        public bool? IsRetention { get; set; }
        public double? Total { get; set; }
    }




    public class IdTemporalDTO
    {
        public int Id { get; set; }
    }

    public class FacturamaClienteDTO
    {
        public string Rfc { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string FiscalRegime { get; set; }
        public string CfdiUse { get; set; }
        public FacturamaAddressDTO Address { get; set; }
    }

    public class FacturamaAddressDTO
    {
        public string Street { get; set; }
        public string ExteriorNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string? Country { get; set; } = "MEX";
    }

    public class AddressDTO
    {
        public string Street { get; set; }
        public string ExteriorNumber { get; set; }
        public string InteriorNumber { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string Municipality { get; set; }
        public string State { get; set; }
        public string? Country { get; set; } = "MEX";
    }
    public class GlobalInformationDTO
    {
        public string? Periodicity { get; set; }
        public string? Months { get; set; }
        public string? Year { get; set; }
    }

    public class EnvioApiDTO
    {
        public int idFactura { get; set; }
        public string usuario { get; set; }

    }
    public class FacturaMasivaDTO
    {
        public List<string> listaIdsFactura { get; set; }
        public string Usuario { get; set; }


    }
    public class EnvioMasivoLoteDTO
    {
        public List<int> IdsFacturas { get; set; }

        public string Usuario { get; set; }
    }

    public class FacturamaFacturaMasivoDTO
    {
        public int IdFactura { get; set; }
        public int IdCliente { get; set; }
        public string CodigoMatricula { get; set; }
        public string Nombre { get; set; }
        public bool? EstadoEnvio { get; set; }
        public string Identificador { get; set; }
        public string Pais { get; set; }
        public decimal Monto { get; set; }
        public string ApiDestino { get; set; }
    }
    public class CornogramaFacturmaDTO
    {
        public int IdCronogramaPagoDetalleFinal { get; set; }

    }
    public class FacturamaFacturaClienteCronogrmaDTO
    {
        public FacturamaFacturaDTO factura { get; set; }
        public FacturamaClienteDTO cliente { get; set; }
        public int? IdCronogramaPagoDetalleFinal { get; set; }


    }



    public class FacturamaFacturaCronogramaDetalleDTO
    {
        public int IdFacturamaFactura { get; set; }
        public int IdCronogramaPagoDetalleFinal { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int IdAlumno { get; set; }
        public string NombreCompletoAlumno { get; set; }
        public string DescripcionPago { get; set; }
        public string MontoPago { get; set; }
    }

    public class EliminarFacturasRequest
    {
        public List<int> IdsFacturas { get; set; }
    }
}
