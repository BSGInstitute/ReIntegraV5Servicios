using System;
using System.Collections.Generic;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    /// <summary>
    /// DTO para enviar correo a través de Gmail SMTP
    /// </summary>
    public class EnviarCorreoGmailDTO
    {
        public string EmailDestinatario { get; set; }
        public string EmailRemitente { get; set; }
        public string PersonalRemitente { get; set; }
        public string ClaveAplicacion { get; set; }
        public string Asunto { get; set; }
        public string MensajeHTML { get; set; }
    }

    /// <summary>
    /// DTO de respuesta del envío de correo
    /// </summary>
    public class ResultadoEnvioCorreoDTO
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaEnvio { get; set; }
        public string Error { get; set; }
    }
}
