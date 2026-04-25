using BSI.Integra.Aplicacion.Comercial.SCode.Service.Implementacion;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Operaciones.Service.Implementacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using static BSI.Integra.Aplicacion.DTO.SCode.Modelos.Calidad.TranscriptionDTO;

namespace BSI.Integra.Servicios.Controllers.Calidad
{
    /// Controlador: SolicitudTipoReporteController
    /// Autor: Gilmer Quispe.
    /// Fecha: 21/12/2022
    /// <summary>
    /// Gestión de Solicitud de Tipo de Reporte
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsVista")]
    public class TranscriptionController : Controller
    {
        private IUnitOfWork unitOfWork;
        public TranscriptionController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// Tipo Función: POST
        /// Autor: Joseph Llanque
        /// Fecha: 27/03/2025
        /// Versión: 1.0
        /// <summary>
        /// Realiza el procesamiento de transcripcion de llamadas
        /// </summary>
        /// <param name="TranscriptionWebhookPayload"> Datos necesarios para la insercion de datos </param>
        /// <returns> DTO: TranscriptionWebhookPayloadDTO </returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> ProcesarTranscripcion([FromBody] TranscriptionWebhookPayloadDTO payload)
        {
           
            if (payload == null)
            {
                return BadRequest("Payload de transcripción inválido.");
            }

            try
            {
                HubConnection signalRConnection = null;
                var resultado = new TranscriptionService(unitOfWork);
                await resultado.InsertTranscriptionDataAsync(payload);
                signalRConnection = new HubConnectionBuilder()
                    .WithUrl($"https://signalr-prototipo.bsginstitute.com/hubIntegraHub?idUsuario={payload.IdPersonal}&usuarioNombre={payload.UserName}&rooms=")
                    .WithAutomaticReconnect()
                    .ConfigureLogging(logging => {
                        logging.AddConsole();
                        logging.SetMinimumLevel(LogLevel.Debug);
                    })
                    .Build();

                await signalRConnection.StartAsync();

                // 4. Enviar notificación
                await signalRConnection.InvokeAsync("NotificarTranscripcion",
                    payload.IdLlamada,
                    payload.Status,
                    payload.IdPersonal?.ToString(),
                    payload.contacto);

                return Ok("Transcripción insertada correctamente.");

            }
            catch (System.Exception ex)
            {
                // Registra el error según corresponda
                return StatusCode(500, $"Error al insertar la transcripción: {ex.Message}");
            }
        }
        /// Tipo Función: GET
        /// Autor: Joseph Llanque 
        /// Fecha: 26/12/2025
        /// Versión: 1.0
        /// <summary>
        /// Obtiene Detalle de transcripcion 
        /// </summary>
        /// <returns> List<ComboDTO> </returns>
        [Route("[action]/{idLlamada}")]
        [HttpGet]
        public async Task<IActionResult> ObtenerTranscripcion(int idLlamada)
        {
            try
            {
                var transcriptionService = new TranscriptionService(unitOfWork);
                var resultado = await transcriptionService.ObtenerTranscripcion(idLlamada);
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
