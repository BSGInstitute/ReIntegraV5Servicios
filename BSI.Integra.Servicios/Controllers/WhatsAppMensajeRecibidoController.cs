using BSI.Integra.Aplicacion.Marketing.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BSI.Integra.Servicios.Controllers
{
    /// Controlador: WhatsAppMensajeRecibidoController
    /// Autor: Jonathan Caipo
    /// Fecha: 18/10/2022
    /// <summary>
    /// Gestión de los Mensajes de WhatsApp Recibido
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class WhatsAppMensajeRecibidoController : ControllerBase
    {
        private IUnitOfWork unitOfWork;
        public WhatsAppMensajeRecibidoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// TipoFuncion: GET
        /// Autor: Jonathan Caipo
        /// Fecha: 18/10/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene últimos mensajes recibido de chat de cada alumno por IdPersonal y validación de Mensaje Ofensivo
        /// </summary>
        /// <returns> objetoDTO: List<WhatsAppMensajesControlOfensivoDTO>  </returns>
        [Route("[action]/{idPersonal}")]
        [HttpGet]
        public ActionResult WhatsAppUltimoMensajeRecibidoChatControlMensaje(int idPersonal)
        {
            if (idPersonal != null)
            {
                try
                {
                    WhatsAppMensajeRecibidoService objetoMensaje = new WhatsAppMensajeRecibidoService(unitOfWork);
                    var resultado = objetoMensaje.ListaUltimoMensajeChatRecibidoControlMensaje(idPersonal);
                    if (resultado != null)
                    {
                        return Ok(resultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 14/11/2022
        /// Versión: 1.0
        /// <summary>
        /// Obtiene el último mensaje recibido por oportunidad.
        /// </summary>
        /// <returns> Objeto WhatsAppMensajesRecibidosOperacionesDTO </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult WhatsAppUltimoMensajeRecibidosPorOportunidad([FromBody] Dictionary<string, string> filtro)
        {
            if (filtro != null)
            {
                try
                {
                    WhatsAppMensajeRecibidoService servicioMensajeEnviado = new WhatsAppMensajeRecibidoService(unitOfWork);
                    var idPersonal = filtro.Where(w => w.Key.ToLower() == "idasesor").FirstOrDefault();
                    var restultado = servicioMensajeEnviado.ObtenerMensajesRecibidosOperaciones(Convert.ToInt32(idPersonal.Value));

                    if (restultado != null)
                    {
                        return Ok(restultado);
                    }
                    else
                    {
                        return BadRequest("Error: Sin Datos");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return BadRequest("Los datos enviados no pueden ser nulos o estar vacios.");
            }
        }
        /// TipoFuncion: POST
        /// Autor: Jonathan Caipo
        /// Fecha: 12/12/2022
        /// Version: 1.0
        /// <summary>
        /// Adjunta archivo para mensaje de WhatsApp
        /// </summary>
        /// <returns> String </returns>
        [Route("[action]")]
        [HttpPost]
        public ActionResult AdjuntarArchivoWhatsApp([FromForm] IFormFile file)
        {
            string respuesta = string.Empty;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                WhatsAppMensajeRecibidoService whatsAppMensajeRecibidoService = new WhatsAppMensajeRecibidoService(unitOfWork);
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    respuesta = whatsAppMensajeRecibidoService.GuardarArchivos(fileBytes, file.ContentType, file.FileName);
                }

                if (string.IsNullOrEmpty(respuesta))
                {
                    return Ok(new { Resultado = "Error" });
                }
                else
                {
                    return Ok(new { Resultado = "Ok", UrlArchivo = respuesta, NombreArchivo = file.FileName });
                }
            }
            catch (Exception Ex)
            {
                return Ok(new { Resultado = "Error" });
            }
        }
    }
}
