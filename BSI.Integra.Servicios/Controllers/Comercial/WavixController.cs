using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.Comercial.Service.Interface;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using Microsoft.AspNetCore.Cors;
using BSI.Integra.Repositorio.UnitOfWork;
namespace BSI.Integra.Servicios.Controllers.Comercial
{
    [Route("api/Comercial/Wavix")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WavixController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        //
        public WavixController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("ProcesarEstadoLlamada")]
        public IActionResult ProcesarEstadoLlamada([FromBody] LlamadaWavixWebHookDTO request)
        {
            try
            {
                var servicio = new LlamadaWavixService(unitOfWork);
                var respuesta = servicio.GuardarLlamadaWebhook(request);
                if (respuesta == true)
                {
                    return Ok(new { Message = "Datos guardados con éxito." });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        Message = "Ocurrió un error al procesar la solicitud.",
                        Error = "Falla al guardar el dato"
                    });
                }
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error al procesar la solicitud.",
                    Error = ex.Message
                });
            }
        }
        /// <summary>
        /// Endpoint para recibir el webhook de llamadas entrantes del freelancer
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("EstadoLlamadaWavix")]
        public IActionResult EstadoLlamadaWavix([FromBody] LlamadaWavixEntranteDTO request)
        {
            try
            {
                var servicio = new LlamadaWavixService(unitOfWork);
                var respuesta = servicio.GuardarLlamadaEntranteWebhook(request);
                if (respuesta == true)
                {
                    return Ok(new { Message = "Datos guardados con éxito." });
                }
                else
                {
                    return StatusCode(500, new
                    {
                        Message = "Ocurrió un error al procesar la solicitud.",
                        Error = "Falla al guardar el dato"
                    });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error al procesar la solicitud.",
                    Error = ex.Message
                });
            }
        }
    }
}
