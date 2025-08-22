using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Finanzas.SiigoApi;
using BSI.Integra.Aplicacion.Finanzas.Service.Implementacion.Finanzas.Siigo;
using BSI.Integra.Aplicacion.Finanzas.Service.Interface.Finanzas.Siigo;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace BSI.Integra.Servicios.Controllers.Finanzas.Siigo
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class SiigoApiController : ControllerBase
    {
        private ISiigoApiService _siigoApiService;
        private ITokenManager _tokenManager;

        public SiigoApiController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _siigoApiService = new SiigoApiService(unitOfWork);
            _tokenManager = tokenManager;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DatosCompletos(DatosCompletosDTO datos)
        {
            var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
            var resultado = await _siigoApiService.DatosCompletos(datos, registroClaimToken.UserName);
            return StatusCode((int)resultado.statusCode, resultado.resultado);
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> CrearClienteSiigo(CrearClienteSiigoDTO cliente)
        //{
        //    var resultado = await _siigoApiService.CrearClienteSiigo(cliente);
        //    return StatusCode((int)resultado.statusCode, resultado.resultado);
        //}
 
        [HttpPost("EnviarSiigoMasivasLote")]
        public async Task<IActionResult> EnviarSiigoMasivasLote([FromBody] EnvioMasivoSiigoLoteDTO datos)
        {
            try
            {
                await _siigoApiService.EnviarSiigoMasivasDesdeBaseDeDatos(datos);
                return Ok(new { mensaje = "Facturación masiva completada con éxito." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("GuardarFacturaInternaSiigo")]
        public async Task<IActionResult> GuardarFacturaInternaSiigo1([FromBody] DatosCompletosDTO datos, [FromQuery] string codigoMatricula, [FromQuery] int idCronogramaPagoDetalleFinal, string? userName)
        {

            if (userName != "PortalWeb_Siigo")
            {
                var registroClaim = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                userName = registroClaim.UserName;
            }


            try
            {
                await _siigoApiService.GuardarDatosAntesDeEnviarASiigo(datos, codigoMatricula, idCronogramaPagoDetalleFinal, userName);

                return Ok(new
                {
                    message = "Factura guardada internamente con éxito.",
                    codigoMatricula = codigoMatricula
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error al guardar los datos.",
                    detalle = ex.Message
                });
            }
        }





        [HttpPost("EnviarSiigoFacturaApi")]
        public async Task<IActionResult> EnviarSiigoFacturaApi([FromBody] EnvioSiigoDTO datos)
        {
            try
            {
                await _siigoApiService.EnviarFacturaSiigoDesdeBaseDeDatos(datos.IdFactura, datos.Usuario);
                return Ok(new { mensaje = "Factura enviada correctamente a Facturama." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        [Route("[action]/{idCronogramaPagoDetalleFinal}")]
        [HttpGet]
        public IActionResult ObtenerIdFacturaPorCodigoSiigo(int IdCronogramaPagoDetalleFinal)
        {
            var idFactura = _siigoApiService.ObtenerIdFacturaPorCodigoMatricula(IdCronogramaPagoDetalleFinal);
            return Ok(new { idFactura });
        }


        [HttpGet("ListarPendientesEnvioSiigo")]
        public IActionResult ListarPendientesEnvioSiigo()
        {
            var resultado = _siigoApiService.ObtenerFacturasPendientesEnvioSiigo();
            return Ok(resultado);
        }


    }
}
