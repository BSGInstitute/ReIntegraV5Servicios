using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Marketing
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsVista")]
    public class CampaniaRemarketingGeneralController : ControllerBase
    {
        private readonly ICampaniaRemarketingGeneralService _campaniaRemarketingGeneralService;

        public CampaniaRemarketingGeneralController(ICampaniaRemarketingGeneralService campaniaRemarketingGeneralService)
        {
            this._campaniaRemarketingGeneralService = campaniaRemarketingGeneralService;
        }

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el listado de campañas remarketing general para grilla
        /// </summary>
        /// <returns>Listado campañas</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoCampania()
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerListadoCampania();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ObtenerRendimientoListadoCampanias([FromBody] List<int> ids)
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerRendimientoListadoCampanias(ids);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerCombosConfiguracionCampania()
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerCombosConfiguracionCampania();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el detalle de envios de una campaña remarketing general
        /// </summary>
        /// <returns>Detalles envio</returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult VerDetallesCampania(int id)
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.VerDetallesCampania(id);
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EditarCampania()
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.EditarCampania();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Elimina un campaña remarketing general
        /// </summary>
        /// <returns>Estado eliminacion</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarCampania([FromBody] int id)
        {
            try
            {
                var resultado = _campaniaRemarketingGeneralService.EliminarCampania(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult ObtenerMensajeGeneradoPorId(int id)
        {
            try
            {
                var resultado = _campaniaRemarketingGeneralService.ObtenerMensajeGeneradoPorId(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult ReenviarMensajeGenerado([FromBody] int id)
        {
            try
            {
                var resultado = _campaniaRemarketingGeneralService.ReenviarMensajeGenerado(id);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
