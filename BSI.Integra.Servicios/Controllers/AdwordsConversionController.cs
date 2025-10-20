using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// <summary>
    /// Controlador: AdwordsConversionController
    /// Autor: Miguel
    /// Fecha: 2025-10-04
    /// Descripción: API para gestionar conversiones offline de Google Ads
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class AdwordsConversionController : ControllerBase
    {
        private readonly IAdwordsConversionService _service;

        public AdwordsConversionController(IAdwordsConversionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Envía las conversiones pendientes a Google Ads
        /// </summary>
        /// <returns>Resultado del proceso de envío</returns>
        /// <response code="200">Proceso completado exitosamente</response>
        /// <response code="500">Error interno del servidor</response>
        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> EnviarConversiones()
        {
            try
            {
                var resultado = await _service.EnviarConversionesPendientes();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al enviar conversiones",
                    error = ex.Message,
                    detalles = ex.InnerException?.Message
                });
            }
        }

        /// <summary>
        /// Obtiene el estado actual de las conversiones en la cola
        /// </summary>
        /// <returns>Lista de estados con cantidades</returns>
        /// <response code="200">Estados obtenidos exitosamente</response>
        /// <response code="500">Error interno del servidor</response>
        [Route("[action]")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> ObtenerEstado()
        {
            try
            {
                var estados = await _service.ObtenerEstadoConversiones();
                return Ok(estados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener estado",
                    error = ex.Message
                });
            }
        }
    }
}
