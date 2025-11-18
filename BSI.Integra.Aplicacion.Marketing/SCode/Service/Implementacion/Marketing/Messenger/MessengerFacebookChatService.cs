using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using Newtonsoft.Json;
using System.Text;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.Messenger
{
    public class MessengerFacebookChatService : IMessengerFacebookChatService
    {
        IMessengerFacebookChatRepository _messengerFacebookChatRepository;

        public MessengerFacebookChatService(IMessengerFacebookChatRepository messengerFacebookChatRepository)
        {
            _messengerFacebookChatRepository = messengerFacebookChatRepository;
        }

        public List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            fechaInicio = fechaInicio ?? DateTime.UtcNow.Date;
            fechaFin = fechaFin ?? fechaInicio.Value.AddDays(1);

            return _messengerFacebookChatRepository.ObtenerGrillaChats(fechaInicio, fechaFin, tipo);
        }

        public List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request)
        {
            return _messengerFacebookChatRepository.ObtenerHistorialChatPorPSID(request);
        }

        public async Task<EnviarMensajeResponse> EnviarMensajeTexto(EnviarMensajeTextoRequest request)
        {
            string URL = $"https://localhost:7205/api/messaging/send";
            //string URL = $"https://localhost:7205/api/messaging/send";

            using var httpClient = new HttpClient();
            string jsonRequest = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage httpResponse = await httpClient.PostAsync(URL, content);

                string rawResponse = await httpResponse.Content.ReadAsStringAsync();

                if (httpResponse.IsSuccessStatusCode)
                {

                    EnviarMensajeResponse successResponse = JsonConvert.DeserializeObject<EnviarMensajeResponse>(rawResponse);

                    if (successResponse != null)
                        return successResponse;

                    return new EnviarMensajeResponse
                    {
                        Success = false,
                        Message = "La API respondió correctamente, pero no se pudo deserializar la respuesta.",
                    };
                }
                else 
                {
                    return new EnviarMensajeResponse
                    {
                        Success = false,
                        Message = $"Error al enviar mensaje. Código HTTP: {(int)httpResponse.StatusCode}. Respuesta: {rawResponse.Substring(0, Math.Min(rawResponse.Length, 150))}...",
                    };
                }
            }
            catch (Exception ex)
            {
                return new EnviarMensajeResponse
                {
                    Success = false,
                    Message = $"Error inesperado al procesar la respuesta: {ex.Message}",
                };
            }
        }
    }
}
