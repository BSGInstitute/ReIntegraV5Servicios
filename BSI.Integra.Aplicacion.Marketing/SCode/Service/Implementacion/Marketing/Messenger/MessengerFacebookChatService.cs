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

        /// Autor: Humberto Oscata
        /// Fecha: 11/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene la grilla de mensajes de messenger entrantes o salientes respecto de un rango de fechas
        /// </summary>
        /// <param name="fechaInicio">fecha inicio rango</param>
        /// <param name="fechaFin">fecha fin rango</param>
        /// <param name="tipo">Tipo de mensajes (entrante, saliente)</param>
        /// <returns>Lista de ultimos mensajes messenger por PSID</returns>
        public List<ResumenMessengerFacebookChatDTO> ObtenerGrillaChats(DateTime? fechaInicio, DateTime? fechaFin, string tipo)
        {
            fechaInicio = fechaInicio ?? DateTime.UtcNow.Date;
            fechaFin = fechaFin ?? fechaInicio.Value.AddDays(1);

            return _messengerFacebookChatRepository.ObtenerGrillaChats(fechaInicio, fechaFin, tipo);
        }

        /// Autor: Humberto Oscata
        /// Fecha: 12/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene el historial de chats para un identificador de ambito de pagina
        /// </summary>
        /// <param name="request">Objeto que contiene el identificadoAmbitoPagina</param>
        /// <returns>Lista de mensajes messenger</returns>
        public List<ChatMessengerFacebookDTO> ObtenerHistorialChatPorPSID(ObtenerHistorialChatPorPSIDRequestDTO request)
        {
            return _messengerFacebookChatRepository.ObtenerHistorialChatPorPSID(request);
        }

        /// Autor: Humberto Oscata
        /// Fecha: 19/11/2025
        /// Version: 1.0
        /// <summary>
        /// Obtiene los datos generales de alumnos registrados por PSID
        /// </summary>
        /// <param name="request">Objeto que contiene el identificadoAmbitoPagina</param>
        /// <returns>Listado de alumnos y sus detalles por PSID</returns>
        public List<ObtenerDatosGeneralesAlumnosPorPSIDResponseDTO> ObtenerDatosGeneralesAlumnosPorPSID(ObtenerDatosGeneralesAlumnosPorPSIDRequestDTO request)
        {
            return _messengerFacebookChatRepository.ObtenerDatosGeneralesAlumnosPorPSID(request);
        }

        /// Autor: Humberto Oscata
        /// Fecha: 14/11/2025
        /// Version: 1.0
        /// <summary>
        /// Envia un mensaje de texto
        /// </summary>
        /// <param name="request">Objeto con los detalles del mensaje</param>
        /// <returns>Objeto con cofirmacion de envio y posible mensaje error</returns>
        public async Task<EnviarMensajeResponse> EnviarMensajeTexto(EnviarMensajeTextoRequest request)
        {
            string URL = $"https://hook-fbmessenger.bsginstitute.com/api/messaging/send";
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

        /// Autor: Humberto Oscata
        /// Fecha: 19/11/2025
        /// Version: 1.0
        /// <summary>
        /// Guarda el registro de oportunidad y alumno creado por PSID
        /// </summary>
        /// <param name="identificadorAmbitoPagina">Identificador de Ambito (PSID) del registro</param>
        /// <param name="idOportunidad">IdOportunidad del registro</param>
        /// <param name="usuario">Usuario de creacion de la oportunidad</param>
        /// <returns>Estado de registro en tabla</returns>
        public bool GuardarAlumnoOportunidadRegistro(string identificadorAmbitoPagina, int idOportunidad, string usuario)
        {
            return _messengerFacebookChatRepository.GuardarAlumnoOportunidadRegistro(identificadorAmbitoPagina, idOportunidad, usuario);
        }
    }
}
