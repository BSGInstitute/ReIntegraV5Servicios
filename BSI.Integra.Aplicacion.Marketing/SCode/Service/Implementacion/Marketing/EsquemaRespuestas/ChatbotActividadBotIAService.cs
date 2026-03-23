using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.EsquemaRespuestas;
using BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.EsquemaRespuestas;
using BSI.Integra.Repositorio.Repository.Interface.Marketing.EsquemaRespuestas;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Implementacion.Marketing.EsquemaRespuestas
{
    /// Autor: Miguel Valdivia
    /// Fecha: 18/03/2026
    /// Version: 1.0
    /// <summary>
    /// Servicio para la gestion de actividades Bot IA y sus numeros asociados.
    /// Orquesta el upsert de numeros en T_AsistenteMarketingWhatsAppAsignacion
    /// y la vinculacion con T_ChatbotActividadBotIANumero.
    /// </summary>
    public class ChatbotActividadBotIAService : IChatbotActividadBotIAService
    {
        private readonly IChatbotActividadBotIARepository _chatbotActividadBotIARepository;
        private readonly IEsquemaRespuestasRepository     _esquemaRespuestasRepository;

        public ChatbotActividadBotIAService(
            IChatbotActividadBotIARepository chatbotActividadBotIARepository,
            IEsquemaRespuestasRepository     esquemaRespuestasRepository)
        {
            _chatbotActividadBotIARepository = chatbotActividadBotIARepository;
            _esquemaRespuestasRepository     = esquemaRespuestasRepository;
        }

        public List<ChatbotActividadBotIAListadoDTO> ObtenerListadoChatbotActividadBotIA()
        {
            var actividades = _chatbotActividadBotIARepository.ObtenerListadoChatbotActividadBotIA();
            var numeros     = _chatbotActividadBotIARepository.ObtenerNumerosPorActividades();

            foreach (var actividad in actividades)
            {
                actividad.Numeros = numeros
                    .Where(n => n.IdChatbotActividadBotIA == actividad.IdChatbotActividadBotIA)
                    .Select(n => n.NumeroWhatsApp)
                    .ToList();
            }

            return actividades;
        }

        public void InsertarChatbotActividadBotIA(InsertarChatbotActividadBotIADTO request, string usuario)
        {
            var idChatbotActividadBotIA = _chatbotActividadBotIARepository.InsertarChatbotActividadBotIA(request, usuario);

            if (request.Modulo == "Whatsapp" && request.Numeros?.Count > 0)
                _VincularNumerosAActividad(idChatbotActividadBotIA, request.Numeros, request.Estado, usuario);
        }

        public void ActualizarChatbotActividadBotIA(ActualizarChatbotActividadBotIADTO request, string usuario)
        {
            _chatbotActividadBotIARepository.ActualizarChatbotActividadBotIA(request, usuario);

            if (!request.Estado)
                _chatbotActividadBotIARepository.DesactivarNumerosWhatsAppDeActividad(request.Id, usuario);

            _chatbotActividadBotIARepository.EliminarChatbotActividadBotIANumeros(request.Id, usuario);

            if (request.Modulo == "Whatsapp" && request.Numeros?.Count > 0)
                _VincularNumerosAActividad(request.Id, request.Numeros, request.Estado, usuario);
        }

        public void EliminarChatbotActividadBotIA(EliminarChatbotActividadBotIADTO request, string usuario)
        {
            _chatbotActividadBotIARepository.DesactivarNumerosWhatsAppDeActividad(request.Id, usuario);
            _chatbotActividadBotIARepository.EliminarChatbotActividadBotIANumeros(request.Id, usuario);
            _chatbotActividadBotIARepository.EliminarChatbotActividadBotIA(request.Id, usuario);
        }

        /// <summary>
        /// Upsert de cada numero en el catalogo WhatsApp y vinculacion a la actividad.
        /// </summary>
        private void _VincularNumerosAActividad(int idChatbotActividadBotIA, List<string> numeros, bool estado, string usuario)
        {
            foreach (var numero in numeros)
            {
                var idAsignacion = _esquemaRespuestasRepository.UpsertAsistenteWhatsAppAsignacion(
                    new UpsertAsistenteWhatsAppAsignacionDTO
                    {
                        NumeroWhatsApp   = numero,
                        EsquemaRespuesta = 1,
                        Estado           = estado
                    },
                    usuario
                );

                _chatbotActividadBotIARepository.InsertarChatbotActividadBotIANumero(
                    idChatbotActividadBotIA,
                    idAsignacion,
                    usuario
                );
            }
        }
    }
}
