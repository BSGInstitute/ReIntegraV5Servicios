namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class WhatsAppMensajeEnviadoApiAtcDTO
    {
        /// <summary>
        /// Respuesta del webhook ATC.
        /// { mensaje: "...", estadoMensaje: true, idWhatsappMensajeAtc: 9874 }
        /// </summary>
        public class RespuestaMensajeHookAtcDTO
        {
            public string Mensaje              { get; set; }
            public bool   EstadoMensaje        { get; set; }
            public int?    IdWhatsappMensajeAtc { get; set; }
        }

        /// <summary>
        /// Respuesta al frontend luego de procesar la validacion y envio.
        /// </summary>
        public class RespuestaMensajeAtcDTO
        {
            public bool   Estado  { get; set; }
            public string Mensaje { get; set; }
        }

        /// <summary>
        /// Resultado de la consulta de hilo chat abierto en ia.T_ChatbotWhatsAppAtcHiloChat.
        /// </summary>
        public class HiloChatAtcDTO
        {
            public int Id                          { get; set; }
            public int IdChatbotConversacionEstado { get; set; }
        }

        /// <summary>
        /// Payload del frontend para finalizar una conversacion activa.
        /// IdAlumno es el identificador primario; WaTo se usa como fallback cuando IdAlumno es 0.
        /// </summary>
        public class FinalizarConversacionDTO
        {
            public int    IdAlumno { get; set; }
            public string WaTo     { get; set; }
        }
    }
}
