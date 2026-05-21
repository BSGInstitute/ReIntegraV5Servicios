using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BSI.Integra.Aplicacion.Base.Exceptions;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace BSI.Integra.Aplicacion.GestionPersonas.SCode.Service.Implementacion
{
    /// Service: WhatsAppMensajeEnviadoApiPostulanteService
    /// Autor: Claude (sdd-apply Slice 2 — gp-whatsapp-endpoints)
    /// Fecha: 2026-05-13
    /// Version: 1.0
    /// <summary>
    /// Orquesta lecturas del chat WhatsApp GP y el envio passthrough al WebHookWhatsApp.
    ///
    /// Mejoras explicitas vs patron legacy (`PostulanteWhatsAppService`, `WhatsAppMensajeEnviadoApiAtcService`):
    ///   - HttpClient via IHttpClientFactory (no `new HttpClient()`) — preserva testabilidad y evita socket exhaustion.
    ///   - Errores explicitos: 504 GatewayTimeout, 502 BadGateway, 4xx BadRequest/Unauthorized
    ///     (mapeados en GlobalExceptionHandlingMiddleware, no `catch { }` silencioso).
    ///   - Logging estructurado por llamada al proxy (IdPostulante, IdPersonal, status, latency).
    /// </summary>
    public class WhatsAppMensajeEnviadoApiPostulanteService : IWhatsAppMensajeEnviadoApiPostulanteService
    {
        // URL prod (comentada hasta deploy)
        private const string WebhookUrl = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGp";
        // private const string WebhookUrl = "https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphGp";

        private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

        private readonly IUnitOfWork                                          _unitOfWork;
        private readonly IHttpClientFactory                                   _httpClientFactory;
        private readonly ILogger<WhatsAppMensajeEnviadoApiPostulanteService>  _logger;

        public WhatsAppMensajeEnviadoApiPostulanteService(
            IUnitOfWork                                          unitOfWork,
            IHttpClientFactory                                   httpClientFactory,
            ILogger<WhatsAppMensajeEnviadoApiPostulanteService>  logger)
        {
            _unitOfWork        = unitOfWork;
            _httpClientFactory = httpClientFactory;
            _logger            = logger;
        }

        /// <summary>FR-1 / FR-5: pendientes del asesor logueado.</summary>
        public async Task<IEnumerable<PendienteWhatsAppPostulanteDTO>> ObtenerPendientesAsync(int idPersonalAsesor)
        {
            return await _unitOfWork.WhatsAppMensajeEnviadoApiPostulanteRepository
                .ObtenerPendientesAsync(idPersonalAsesor);
        }

        /// <summary>FR-2: ultimas conversaciones del asesor (1 por postulante).</summary>
        public async Task<IEnumerable<ConversacionWhatsAppPostulanteDTO>> ObtenerConversacionesAsync(int idPersonalAsesor)
        {
            return await _unitOfWork.WhatsAppMensajeEnviadoApiPostulanteRepository
                .ObtenerConversacionesAsync(idPersonalAsesor);
        }

        /// <summary>
        /// FR-9: passthrough al repo para validar ventana Meta 24h.
        /// Sin transformacion: paridad estricta con ATC (que tambien delega al repo).
        /// </summary>
        public async Task<bool> ValidarVentana24HorasAsync(string numero)
        {
            return await _unitOfWork.WhatsAppMensajeEnviadoApiPostulanteRepository
                .ValidarVentana24HorasAsync(numero);
        }

        /// <summary>FR-3: historial cronologico crudo. NotFound si el repo no encuentra el postulante.</summary>
        public async Task<HistorialChatPostulanteDTO> ObtenerHistorialAsync(int idPostulante, int? idPais)
        {
            var dto = await _unitOfWork.WhatsAppMensajeEnviadoApiPostulanteRepository
                .ObtenerHistorialAsync(idPostulante, idPais);

            if (dto is null)
            {
                throw new NotFoundException($"No se encontro historial para el postulante {idPostulante}.");
            }

            return dto;
        }

        /// <summary>
        /// FR-4 + FR-8: passthrough al WebHookWhatsApp. Maneja timeout/5xx/4xx con excepciones
        /// claras que el middleware mapea a 504/502/400/401.
        /// TODO: hook para ClasificacionRespuestaService futuro (paralelo a ATC). El bot intercepta
        /// aca antes del HTTP call.
        /// </summary>
        public async Task<EnviarMensajeWhatsAppPostulanteResponse> EnviarMensajeAsync(
            EnviarMensajeWhatsAppPostulanteRequest request, int idPersonalAsesor)
        {
            // Asegurar que el webhook reciba el IdPersonal del JWT (el front no es confiable).
            request.IdPersonal = idPersonalAsesor;

            var client      = _httpClientFactory.CreateClient();
            client.Timeout  = TimeSpan.FromSeconds(30);
            var contenido   = JsonContent.Create(request);
            var cronometro  = Stopwatch.StartNew();
            HttpResponseMessage? response = null;

            try
            {
                response = await client.PostAsync(WebhookUrl, contenido);
            }
            catch (TaskCanceledException ex)
            {
                cronometro.Stop();
                _logger.LogWarning(ex,
                    "Timeout WebHookWhatsApp postulante={IdPostulante} asesor={IdPersonal} ms={Ms}",
                    request.IdPostulante, idPersonalAsesor, cronometro.ElapsedMilliseconds);
                throw new GatewayTimeoutException("El webhook WhatsApp no respondio dentro del timeout (30s).");
            }
            catch (HttpRequestException ex)
            {
                cronometro.Stop();
                _logger.LogWarning(ex,
                    "Error de red WebHookWhatsApp postulante={IdPostulante} asesor={IdPersonal} ms={Ms}",
                    request.IdPostulante, idPersonalAsesor, cronometro.ElapsedMilliseconds);
                throw new BadGatewayException("Error de red al contactar el webhook WhatsApp.");
            }

            cronometro.Stop();
            var bodyTxt = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(
                "Proxy WebHookWhatsApp postulante={IdPostulante} asesor={IdPersonal} status={Status} ms={Ms}",
                request.IdPostulante, idPersonalAsesor, (int)response.StatusCode, cronometro.ElapsedMilliseconds);

            // 5xx → BadGateway
            if ((int)response.StatusCode >= 500)
            {
                throw new BadGatewayException(MensajeUpstream(bodyTxt, "El webhook WhatsApp devolvio un error de servidor."));
            }

            // 401/403 → Unauthorized
            if (response.StatusCode is HttpStatusCode.Unauthorized or HttpStatusCode.Forbidden)
            {
                throw new UnauthorizedAccessRequestException(
                    MensajeUpstream(bodyTxt, "No autorizado por el webhook WhatsApp."));
            }

            // Otros 4xx → BadRequest con el mensaje del webhook
            if ((int)response.StatusCode >= 400)
            {
                throw new BadRequestException(
                    MensajeUpstream(bodyTxt, "El webhook WhatsApp rechazo la solicitud."));
            }

            // 2xx → deserializar response del webhook
            if (string.IsNullOrWhiteSpace(bodyTxt))
            {
                return new EnviarMensajeWhatsAppPostulanteResponse
                {
                    Exito   = true,
                    Mensaje = "OK"
                };
            }

            var resp = JsonSerializer.Deserialize<EnviarMensajeWhatsAppPostulanteResponse>(bodyTxt, JsonOptions);
            return resp ?? new EnviarMensajeWhatsAppPostulanteResponse { Exito = true, Mensaje = "OK" };
        }

        private static string MensajeUpstream(string bodyTxt, string fallback)
        {
            return string.IsNullOrWhiteSpace(bodyTxt) ? fallback : bodyTxt;
        }
    }
}
