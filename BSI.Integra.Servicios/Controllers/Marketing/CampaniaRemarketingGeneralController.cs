using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface;
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

        /// Tipo Función: GET
        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene detalles del estado de ejecucion para una llamada, ademas de los mensajes generados hasta el momento
        /// </summary>
        /// <param name="idLlamadaIA">ID de la llamada a la IA para obtener mensajes</param>
        /// <returns>Detalles de estado ejecucion y listado de mensajes generados</returns>
        [HttpGet]
        [Route("[action]/{idLlamadaIA}")]
        public async Task<ActionResult> ObtenerResultadosGeneracionTextoPorCampania(string idLlamadaIA)
        {
            try
            {
                var listado = await _campaniaRemarketingGeneralService.ObtenerResultadosGeneracionTextoPorCampania(idLlamadaIA);
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
        /// Actualiza y ejecuta una campaña remarketing general de acuerdo a lo programado
        /// </summary>
        /// <returns>Estado de la programacion y/o ejecucion</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ActualizarEjecutarEnvioCampaniaRemarketing([FromBody] ConfiguracionCampaniaRemarketingDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var listado = await _campaniaRemarketingGeneralService.ActualizarEjecutarEnvioCampaniaRemarketing(request, usuario);
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
        [Route("[action]/{idCampania}")]
        public IActionResult VerDetallesCampania(int idCampania)
        {
            try
            {
                var listado = _campaniaRemarketingGeneralService.VerDetallesCampania(idCampania);
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

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Inicia la generacion de mensajes y guarda al configuracion inicial
        /// </summary>
        /// <returns>Estado de ejecucion</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> GenerarListadoTextosRemarketing(ConfiguracionCampaniaRemarketingDTO request)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;
                var _respuestaCorrecta = ValidacionClaim.ValidarClaimFechaExpiracion(claimsIdentity);
                var usuario = _respuestaCorrecta.RegistroClaimToken.UserName;

                var resultado = await _campaniaRemarketingGeneralService.GenerarListadoTextosRemarketing(request, usuario);
                return Ok(new { resultado });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Obtiene los mensajes generados para una alumno y llamada especifica
        /// </summary>
        /// <returns>Estado de ejecucion</returns>
        [HttpGet]
        [Route("[action]/{identificadorLlamadaIA}/{idAlumno}")]
        public async Task<IActionResult> ObtenerMensajeGeneradoPorId(string identificadorLlamadaIA, int idAlumno)
        {
            try
            {
                var resultado = await _campaniaRemarketingGeneralService.ObtenerMensajeGeneradoPorId(identificadorLlamadaIA, idAlumno);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 09/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Reenvia un mensaje ya generado a un alumno especifico
        /// </summary>
        /// <returns>Estado de ejecucion</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> ReenviarMensajeGenerado(ReenviarMensajeRequest request)
        {
            try
            {
                var resultado = await _campaniaRemarketingGeneralService.ReenviarMensajeGenerado(request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// Tipo Función: POST
        /// Autor: Humberto Oscata
        /// Fecha: 13/02/2026
        /// Versión: 1.0
        /// <summary>
        /// Endpoint invocado por el worker para ejecutar campañas programadas cuya fecha coincide con el minuto actual
        /// </summary>
        /// <returns>Estado de ejecución</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EjecutarCampaniasProgramadas()
        {
            try
            {
                await _campaniaRemarketingGeneralService.EjecutarCampaniasProgramadas();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
