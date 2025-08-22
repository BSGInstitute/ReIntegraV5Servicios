using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using System.Net;

namespace BSI.Integra.Aplicacion.Finanzas.Service.Interface.Finanzas.Siigo
{
    public interface ISiigoApiService
    {
        Task<(string resultado, HttpStatusCode statusCode)> DatosCompletos(DatosCompletosDTO datos, string usuarioModificacion);
        Task<(string resultado, HttpStatusCode statusCode)> CrearClienteSiigo(CrearClienteSiigoDTO cliente);
        Task<(string resultado, HttpStatusCode statusCode)> CrearFacturaDeVentaSiigo(CrearFacturaDeVentaSiigoDTO datos);
        Task<bool> ActualizaTokenSiigo(string token, DateTime fechaExpiracion, string usuarioModificacion);
       // Task GuardarDatosAntesDeEnviarASiigo(DatosCompletosDTO datos, string codigoMatricula, string usuario);
        Task EnviarSiigoMasivasDesdeBaseDeDatos(EnvioMasivoSiigoLoteDTO datos);
        public int ObtenerIdFacturaPorCodigoMatricula(int IdCronogramaPagoDetalleFinal);
         Task EnviarFacturaSiigoDesdeBaseDeDatos(int idFactura, string usuario);
        Task GuardarDatosAntesDeEnviarASiigo(DatosCompletosDTO datos, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario);

        public List<SiigoFacturaMasivoDTO> ObtenerFacturasPendientesEnvioSiigo();



    }
}
