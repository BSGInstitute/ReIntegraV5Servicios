using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades;
using System.Net;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface
{
    public interface IFacturamaService
    {
        Task<(string resultado, HttpStatusCode statusCode)> CrearFacturaAsync(FacturamaFacturaDTO factura);

        Task<(string resultado, HttpStatusCode statusCode)> CrearClienteAsync(FacturamaClienteDTO cliente);
        Task<(string resultado, HttpStatusCode statusCode)> DatosCompletosFacturama(FacturamaFacturaClienteDTO datos);
        Task<(string resultado, HttpStatusCode statusCode)> BuscarClienteAsync(string keyword);

        List<RegimenFiscalDTO> ObtenerListaRegimenFiscal();
        List<UsoCfdiDTO> ObtenerListaUsoCfdi();
        List<FormapagoFacturamaDTO> ObtenerFormapagoFacturama();
        public bool ActualizaEnviadoFacturama(int id, String UsuarioModificacion);
        public string InsertarRegimenFiscal(string clave, string descripcion, string usuario);
        public bool ActualizarRegimenFiscal(int id, string clave, string descripcion, string usuario);
        public bool EliminarRegimenFiscal(int id, string usuario);
        public string InsertarUsoComprobante(string clave, string descripcion, string usuario);
        public bool ActualizarUsoComprobante(int id, string clave, string descripcion, string usuario);
        public bool EliminarUsoComprobante(int id, string usuario);
        public List<ResumenMatriculaDTO> ObtenerResumenMatriculas(FiltroFechaDTO filtro);
      
        //Task<int> GuardarFacturaClienteCompleta(FacturamaFacturaClienteDTO dto, string codigoMatricula, string usuario);
        public FacturamaFacturaClienteCronogrmaDTO ObtenerFacturaClientePorCodigoMatricula(string codigoMatricula);
        public int ObtenerIdFacturaPorCodigoMatricula(string codigoMatricula);
        Task<(string resultado, HttpStatusCode statusCode)> EnviarFacturaPorCorreoAsync(string cfdiId, string emailCliente);
        public List<FacturamaFacturaMasivoDTO> ObtenerFacturasPendientesEnvio();
        Task EnviarFacturasMasivasDesdeBaseDeDatos(EnvioMasivoLoteDTO datos);
        Task<int> GuardarFacturaClienteCompleta(FacturamaFacturaClienteDTO dto, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario);
        public bool ExisteFacturaConfigurada(int idCronogramaPagoDetalleFinal);




   }
}
