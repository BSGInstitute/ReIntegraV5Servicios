using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Transversal.Service.Interface;
using BSI.Integra.Repositorio.UnitOfWork;

using Newtonsoft.Json;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppMensajeEnviadoApiAtcDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Implementacion
{
    /// Autor: Alexis Arroyo
    /// Fecha: 15/04/2026
    /// Version: 1.0
    /// <summary>
    /// Intermediario backend para envio de mensajes WhatsApp ATC.
    /// Contiene la logica de negocio: valida asignacion de BOT, gestiona el hilo chat
    /// y delega el envio real al webhook externo.
    /// El acceso a datos se delega a IWhatsAppMensajeEnviadoApiAtcRepository via IUnitOfWork.
    /// </summary>
    public class WhatsAppMensajeEnviadoApiAtcService : IWhatsAppMensajeEnviadoApiAtcService
    {
        private readonly IUnitOfWork _unitOfWork;

        private const string WebhookUrl = "https://hook-whatsapp.bsginstitute.com/api/WebHookWhatsApp/WhatsAppMensajeApiGraphAtc";
        //private const string WebhookUrl = "https://localhost:7225/api/WebHookWhatsApp/WhatsAppMensajeApiGraphAtc";

        private const int EstadoBot = 1; // ia.T_ChatbotConversacionEstado

        private const int EstadoDerivado = 2; // ia.T_ChatbotConversacionEstado

		private const int IdActor = 2; // Id del actor 
        public WhatsAppMensajeEnviadoApiAtcService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Valida la logica del chatbot ATC y delega el envio al webhook.
        /// Flujo:
        ///   1. Sin BOT asignado  -> envio directo (flujo normal).
        ///   2. Con BOT asignado  -> gestiona hilo, envia al webhook, guarda clasificacion + mensaje chatbot.
        /// </summary>
        public async Task<RespuestaMensajeAtcDTO> EnviarMensajeValidacion(WhatsAppEnviarMensajeDTO Mensaje, string usuario)
        {
             var respuesta = new RespuestaMensajeAtcDTO();
            try
            {
                var repo = _unitOfWork.WhatsAppMensajeEnviadoApiAtcRepository;

                // 1. Validad si estas en la whitelist el asesor
                var tieneBotAsignado = await repo.TieneBotAsignado(Mensaje.IdPersonal);

                if (!tieneBotAsignado)
                {
                    //Continuar con el flujo normal enviar por el webhook
                    var hookRespuestaNormal = await UrlPostAsync(WebhookUrl, JsonConvert.SerializeObject(Mensaje));
                    IntentarLoguear(Mensaje.IdAlumno, Mensaje.WaTo, hookRespuestaNormal?.Mensaje);
                    respuesta.Estado  = hookRespuestaNormal?.EstadoMensaje ?? false;
                    respuesta.Mensaje = hookRespuestaNormal?.Mensaje;
                    return respuesta;
                }

                // 2. Validar que se tenga un hilo abierto 
                var hilo = await repo.ObtenerHiloAbierto(Mensaje.IdAlumno);

                int idHilo;

                if (hilo == null)
                {
                    // Crear nuevo hilo 
                    idHilo = await repo.CrearHiloChat(
						Mensaje.IdAlumno > 0 ? (int?)Mensaje.IdAlumno : null,
						Mensaje.WaTo,
                        usuario);
                }
                else
                {
                    idHilo = hilo.Id;

                    if (hilo.IdChatbotConversacionEstado == EstadoBot || hilo.IdChatbotConversacionEstado == EstadoDerivado)
                    {
                        // Actualizar el estado del hilo a Asesor
                        await repo.ActualizarHiloAsesor(idHilo, usuario);
                    }
                   
                }

                // 3. Enviar mensaje al webhook
                var datoRespuesta = await UrlPostAsync(WebhookUrl, JsonConvert.SerializeObject(Mensaje));
                //IntentarLoguear(Mensaje.IdAlumno, Mensaje.WaTo, datoRespuesta?.Error);

                // Errores conocidos del webhook
                if (datoRespuesta?.Mensaje?.Contains("131026") == true)
                {
                    respuesta.Estado  = false;
                    respuesta.Mensaje = "El cliente no tiene WhatsApp activo o está inhabilitado temporalmente.";
                    return respuesta;
                }
                if (datoRespuesta?.Mensaje?.Contains("000001") == true)
                {
                    respuesta.Estado  = false;
                    respuesta.Mensaje = "El asesor no tiene chip asignado para el país del alumno.";
                    return respuesta;
                }
                if (datoRespuesta == null || !datoRespuesta.EstadoMensaje)
                {
                    respuesta.Estado  = false;
                    respuesta.Mensaje = datoRespuesta?.Mensaje ?? "Error al enviar el mensaje.";
                    return respuesta;
                }

                // 4. Guardar clasificacion y mensaje chatbot en un solo SP
                await repo.InsertarMensajeChatbotCompleto(datoRespuesta.IdWhatsappMensajeAtc ?? 0, idHilo, IdActor, usuario); // 2 = ASESOR

                respuesta.Estado  = true;
                respuesta.Mensaje = "Mensaje enviado correctamente.";
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Finaliza la conversacion activa del alumno cambiando el estado a CERRADA_ASESOR (6).
        /// Busca el hilo por IdAlumno (primario) o WaTo (fallback).
        /// </summary>
        public async Task<RespuestaMensajeAtcDTO> FinalizarConversacion(FinalizarConversacionDTO json, string usuario)
        {
            var respuesta = new RespuestaMensajeAtcDTO();
            try
            {
                var repo = _unitOfWork.WhatsAppMensajeEnviadoApiAtcRepository;

                var cerrado = await repo.FinalizarConversacion(json.IdAlumno, json.WaTo, usuario);

                if (!cerrado)
                {
                    respuesta.Estado  = false;
                    respuesta.Mensaje = "No se encontró una conversación activa para finalizar.";
                    return respuesta;
                }

                respuesta.Estado  = true;
                respuesta.Mensaje = "Conversación finalizada correctamente.";
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IntentarLoguear(int idAlumno, string waTo, string? mensaje)
        {
            try
            {
                _unitOfWork.WhatsAppMensajeEnviadoRepository.InsertarMensajesLogJsonEnvios(
                    idAlumno, waTo, mensaje ?? string.Empty);
            }
            catch { }
        }

        private async Task<RespuestaMensajeHookAtcDTO> UrlPostAsync(string urlBase, string jsonStringResult)
        {
            var respuestaMensajeHook = new RespuestaMensajeHookAtcDTO();
            try
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
                };
                using var client = new HttpClient(handler);
                var content  = new StringContent(jsonStringResult, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(urlBase, content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                respuestaMensajeHook = JsonConvert.DeserializeObject<RespuestaMensajeHookAtcDTO>(responseBody)!;
            }
            catch { }
            return respuestaMensajeHook;
        }
    }
}
