using BSI.Integra.Aplicacion.Servicios.Service.Interface;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BSI.Integra.Aplicacion.Servicios.Service.Implementacion
{
    public class TMK_TwilioService : ITMK_TwilioService
    {
        //Valores estaticos
        private const string _accountSid = "AC789efd819d71fc20cb7817c404b21794";
        private const string _authToken = "2ed88f13596c956bea8451d13e47b8ee";
        private const string _origenNumber = "+14327550058";
        public TMK_TwilioService()
        {
            TwilioClient.Init(_accountSid, _authToken);
        }
        /// Autor: _ _ _ _ _ _ _ .
        /// Fecha: 29/09/2022
        /// Version: 1.0
        /// <summary>
        /// Envía mensaje de texto
        /// </summary>
        /// <returns> string: Identificador único del mensaje enviado </returns>
        public string EnviarMensajeTexto(string mensaje, string numeroDestino)
        {
            try
            {
                var message = MessageResource.Create(body: mensaje,
                                                            from: new Twilio.Types.PhoneNumber(_origenNumber),
                                                            to: new Twilio.Types.PhoneNumber(numeroDestino),
                                                            pathAccountSid: _accountSid);
                return message.Sid;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}
