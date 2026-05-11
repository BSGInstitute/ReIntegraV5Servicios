using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Interaccion.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: LlamadasWhatsAppController
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Endpoint de historial y reporting de llamadas de WhatsApp Business Calling.
    /// La señalización en tiempo real (webhook, WebRTC, SignalR) la maneja WebhookWhatsappApi.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class LlamadasWhatsAppController : ControllerBase
    {
        private IUnitOfWork unitOfWork;

        public LlamadasWhatsAppController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// Tipo Función: GET
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-08
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial paginado de llamadas filtrando por país, área, agente, número, tipo, estado y rango de fechas.
        /// </summary>
        /// <param name="filtro">Filtro de búsqueda y paginación</param>
        /// <returns>Lista paginada de llamadas con TotalRegistros para el cliente</returns>
        [HttpGet("Historial")]
        public IActionResult ObtenerHistorial([FromQuery] LlamadasHistorialFiltroDTO filtro)
        {
            try
            {
                var servicio = new LlamadasWhatsAppService(unitOfWork);
                var resultado = servicio.ObtenerHistorial(filtro);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}
