using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Marketing.WhatsApp
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]

    public class WhatsAppMasivoController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public WhatsAppMasivoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public void AsignarCantidadDeDataAPersonal()
        {

        }
        [Route("idprioridad")]
        [HttpGet]
        public IActionResult ObtenerTotalDeDataPorPrioridad(int idprioridad)
        {
            return Ok();
        }

        /// Autor: develop-mvaldiviac
        /// Fecha: 2026-05-05
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el historial de oportunidades de un alumno
        /// para ser usado en la calificacion IA V2.
        /// GET /api/WhatsAppMasivo/ObtenerHistorialOportunidades/{idAlumno}
        /// </summary>
        [HttpGet("ObtenerHistorialOportunidades/{idAlumno}")]
        public IActionResult ObtenerHistorialOportunidades(int idAlumno)
        {
            try
            {
                var servicio = new WhatsAppEnvioMasivoService(unitOfWork);
                var resultado = servicio.ObtenerHistorialOportunidadesPorAlumno(idAlumno);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: develop-mvaldiviac
        /// Fecha: 2026-05-05
        /// Versión: 1.0
        /// <summary>
        /// Inicia una calificacion batch V2 con perfil_lead e historial_oportunidades (en snake_case + texto).
        /// POST /api/WhatsAppMasivo/IniciarCalificacionBatchV2
        /// </summary>
        [HttpPost("IniciarCalificacionBatchV2")]
        public async Task<ActionResult> IniciarCalificacionBatchV2([FromBody] CalificacionBatchV2RequestDTO request)
        {
            try
            {
                var servicio = new WhatsAppEnvioMasivoService(unitOfWork);
                var resultado = await servicio.IniciarCalificacionBatchV2(request);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: develop-mvaldiviac
        /// Fecha: 2026-05-05
        /// Versión: 1.0
        /// <summary>
        /// Consulta el estado de una calificacion batch V2 por su llamadaId.
        /// GET /api/WhatsAppMasivo/ObtenerEstadoCalificacionV2/{llamadaId}
        /// </summary>
        [HttpGet("ObtenerEstadoCalificacionV2/{llamadaId}")]
        public async Task<ActionResult> ObtenerEstadoCalificacionV2(string llamadaId)
        {
            try
            {
                var servicio = new WhatsAppEnvioMasivoService(unitOfWork);
                var resultado = await servicio.ObtenerEstadoCalificacionV2(llamadaId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// Autor: develop-mvaldiviac
        /// Fecha: 2026-05-05
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los resultados de una calificacion batch V2 por su llamadaId.
        /// GET /api/WhatsAppMasivo/ObtenerResultadosCalificacionV2/{llamadaId}
        /// </summary>
        [HttpGet("ObtenerResultadosCalificacionV2/{llamadaId}")]
        public async Task<ActionResult> ObtenerResultadosCalificacionV2(string llamadaId)
        {
            try
            {
                var servicio = new WhatsAppEnvioMasivoService(unitOfWork);
                var resultado = await servicio.ObtenerResultadosCalificacionV2(llamadaId);
                return Content(resultado, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
