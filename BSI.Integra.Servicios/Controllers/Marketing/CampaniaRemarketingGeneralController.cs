using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el resumen del rendimiento para un listado de campanias
        /// </summary>
        /// <returns>Resumen rendimiento campanias</returns>
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

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el listado de combos necesarios para crear y configurara una campaña remarketing general
        /// </summary>
        /// <returns>Listado combos campañas</returns>
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
        /// Obtiene el listado de segmentos en FiltroSegmento para elegirla en campañas remarketing general
        /// </summary>
        /// <returns>Listado de segmentos creados</returns>
        [HttpGet]
        [Route("[action]")]
        public IActionResult ObtenerListadoSegmentosCreados()
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerListadoSegmentosCreados();
                return Ok(listado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult ObtenerResultadosGeneracionTextoPorCampania(int id)
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerResultadosGeneracionTextoPorCampania(id);
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
        /// Guarda y ejecuta una campaña remarketing general de acuerdo a lo programado
        /// </summary>
        /// <returns>Estado de la programacion y/o ejecucion</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult GuardarEjecutarEnvioCampaniaRemarketing(EnvioCampaniaRemarketingDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _campaniaRemarketingGeneralService.GuardarEjecutarEnvioCampaniaRemarketing(request, usuario);
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
        /// Edita y ejecuta una campaña remarketing general de acuerdo a lo programado
        /// </summary>
        /// <returns>Estado de la programacion y/o ejecucion</returns>
        [HttpPost]
        [Route("[action]")]
        public IActionResult EditarEjecutarEnvioCampaniaRemarketing(EnvioCampaniaRemarketingDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = _campaniaRemarketingGeneralService.EditarEjecutarEnvioCampaniaRemarketing(request, usuario);
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

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene la informacion de una campaña remarketing general configurada, para editarla
        /// </summary>
        /// <returns>Datos almacenados de la campania general</returns>
        [HttpGet]
        [Route("[action]/{id}")]
        public IActionResult ObtenerCampaniaRemarketingPorId(int id)
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.ObtenerCampaniaRemarketingPorId(id);
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
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = _campaniaRemarketingGeneralService.EliminarCampania(id, usuario);
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
