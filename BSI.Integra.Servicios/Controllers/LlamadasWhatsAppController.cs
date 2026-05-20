using BSI.Integra.Aplicacion.Base.BO;
using BSI.Integra.Aplicacion.Comercial.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Interaccion.Service.Implementacion;
using BSI.Integra.Repositorio.UnitOfWork;
using BSI.Integra.Servicios.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        /// Tipo Función: GET
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-15
        /// Versión: 1.0
        /// <summary>
        /// Consulta el estado actual del consentimiento de llamada saliente para un número.
        /// Lo usa el frontend para decidir si mostrar "Solicitar llamada", "Esperando respuesta",
        /// "Llamar ahora" (consentimiento vigente) o "Rechazado".
        /// </summary>
        /// <param name="numeroWhatsApp">Número del cliente (con código de país, con o sin `+`)</param>
        /// <param name="idPais">Id del país del cliente</param>
        /// <returns>Estado del consentimiento + flags PuedeSolicitar/PuedeLlamar</returns>
        [HttpGet("EstadoConsentimiento")]
        public IActionResult ObtenerEstadoConsentimiento(
            [FromQuery] string numeroWhatsApp,
            [FromQuery] int idPais)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroWhatsApp))
                    return BadRequest(new { mensaje = "numeroWhatsApp es requerido" });

                var servicio = new LlamadasWhatsAppService(unitOfWork);
                var resultado = servicio.ObtenerEstadoConsentimiento(numeroWhatsApp, idPais);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// Tipo Función: POST
        /// Autor: WhatsApp Business Calling API integration
        /// Fecha: 2026-05-19
        /// Versión: 1.0
        /// <summary>
        /// Recibe el blob de la grabación de una llamada (entrante o saliente) generado con
        /// MediaRecorder del browser, lo sube a Azure Blob (storage 'repositorioaudiollamada',
        /// container 'whatsapp-calling/Grabaciones/') y persiste GrabacionUrl + GrabacionBlobNombre
        /// en com.T_WhatsappLlamada vía SP_WhatsappLlamada_ActualizarGrabacion.
        /// </summary>
        [HttpPost("SubirGrabacion")]
        public IActionResult SubirGrabacion([FromForm] SubirGrabacionLlamadaDTO entidad)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entidad?.File == null || entidad.File.Length == 0)
                return BadRequest(new { mensaje = "Archivo de grabación vacío o no enviado" });

            if (entidad.IdWhatsappLlamada <= 0)
                return BadRequest(new { mensaje = "IdWhatsappLlamada inválido" });

            try
            {
                var registroClaimToken = ValidacionClaim.ObtenerRegistroClaimToken(User.Identity as ClaimsIdentity);
                string usuario = registroClaimToken?.UserName ?? "WebClient";

                // Naming: llamada_<idLlamada>_<yyyyMMddHHmmss>.webm
                // Extensión derivada del content-type para soportar webm/ogg/mp4 sin asumir formato.
                string extension = entidad.File.ContentType switch
                {
                    string ct when ct.Contains("webm") => ".webm",
                    string ct when ct.Contains("ogg")  => ".ogg",
                    string ct when ct.Contains("mp4")  => ".mp4",
                    string ct when ct.Contains("wav")  => ".wav",
                    _                                   => ".webm"
                };
                string nombreArchivo = $"llamada_{entidad.IdWhatsappLlamada}_{DateTime.Now:yyyyMMddHHmmss}{extension}";

                const string rutaBlob     = "whatsapp-calling/Grabaciones/";
                const string rutaCompleta = $"https://repositorioaudiollamada.blob.core.windows.net/{rutaBlob}";

                var gestionArchivo = new GestionArchivoLlamadaService(unitOfWork);
                string url = gestionArchivo.SubirArchivoAudioLlamada(
                    entidad.File.ConvertToByte(),
                    entidad.File.ContentType,
                    nombreArchivo,
                    rutaCompleta,
                    rutaBlob);

                if (string.IsNullOrEmpty(url))
                {
                    return StatusCode(500, new SubirGrabacionLlamadaResultadoDTO
                    {
                        Ok = false,
                        Mensaje = "Falló la subida del blob a Azure"
                    });
                }

                unitOfWork.LlamadasWhatsAppRepository.ActualizarGrabacion(
                    entidad.IdWhatsappLlamada, url, nombreArchivo, usuario);

                return Ok(new SubirGrabacionLlamadaResultadoDTO
                {
                    Ok         = true,
                    Url        = url,
                    BlobNombre = nombreArchivo
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new SubirGrabacionLlamadaResultadoDTO
                {
                    Ok      = false,
                    Mensaje = ex.Message
                });
            }
        }
    }
}
