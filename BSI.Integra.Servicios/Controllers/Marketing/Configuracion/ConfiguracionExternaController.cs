using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BSI.Integra.Servicios.Controllers.Marketing.Configuracion
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class ConfiguracionExternaController : ControllerBase
    {
        private readonly IConfiguracionExternaService _configuracionExternaService;

        public ConfiguracionExternaController(IConfiguracionExternaService configuracionExternaService)
        {
            _configuracionExternaService = configuracionExternaService;
        }

        /// <summary>
        /// Toma los datos del esquema desde BD, construye el JSON de interaccion
        /// y lo envía via PATCH a la API externa del asistente WhatsApp.
        /// </summary>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> SincronizarEsquemaInteraccion([FromBody] SincronizarEsquemaRequestDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _configuracionExternaService.SincronizarEsquemaInteraccionAsync(request.IdChatbotEsquema);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
