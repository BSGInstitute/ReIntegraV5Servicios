using BSI.Integra.Aplicacion.Marketing.Service.Implementacion.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Marketing.Service.Interface.Marketing.WhatsApp;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Aplicacion.Servicios.SCode.Service.Implementacion.Wavix;
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
        public WavixController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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
                var wavixService = new WavixService(unitOfWork);
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
                var wavixService = new WavixService(unitOfWork);
                var resultado = wavixService.GetNumberByUser(idPersonal);
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
                var wavixService = new WavixService(unitOfWork);
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
                var wavixService = new WavixService(unitOfWork);
                var  whatsAppMensajesService = new WhatsAppMensajesService(unitOfWork);
                var resultado = wavixService.ObtenerEstadoUltimaLlamada(idPersonal, idOportunidad, idActividadDetalle, idAlumno, nroIntentoLlamada);

                whatsAppMensajesService.WavixNotificacionesMensaje(idPersonal, resultado);
                return Ok();
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
        /// Obtiene  configuracion Wavix 
        /// </summary>
        /// <returns> List<WavixPersonalDTO> </returns>
        [Route("[action]")]
        [HttpGet]
        public ActionResult GetConfigurationTrunks()
        {
            try
            {
                var wavixService = new WavixService(unitOfWork);
                var resultado = wavixService.GetConfigurationTrunks();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }






    }
}
