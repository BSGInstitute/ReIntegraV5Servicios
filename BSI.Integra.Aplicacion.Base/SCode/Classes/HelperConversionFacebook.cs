using BSI.Integra.Aplicacion.DTOs;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
namespace BSI.Integra.Aplicacion.Base.Classes
{
    public class HelperConversionFacebook
    {
        private static readonly string _accessToken;
        private static readonly string _pixelId;
        private static readonly bool _testMode;
        private static readonly string _testEventCode;

        static HelperConversionFacebook()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _accessToken = configuration["Facebook:AccessToken"];
            _pixelId = configuration["Facebook:PixelId"];
            _testMode = bool.Parse(configuration["Facebook:TestMode"] ?? "true");
            _testEventCode = configuration["Facebook:TestEventCode"] ?? "TEST4555";
        }
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
        public async Task<bool> EnviarApiConversionesFacebookAsincronica(int IdFacebookFormularioLeadgenm, string leadId, string email, string telefono, string Probabilidad, int idFaseAnterior, int idFaseOportunidadActual)
        {
            var Eventos = ValidarFasesProbabilidadEvento(Probabilidad, idFaseAnterior, idFaseOportunidadActual);
            if (Eventos.Count == 0)
                return false;
            try
            {
                bool exito = true;
                bool tieneEmailValido = !string.IsNullOrEmpty(email);
                bool tieneTelefonoValido = !string.IsNullOrEmpty(telefono);

                if (!tieneEmailValido && !tieneTelefonoValido)
                {
                    return false;
                }

                foreach (var eventos in Eventos)
                {
                    var eventName = GetEventNameForFase(eventos);
                    var eventTime = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();

                    var userData = new UserDataDTO
                    {
                        LeadId = leadId
                    };

                    if (tieneEmailValido)
                    {
                        userData.Em = Sha256Hash(email);
                    }

                    if (tieneTelefonoValido)
                    {
                        userData.Ph = Sha256Hash(telefono);
                    }

                    var facebookEvent = new FacebookLeadEventDTO
                    {
                        EventName = eventName,
                        EventTime = eventTime,
                        UserData = userData,
                        CustomData = new CustomDataDTO()
                    };

                    var resultado = await SendLeadEventAsync(facebookEvent);
                    if (!resultado) exito = false;
                }

                return exito;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private List<int> ValidarFasesProbabilidadEvento(string Probabilidad, int idFaseAnterior, int idFaseOportunidadActual)
        {
            var resultados = new List<int>();
            if ((idFaseOportunidadActual == 1 || idFaseOportunidadActual == 4 || idFaseOportunidadActual == 14 || idFaseOportunidadActual == 11 || idFaseOportunidadActual == 36) && (Probabilidad == "Media" || Probabilidad == "Alta" || Probabilidad == "Muy Alta"))
            {
                resultados.Add(1);
            }
            if (Probabilidad == "Muy Alta")
            {
                resultados.Add(2);
            }
            if (Probabilidad == "Muy Alta" && idFaseAnterior == 2 && (idFaseOportunidadActual!=4 && idFaseOportunidadActual != 36 && idFaseOportunidadActual != 11))
            {
                resultados.Add(3);
            }
            if (idFaseAnterior==13 || idFaseOportunidadActual == 13)
            {
                resultados.Add(4);
            }
            if (idFaseAnterior == 8 || idFaseOportunidadActual == 8)
            {
                resultados.Add(5);
            }
            if (idFaseAnterior == 12 || idFaseOportunidadActual == 12 || idFaseAnterior == 5 || idFaseOportunidadActual == 5 || idFaseAnterior == 23 || idFaseOportunidadActual == 23)
            {
                resultados.Add(6);
            }
            return resultados;
        }

        private string GetEventNameForFase(int idEventoFunnel)
        {
            return idEventoFunnel switch
            {
                1 => "Base Total",
                2 => "Base Muy Alta",
                3 => "Base Útil",
                4 => "Fase Máxima IT",
                5 => "Fase Máxima IP",
                6 => "Fase Máxima IC, IS y M",
                _ => $"Lead"
            };
        }
        private async Task<bool> SendLeadEventAsync(FacebookLeadEventDTO leadEvent)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    var url = $"https://graph.facebook.com/v23.0/{_pixelId}/events";

                    var requestData = new
                    {
                        access_token = _accessToken,
                        data = new[] { leadEvent },
                        //test_event_code = _testMode ? _testEventCode : null
                    };

                    var json = JsonConvert.SerializeObject(requestData, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    });

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(url, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        string errorMessage = $"Error Facebook API -Status: { response.StatusCode}-{responseContent}";
                        //SendErrorEmail(errorMessage);
                        return false;
                    }

                    Console.WriteLine($"Success Facebook API: {responseContent}");
                    return true;
                }
            }
            catch (TaskCanceledException)
            {
                string errorMessage = "Timeout en la conexión con Facebook API (30 segundos)";
                //SendErrorEmail(errorMessage);
                return false;
            }
            catch (HttpRequestException ex)
            {
                string errorMessage = $"Error de conexión con Facebook: {ex.Message}";
                //SendErrorEmail(errorMessage, ex.ToString());
                return false;
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error inesperado al enviar a Facebook: {ex.Message}";
                //SendErrorEmail(errorMessage,ex.ToString());
                return false;
            }
        }        

        private static string Sha256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                return null;

            using (SHA256 sha256 = SHA256.Create())
            {
                var normalizedInput = input.Trim().ToLower();
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(normalizedInput));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
        //private void SendErrorEmail(string error, string fullError = null)
        //{
        //    try
        //    {
        //        List<string> correos = new List<string>();
        //        correos.Add("mmantilla@bsginstitute.com");
        //        var Mailservice = new TMK_MailService();
        //        var mailData = new TMKMailDataDTO();
        //        mailData.Sender = "jcayo@bsginstitute.com";
        //        mailData.Recipient = string.Join(",", correos);
        //        mailData.Subject = "Error Envío Facebook Lead Event";
        //        mailData.Message =
        //            $"Error: {error}<br/>" +
        //            $"{(fullError != null ? $"Detalles completos:<br/>{fullError}" : "")}";
        //        mailData.Cc = "";
        //        mailData.Bcc = "";
        //        mailData.AttachedFiles = null;

        //        Mailservice.SetData(mailData);
        //        Mailservice.SendMessageTask();

        //        Console.WriteLine("Correo de error enviado exitosamente");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error al enviar correo de notificación: {ex.Message}");
        //    }
        //}
    }
}