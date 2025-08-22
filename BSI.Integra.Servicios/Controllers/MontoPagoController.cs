using BSI.Integra.Aplicacion.Transversal.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: MontoPagoController
    /// Autor: Erick Marcelo Quispe.
    /// Fecha: 27/07/2022
    /// <summary>
    /// Gestión de MontoPago
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class MontoPagoController : ControllerBase
    {
        IMontoPagoService _montoPagoService;
        ITokenManager _tokenManager;
        public MontoPagoController(IUnitOfWork unitOfWork, ITokenManager tokenManager)
        {
            _montoPagoService = new MontoPagoService(unitOfWork);
            _tokenManager = tokenManager;
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 04/01/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene todos los Paquetes del centro costo
        /// </summary>
        /// <param name="idCentroCosto"></param>
        /// <returns></returns>
        [Route("[Action]/{idCentroCosto}")]
        [HttpGet]
        public ActionResult ObtenerPaquetes(int idCentroCosto)
        {
            try
            {
                var resultado = _montoPagoService.ObtenerPaquetesIdCentroCosto(idCentroCosto);
                return Ok(resultado);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 06/06/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle monto pago
        /// </summary>
        /// <param name="idPrograma"></param>
        /// <param name="idCategoria"></param>
        /// <returns></returns>
        [Route("[action]/{idPrograma}/{idCategoria}")]
        [HttpGet]
        public IActionResult ObtenerPgeneralMontoPagoDetalle(int idPrograma, int idCategoria)
        {
            return Ok(_montoPagoService.ObtenerPgeneralMontoPagoDetalle(idPrograma, idCategoria));
        }
        /// Tipo Función: GET
        /// Autor: Gretel Canasa
        /// Fecha: 06/07/2023
        /// Versión: 1.0
        /// <summary>
        [Route("[action]")]
        [HttpGet]
        public ActionResult ObtenerProgramasMontoPagoPanel()
        {
            return Ok(_montoPagoService.ListarProgramaGeneralParaMontoPago());
        }
        /// Tipo Función: DELETE
        /// Autor: Flavio R. Mamani Fabian
        /// Fecha: 03/08/2023
        /// Versión: 1.0
        /// <summary>
        /// Elimina el monto pago
        /// </summary>
        /// <param name="idMontoPago"></param>
        /// <returns>Estado eliminacion</returns>
        [Authorize]
        [JwtExpirationValidation]
        [Route("[action]/{idMontoPago}")]
        [HttpDelete]
        public ActionResult EliminarMontoPago(int idMontoPago)
        {
            return Ok(_montoPagoService.EliminarMontoPago(idMontoPago, _tokenManager.UserName));
        }
        /// Tipo Función: GET
        /// Autor: Edmundo Llaza
        /// Fecha: 01/09/2023
        /// Versión: 1.0
        /// <summary>
        /// Retorna combo montos pago
        /// </summary>
        /// <returns></returns>
        [Route("ObtenerCombosModuloAsync")]
        [HttpGet]
        public async Task<IActionResult> ObtenerCombosModuloAsync()
        {
            return Ok(await _montoPagoService.ObtenerCombosModuloAsync());
        }
    }
}
