using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Linkedin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi
{
    public class DatosCompletosDTO
    {
        public CrearFacturaDeVentaSiigoDTO Factura { get; set; }
        public CrearClienteSiigoDTO Cliente { get; set; }
    }

    public class CrearFacturaDeVentaSiigoDTO
    {
        public DocumentoDTO Documento { get; set; }
        public string Fecha { get; set; }
        public ClienteDTO Cliente { get; set; }
        public int Vendedor { get; set; }
        public string Observaciones { get; set; }
        public List<ItemDTO> Items { get; set; }
        public List<PagoDTO> Pagos { get; set; }
    }

    public class DocumentoDTO
    {
        public int Id { get; set; }
    }

    public class ClienteDTO
    {
        public string NumeroIdentification { get; set; }
    }

    public class ItemDTO
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class PagoDTO
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string FechaVencimiento { get; set; }
    }

    public class CrearClienteSiigoDTO
    {
        public string TipoCliente { get; set; }
        public string TipoPersona { get; set; }
        public string TipoIdentificacion { get; set; }
        public string Identificacion { get; set; }
        public string[] Nombres { get; set; }
        public string[] CodigosFiscal { get; set; }
        public string Direccion { get; set; }
        public string CodigoPais { get; set; }
        public string CodigoDepartamento { get; set; }
        public string CodigoCiudad { get; set; }
        public string TelefonoIndicativo { get; set; }
        public string TelefonoNumero { get; set; }
        public string TelefonoExtension { get; set; }
        public string ContactoNombre { get; set; }
        public string ContactoApellido { get; set; }
        public string ContactoEmail { get; set; }
    }

    public class EnvioMasivoSiigoLoteDTO
    {
        public List<int> IdsFacturas { get; set; }
        public string Usuario { get; set; }
    }
    public class EnvioSiigoDTO
    {
        public int IdFactura { get; set; }
        public string Usuario { get; set; }
    }
    public class SiigoFacturaMasivoDTO
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

}