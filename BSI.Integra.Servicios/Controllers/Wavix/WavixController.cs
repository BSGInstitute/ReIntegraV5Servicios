using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.SCode.Service.Implementacion.Wavix;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wavix;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Configurations;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers.Wavix
{
    [Route("api/Wavix")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WavixController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public WavixController(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            this.unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }


        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  configuracion Wavix por Asesor
        /// </summary>
        /// <returns> List<WavixPersonalDTO> </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult GetUserAccess(int idPersonal)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.GetUserAccess(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque
        /// Fecha: 02/02/2023
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  configuracion Wavix por Asesor
        /// </summary>
        /// <returns> List<WavixPersonalDTO> </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult GetNumberByUser(int idPersonal)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.GetNumberByUser (idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 22/22/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  estado de la llamada wavix
        /// </summary>
        /// <returns> List<WavixPersonalDTO> </returns>
        [Route("[action]/{idPersonal}/{idOportunidad}/{idActividadDetalle}/{idAlumno}/{nroIntentoLlamada}")]
        [HttpGet]
        public ActionResult ObtenerEstadoLlamadaWavix(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork,httpClient);
                var resultado = wavixService.ObtenerEstadoUltimaLlamada(idPersonal, idOportunidad, idActividadDetalle, idAlumno, nroIntentoLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        /// Tipo Función: GET
        /// Autor: Carlos Crispin
        /// Fecha: 22/22/2024
        /// Versión: 1.0
        /// <summary>
        /// Obtiene  estado de la llamada wavix y la envia por signalr
        /// </summary>
        /// <returns> List<WavixPersonalDTO> </returns>
        [Route("[action]/{idPersonal}/{idOportunidad}/{idActividadDetalle}/{idAlumno}/{nroIntentoLlamada}")]
        [HttpGet]
        public ActionResult ObtenerEstadoLlamadaWavixSignal(int idPersonal, int idOportunidad, int idActividadDetalle, int idAlumno, int nroIntentoLlamada)
        {

            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var resultado = wavixService.ObtenerEstadoUltimaLlamada(idPersonal, idOportunidad, idActividadDetalle, idAlumno, nroIntentoLlamada);

                whatsAppMensajesService.WavixNotificacionesMensaje(idPersonal, resultado);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Lista los SIP trunks desde la API de Wavix
        /// </summary>
        /// <returns> SipTrunkListResponseDTO </returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> ListarSipTrunks([FromQuery] string apiKey, [FromQuery] int? page = null, [FromQuery] int? perPage = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.ListarSipTrunks(apiKey, page, perPage);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la configuración de un SIP trunk específico por su ID
        /// </summary>
        /// <returns> SipTrunkConfigDTO </returns>
        [Route("[action]/{idSipTrunk}")]
        [HttpGet]
        public async Task<ActionResult> ObtenerSipTrunkPorId([FromQuery] string idSipTrunk, [FromQuery] string apiKey)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.ObtenerSipTrunkPorId(apiKey, idSipTrunk);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Genera un token para el widget embebible de Wavix
        /// </summary>
        /// <returns> GenerarTokenWidgetResponseDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> GenerarTokenWidget([FromQuery] string apiKey, [FromBody] GenerarTokenWidgetRequestDTO request)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.GenerarTokenWidget(apiKey, request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la configuración completa de Wavix para un personal (config + token)
        /// Este endpoint unifica: obtener config personal, obtener SIP trunk y generar token
        /// </summary>
        /// <returns> ConfiguracionCompletaWavixDTO </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public async Task<ActionResult> ObtenerConfiguracionCompletaWavix([FromRoute] int idPersonal)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.ObtenerConfiguracionCompletaWavix(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult ObtenerTokenActivo([FromRoute] int idPersonal)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.ObtenerTokenActivo(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        #region Endpoints para Wavix Embeddable v2.0.0

        /// <summary>
        /// Obtiene la lista de SIP trunks disponibles
        /// Alias de ListarSipTrunks para compatibilidad con el frontend
        /// </summary>
        /// <returns>SipTrunkListResponseDTO</returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult> GetSipTrunks([FromQuery] string apiKey, [FromQuery] int? page = null, [FromQuery] int? perPage = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.ListarSipTrunks(apiKey, page, perPage);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Genera un token para el widget embebible de Wavix
        /// Alias de GenerarTokenWidget para compatibilidad con el frontend
        /// </summary>
        /// <returns>GenerarTokenWidgetResponseDTO</returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ActionResult> GenerateWidgetToken([FromQuery] string apiKey, [FromBody] GenerarTokenWidgetRequestDTO request)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.GenerarTokenWidget(apiKey, request);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene la lista de tokens activos de un personal
        /// </summary>
        /// <param name="idPersonal">ID del personal</param>
        /// <returns>List<TokenActivoListDTO></returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult GetActiveTokens([FromRoute] int idPersonal)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.ObtenerTokensActivos(idPersonal);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtiene un token específico por su UUID
        /// </summary>
        /// <param name="tokenUuid">UUID del token</param>
        /// <returns>TokenActivoListDTO</returns>
        [Route("[action]/{tokenUuid}")]
        [HttpGet]
        public ActionResult GetToken([FromRoute] string tokenUuid)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.ObtenerTokenPorUuid(tokenUuid);

                if (resultado == null)
                {
                    return NotFound(new { mensaje = "Token no encontrado" });
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Actualiza el payload de un token en la API de Wavix
        /// </summary>
        /// <param name="tokenUuid">UUID del token a actualizar</param>
        /// <param name="apiKey">API Key para autenticación</param>
        /// <param name="request">Nuevo payload</param>
        /// <returns>TokenOperacionResponseDTO</returns>
        [Route("[action]/{tokenUuid}")]
        [HttpPut]
        public async Task<ActionResult> UpdateTokenPayload(
            [FromRoute] string tokenUuid,
            [FromQuery] string apiKey,
            [FromBody] ActualizarTokenPayloadRequestDTO request)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = await wavixService.ActualizarTokenPayload(apiKey, tokenUuid, request.Payload);

                if (!resultado.Exito)
                {
                    return BadRequest(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Invalida (elimina lógicamente) un token por su UUID
        /// </summary>
        /// <param name="tokenUuid">UUID del token a eliminar</param>
        /// <param name="usuario">Usuario que realiza la operación (opcional)</param>
        /// <returns>TokenOperacionResponseDTO</returns>
        [Route("[action]/{tokenUuid}")]
        [HttpDelete]
        public ActionResult DeleteToken([FromRoute] string tokenUuid, [FromQuery] string usuario = "SYSTEM")
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient();
                var wavixService = new WavixService(unitOfWork, httpClient);
                var resultado = wavixService.InvalidarToken(tokenUuid, usuario);

                if (!resultado.Exito)
                {
                    return NotFound(resultado);
                }

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        ///// Tipo Función: GET
        ///// Autor: Joseph Llanque
        ///// Fecha: 02/02/2023
        ///// Versión: 1.0
        ///// <summary>
        ///// Obtiene  configuracion Wavix 
        ///// </summary>
        ///// <returns> List<WavixPersonalDTO> </returns>
        //[Route("[action]")]
        //[HttpGet]
        //public ActionResult GetConfigurationTrunks()
        //{
        //    try
        //    {
        //        var wavixService = new WavixService(unitOfWork);
        //        var resultado = wavixService.GetConfigurationTrunks();
        //        return Ok(resultado);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}






    }
}
