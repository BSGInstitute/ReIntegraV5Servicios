using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using Google.Api.Ads.AdWords.v201809;

namespace BSI.Integra.Repositorio.Repository.Interface.Finanzas.Siigo
{
    public interface ISiigoApiRepository
    {
        StringDTO ObtenerToken();
        Task<bool> ActualizaTokenSiigo(string token, DateTime fechaExpiracion, string usuarioModificacion);
        // Task GuardarFacturaSiigoInterna(DatosCompletosDTO datos, string codigoMatricula, string usuario);
       // Task<string> GuardarFacturaSiigoInterna(DatosCompletosDTO datos, string codigoMatricula, string usuario);
        //public DatosCompletosDTO ObtenerDatosFacturaSiigoPorId(int idFactura);
        public DatosCompletosDTO ObtenerDatosFacturaClientePorId(int idFactura);
        public int ObtenerIdFacturaPorCodigoMatricula(int IdCronogramaPagoDetalleFinal);
        Task<string> GuardarFacturaSiigoInterna(DatosCompletosDTO datos, string codigoMatricula, int idCronogramaPagoDetalleFinal, string usuario);
        public List<SiigoFacturaMasivoDTO> ObtenerFacturasPendientesEnvioSiigo();
        public void ActualizarFacturaComoEnviada(int idFactura, string usuario);
        public int ObtenerIdCronogramaPorIdFactura(int idFactura);







        }
}
