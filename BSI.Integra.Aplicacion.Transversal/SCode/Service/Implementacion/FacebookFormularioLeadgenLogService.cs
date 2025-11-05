using AutoMapper;
using BSI.Integra.Aplicacion.Base.Classes;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Servicios.Service.Implementacion;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using BSI.Integra.Repositorio.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// <summary>
    /// Gestión general de T_FacebookFormularioLeadgenLog
    /// Autor: Max Mantilla Rodriguez
    /// Fecha: 09/10/2026
    /// </summary>
    public class FacebookFormularioLeadgenLogService : IFacebookFormularioLeadgenLogService
    {
        #region Campos y Constantes
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private static readonly string _accessToken;
        private static readonly string _pixelId;
        private static readonly bool _testMode;
        private static readonly string _testEventCode;
        //Pixel de fase maxima para el Area de Construcción
        private static readonly string _accessTokenFaseMaximaConstruccion;
        private static readonly string _pixelIdFaseMaximaConstruccion;
        private static readonly bool _testModeFaseMaximaConstruccion;
        private static readonly string _testEventCodeFaseMaximaConstruccion;
        private static readonly HttpClient _httpClient;
        private static readonly HashSet<int> _fasesMaximas = new() { 12, 5, 23 };
        private static readonly HashSet<string> _probabilidadesValidas = new() { "Media", "Alta", "Muy Alta" };

        private const string FACEBOOK_API_URL = "https://graph.facebook.com/v23.0/{0}/events";
        private const int HTTP_TIMEOUT_SECONDS = 30;
        private const int MAX_LONGITUD_RESPUESTA = 4000;
        #endregion

        #region Constructores
        static FacebookFormularioLeadgenLogService()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _accessToken = configuration["Facebook:AccessToken"];
            _pixelId = configuration["Facebook:PixelId"];
            _testMode = bool.Parse(configuration["Facebook:TestMode"] ?? "true");
            _testEventCode = configuration["Facebook:TestEventCode"] ?? "TEST4555";
            //Pixel de fase maxima para el Area de Construcción
            _accessTokenFaseMaximaConstruccion = configuration["FacebookFaseMaximaConstruccion:AccessToken"];
            _pixelIdFaseMaximaConstruccion = configuration["FacebookFaseMaximaConstruccion:PixelId"];
            _testModeFaseMaximaConstruccion = bool.Parse(configuration["FacebookFaseMaximaConstruccion:TestMode"] ?? "true");
            _testEventCodeFaseMaximaConstruccion = configuration["FacebookFaseMaximaConstruccion:TestEventCode"] ?? "TEST4555";

            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(HTTP_TIMEOUT_SECONDS)
            };
        }

        public FacebookFormularioLeadgenLogService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<TFacebookFormularioLeadgenLog, FacebookFormularioLeadgenLog>()
                   .ReverseMap());
            _mapper = new Mapper(config);
        }
        #endregion

        #region Métodos Base - Optimizados
        public FacebookFormularioLeadgenLog Add(FacebookFormularioLeadgenLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenLogRepository.Add(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookFormularioLeadgenLog>(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error agregando entidad: {ex.Message}", ex);
            }
        }

        public FacebookFormularioLeadgenLog Update(FacebookFormularioLeadgenLog entidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenLogRepository.Update(entidad);
                _unitOfWork.Commit();
                return _mapper.Map<FacebookFormularioLeadgenLog>(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error actualizando entidad: {ex.Message}", ex);
            }
        }

        public bool Delete(int id, string usuario)
        {
            try
            {
                _unitOfWork.FacebookFormularioLeadgenLogRepository.Delete(id, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error eliminando entidad ID {id}: {ex.Message}", ex);
            }
        }

        public List<FacebookFormularioLeadgenLog> Add(List<FacebookFormularioLeadgenLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenLogRepository.Add(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookFormularioLeadgenLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error agregando listado: {ex.Message}", ex);
            }
        }

        public List<FacebookFormularioLeadgenLog> Update(List<FacebookFormularioLeadgenLog> listadoEntidad)
        {
            try
            {
                var modelo = _unitOfWork.FacebookFormularioLeadgenLogRepository.Update(listadoEntidad);
                _unitOfWork.Commit();
                return _mapper.Map<List<FacebookFormularioLeadgenLog>>(modelo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error actualizando listado: {ex.Message}", ex);
            }
        }

        public bool Delete(List<int> listadoIds, string usuario)
        {
            try
            {
                _unitOfWork.FacebookFormularioLeadgenLogRepository.Delete(listadoIds, usuario);
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error eliminando listado IDs: {ex.Message}", ex);
            }
        }
        #endregion

        #region DTOs
        public class FacebookLeadEventDTO
        {
            [JsonProperty("event_name")]
            public string EventName { get; set; }

            [JsonProperty("event_time")]
            public long EventTime { get; set; }

            [JsonProperty("action_source")]
            public string ActionSource { get; set; } = "system_generated";

            [JsonProperty("user_data")]
            public UserDataDTO UserData { get; set; }

            [JsonProperty("custom_data")]
            public CustomDataDTO CustomData { get; set; }
        }

        public class UserDataDTO
        {
            [JsonProperty("em")]
            public string Em { get; set; }

            [JsonProperty("ph")]
            public string Ph { get; set; }

            [JsonProperty("lead_id")]
            public string LeadId { get; set; }
        }

        public class CustomDataDTO
        {
            [JsonProperty("event_source")]
            public string EventSource { get; set; } = "crm";

            [JsonProperty("lead_event_source")]
            public string LeadEventSource { get; set; } = "CRM Integra";
        }
        #endregion

        #region Métodos Facebook Conversion API - Optimizados
        public void EvaluarConversionFacebook(int oportunidadId)
        {
            try
            {
                var conversionFb = _unitOfWork.OportunidadRepository
                    .ObtenerInformacionOportunidadConversion(oportunidadId);

                if (conversionFb == null) return;

                var informacionOportunidad = _unitOfWork.OportunidadRepository
                    .ObtenerInformacionOportunidadProbabilidad(oportunidadId);

                if (informacionOportunidad == null) return;

                try
                {
                    EnviarApiConversionesFacebookAsincronica(
                        conversionFb.IdFacebookFormularioLeadgen,
                        conversionFb.LeadId,
                        conversionFb.Email,
                        conversionFb.Telefono,
                        informacionOportunidad.ClasificacionProbabilidad,
                        informacionOportunidad.IdFaseOportunidadAnterior,
                        informacionOportunidad.IdFaseOportunidadActual,
                        informacionOportunidad.IdAreaCapacitacion,
                        _accessToken,
                        _pixelId

                    );
                    //Registro para Pixel de Construccion
                    if (informacionOportunidad.IdAreaCapacitacion == 2)
                    {
                        EnviarApiConversionesFacebookAsincronica(
                        conversionFb.IdFacebookFormularioLeadgen,
                        conversionFb.LeadId,
                        conversionFb.Email,
                        conversionFb.Telefono,
                        informacionOportunidad.ClasificacionProbabilidad,
                        informacionOportunidad.IdFaseOportunidadAnterior,
                        informacionOportunidad.IdFaseOportunidadActual,
                        informacionOportunidad.IdAreaCapacitacion,
                        _accessTokenFaseMaximaConstruccion,
                        _pixelIdFaseMaximaConstruccion
                    );
                    }
                }
                catch (Exception ex)
                {
                    EnviarCorreoError($"Error en EnviarApiConversionesFacebookAsincronica",
                        $@"IdOportunidad: {oportunidadId}<br/>
                           IdFacebookFormularioLeadgen: {conversionFb?.IdFacebookFormularioLeadgen}<br/>
                           LeadId: {conversionFb?.LeadId}<br/>
                           Email: {conversionFb?.Email}<br/>
                           Telefono: {conversionFb?.Telefono}<br/>
                           ClasificacionProbabilidad: {informacionOportunidad?.ClasificacionProbabilidad}<br/>
                           IdFaseOportunidadAnterior: {informacionOportunidad?.IdFaseOportunidadAnterior}<br/>
                           IdFaseOportunidadActual: {informacionOportunidad?.IdFaseOportunidadActual}<br/>",
                        ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EvaluarConversionFacebook: {ex.Message}");
            }
        }

        public bool EnviarApiConversionesFacebookAsincronica(
            int idFacebookFormularioLeadgen,
            string leadId,
            string email,
            string telefono,
            string probabilidad,
            int idFaseAnterior,
            int idFaseOportunidadActual,
            int idAreaCapacitacion,
            string token,
            string pixel)
        {
            var eventos = ValidarFasesProbabilidadEvento(probabilidad, idFaseAnterior, idFaseOportunidadActual);
            if (eventos.Count == 0) return false;

            var tieneEmailValido = !string.IsNullOrEmpty(email);
            var tieneTelefonoValido = !string.IsNullOrEmpty(telefono);

            if (!tieneEmailValido && !tieneTelefonoValido) return false;

            var resultados = new List<bool>();
            foreach (var evento in eventos)
            {
                var resultado = EnviarEventoFacebook(evento, leadId, email, telefono, tieneEmailValido, tieneTelefonoValido, idFacebookFormularioLeadgen, token, pixel);
                resultados.Add(resultado);
            }

            return resultados.All(result => result);
        }

        private bool EnviarEventoFacebook(int evento, string leadId, string email, string telefono, bool tieneEmail, bool tieneTelefono, int idFacebookFormularioLeadgen, string token, string pixel)
        {
            var facebookEvent = CrearEventoFacebook(evento, leadId, email, telefono, tieneEmail, tieneTelefono);
            return EnviarEventoLead(facebookEvent, evento, leadId, email, telefono, tieneEmail, tieneTelefono, idFacebookFormularioLeadgen, token, pixel);
        }

        private FacebookLeadEventDTO CrearEventoFacebook(int idEvento, string leadId, string email, string telefono, bool tieneEmail, bool tieneTelefono)
        {
            var userData = new UserDataDTO { LeadId = leadId };

            if (tieneEmail)
                userData.Em = CalcularHashSha256(email);

            if (tieneTelefono)
                userData.Ph = CalcularHashSha256(telefono);

            return new FacebookLeadEventDTO
            {
                EventName = ObtenerNombreEventoPorFase(idEvento),
                EventTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds(),
                UserData = userData,
                CustomData = new CustomDataDTO()
            };
        }

        private List<int> ValidarFasesProbabilidadEvento(string probabilidad, int idFaseAnterior, int idFaseOportunidadActual)
        {
            var resultados = new List<int>();

            // Evento 1: Fases con probabilidades válidas
            if (_probabilidadesValidas.Contains(probabilidad))
                resultados.Add(1);

            // Evento 2: Probabilidad Muy Alta
            if (probabilidad == "Muy Alta")
                resultados.Add(2);

            // Evento 3: Transición específica de fases
            if (probabilidad == "Muy Alta" && idFaseAnterior == 2 &&
                !new HashSet<int> { 4, 36, 11 }.Contains(idFaseOportunidadActual))
                resultados.Add(3);

            // Evento 4: Fase 13
            if (idFaseAnterior == 13 || idFaseOportunidadActual == 13)
                resultados.Add(4);

            // Evento 5: Fase 8
            if (idFaseAnterior == 8 || idFaseOportunidadActual == 8)
                resultados.Add(5);

            // Evento 6: Fases máximas
            if (_fasesMaximas.Contains(idFaseAnterior) || _fasesMaximas.Contains(idFaseOportunidadActual))
                resultados.Add(6);

            return resultados;
        }

        private static string ObtenerNombreEventoPorFase(int idEventoFase) => idEventoFase switch
        {
            1 => "Base Total",
            2 => "Base Muy Alta",
            3 => "Base Útil",
            4 => "Fase Máxima IT",
            5 => "Fase Máxima IP",
            6 => "Fase Máxima IC, IS y M",
            _ => "Lead"
        };

        private bool EnviarEventoLead(FacebookLeadEventDTO leadEvent, int evento, string leadId, string email, string telefono, bool tieneEmail, bool tieneTelefono, int idFacebookFormularioLeadgen, string token, string pixel)
        {
            try
            {
                var url = string.Format(FACEBOOK_API_URL, pixel);
                var requestData = new
                {
                    access_token = token,
                    data = new[] { leadEvent }
                };

                var jsonEnvio = JsonConvert.SerializeObject(requestData,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

                var content = new StringContent(jsonEnvio, Encoding.UTF8, "application/json");

                string respuestaApi = "Enviando...";
                bool esError = false;

                try
                {
                    // Ejecutar llamada a la API de Facebook
                    var response = _httpClient.PostAsync(url, content).Result;
                    var responseContent = response.Content.ReadAsStringAsync().Result;

                    respuestaApi = responseContent.Length > MAX_LONGITUD_RESPUESTA
                        ? responseContent.Substring(0, MAX_LONGITUD_RESPUESTA)
                        : responseContent;

                    esError = !response.IsSuccessStatusCode;

                    if (!response.IsSuccessStatusCode)
                    {
                        EnviarCorreoError($"Facebook API Error", $"IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Error Facebook API - Status: {response.StatusCode} - {responseContent}");
                        return false;
                    }

                    Console.WriteLine($"", $"Success Facebook API: {responseContent}");
                    return true;
                }

                catch (TaskCanceledException ex)
                {
                    respuestaApi = "Timeout: " + ex.Message;
                    esError = true;
                    EnviarCorreoError($"Facebook API Timeout", "IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Timeout en la conexión con Facebook API (30 segundos)");
                    return false;
                }
                catch (HttpRequestException ex)
                {
                    respuestaApi = "HttpRequestException: " + ex.Message;
                    esError = true;
                    EnviarCorreoError($"Facebook API HttpRequestException", $"IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Error de conexión con Facebook: {ex.Message}", ex.ToString());
                    return false;
                }
                catch (Exception ex)
                {
                    respuestaApi = "Exception: " + ex.Message;
                    esError = true;

                    EnviarCorreoError($"Facebook API Exception ", $"IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Error inesperado al enviar a Facebook: {ex.Message}", ex.ToString());
                    return false;
                }
                finally
                {
                    RegistrarLogFacebook(idFacebookFormularioLeadgen, jsonEnvio, respuestaApi, esError, ObtenerNombreEventoPorFase(evento), pixel);
                }
            }
            catch (Exception ex)
            {
                EnviarCorreoError($"Facebook API Error crítico", $"IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Error crítico en EnviarEventoLeadAsync: {ex.Message}", ex.ToString());
                RegistrarLogFacebook(idFacebookFormularioLeadgen, "Error en proceso principal", "Exception: " + ex.Message, true, ObtenerNombreEventoPorFase(evento), pixel);
                return false;
            }
        }

        private void RegistrarLogFacebook(int idFacebookFormularioLeadgen, string jsonEnvio, string respuestaApi, bool esError, string evento, string pixel)
        {
            try
            {
                var entidad = new FacebookFormularioLeadgenLog
                {
                    Id = 0,
                    IdFacebookFormularioLeadgen = idFacebookFormularioLeadgen,
                    JsonApiFacebook = jsonEnvio,
                    RespuestaApiFacebook = respuestaApi,
                    EsError = esError,
                    FechaCreacion = DateTime.Now,
                    FechaModificacion = DateTime.Now,
                    UsuarioCreacion = "System",
                    UsuarioModificacion = "System",
                    Estado = true,
                    Evento = evento,
                    Pixel = pixel
                };



                _unitOfWork.FacebookFormularioLeadgenLogRepository.Add(entidad);
                _unitOfWork.Commit();


                Console.WriteLine($"Log registrado exitosamente - Error: {esError}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error crítico registrando log: {ex.Message}");
                EnviarCorreoError($"Facebook API Error crítico registrando", $"IdFacebookFormularioLeadgen: {idFacebookFormularioLeadgen} - Error crítico registrando log Facebook: {ex.Message}", ex.ToString());
            }
        }

        private static string CalcularHashSha256(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            using var sha256 = SHA256.Create();
            var normalizedInput = input.Trim().ToLower();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(normalizedInput));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
        #endregion

        #region Métodos de Soporte - Optimizados
        private void EnviarCorreoError(string asunto, string mensaje = null, string errorCompleto = null)
        {
            try
            {
                var servicioCorreo = new TMK_MailService();
                var datosCorreo = new TMKMailDataDTO
                {
                    Sender = "jcayo@bsginstitute.com",
                    Recipient = "mmantilla@bsginstitute.com",
                    Subject = asunto,
                    Message = $"{mensaje}{(errorCompleto != null ? $"<br/>Detalles completos:<br/>{errorCompleto}" : "")}",
                    Cc = "",
                    Bcc = "",
                    AttachedFiles = null
                };

                servicioCorreo.SetData(datosCorreo);
                servicioCorreo.SendMessageTask();

                Console.WriteLine("Correo de error enviado exitosamente");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo de notificación: {ex.Message}");
            }
        }
        #endregion

        #region Dispose Pattern
        public void Dispose()
        {
            _httpClient?.Dispose();
            _unitOfWork?.Dispose();
        }
        #endregion
    }
}