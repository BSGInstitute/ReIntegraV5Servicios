using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Messenger;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Messenger;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.Messenger;
using Nancy.Json;
using Newtonsoft.Json;
using System.Net;
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

        /// Autor: Humberto Oscata
        /// Fecha: 22/04/2026
        /// Version: 1.0
        /// <summary>
        /// Captura registros de alumnos en base a chats de messenger mediante un modelo IA
        /// </summary>
        /// <param name="datosExtraccionRegistrosMessenger">Contiene PSID del alumno y rango de antiguedad de chats</param>
        /// <returns>Datos capturados por el modelo IA</returns>
        public async Task<DatosExtraccionRegistrosResponseDTO> CapturarRegistrosModeloIA(DatosExtraccionRegistrosMessengerDTO datosExtraccionRegistrosMessenger)
        {
            // 1. Obtencion de chats
            List<MensajeExtraccionRegistroDTO> ChatsWhatsAppMarketing = new List<MensajeExtraccionRegistroDTO>();
            DateTime fechaFin = DateTime.Now;
            DateTime fechaInicio = fechaFin.AddDays(-datosExtraccionRegistrosMessenger.Rango);

            ChatsWhatsAppMarketing = _messengerFacebookChatRepository.ObtenerChatsMessengerPorIdentificadorAmbitoPagina(datosExtraccionRegistrosMessenger.IdentificadorAmbitoPagina, fechaInicio, fechaFin);

            DatosExtraccionRegistrosRequestDTO datosExtraccionRegistrosRequest = new DatosExtraccionRegistrosRequestDTO
            {
                Id_cliente = datosExtraccionRegistrosMessenger.IdentificadorAmbitoPagina,
                Timestamp = fechaFin.ToString(),
                Mensajes = ChatsWhatsAppMarketing,
                Campos = new List<string>
                                {
                                    "nombres",
                                    "apellidos",
                                    "cargo",
                                    "area_de_formacion",
                                    "area_de_trabajo",
                                    "industria"
                                },
                Info_curso = ""
            };

            // 2. Envio de chats al modelo IA
            string url = $"http://ia-asistente-marketing-whatsapp-api.bsginstitute.com/api/extractor_texto/consulta/";

            var Serializer = new JavaScriptSerializer();
            var serializedResult = Serializer.Serialize(datosExtraccionRegistrosRequest);

            var resultado = await PostJsonAsync<DatosExtraccionRegistrosResponseDTO>(url, serializedResult);

            if (resultado == null)
                throw new Exception("La respuesta de la API externa fue nula o falló.");

            return resultado;
        }

        /// Autor: Humberto Oscata
        /// Fecha: 22/04/2026
        /// Version: 1.0
        /// <summary>
        /// Desactiva la interacción automática del asistente Messenger para un cliente y campania específicos
        /// </summary>
        /// <param name="identificadorAmbitoPagina">PSID del alumno</param>
        /// <param name="idAlumno">ID del alumno</param>
        /// <returns>Resultado del servicio externo</returns>
        public async Task<DesactivarInteraccionResponseDTO> DesactivarInteraccionAutomaticaMessenger(string identificadorAmbitoPagina, string idAlumno)
        {
            string url = $"http://ia-asistente-marketing-whatsapp-api.bsginstitute.com/api/interaccion_messenger/forzar_derivacion/?id_ambito={identificadorAmbitoPagina}&id_alumno={idAlumno}";

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, null);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Error al llamar al API externa: {responseContent}");

                var resultado = JsonConvert.DeserializeObject<DesactivarInteraccionResponseDTO>(responseContent);

                if (resultado == null)
                    throw new Exception("La respuesta de la API externa fue nula o inválida.");

                return resultado;
            }
        }

        private async Task<T> PostJsonAsync<T>(string url, string jsonString)
        {
            try
            {
                var http = (HttpWebRequest)WebRequest.Create(new Uri(url));
                http.Accept = "application/json";
                http.ContentType = "application/json";
                http.Method = "POST";

                byte[] bytes = Encoding.ASCII.GetBytes(jsonString);

                using (Stream requestStream = await http.GetRequestStreamAsync())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                }

                using (var response = await http.GetResponseAsync())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string content = await reader.ReadToEndAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error en el metodo PostJsonAsync: {ex.Message}", ex);
            }
        }
    }
}
