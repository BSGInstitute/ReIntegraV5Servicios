using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendingblueRespuestaGenericaDTO
    {
        public class RespuestaGenerica
        {
            public string SendingblueRespuesta { get; set; }
            public ErrorGenerico error { get; set; }
        }
        public class ErrorGenerico
        {
            public bool Response { get; set; }
            public DetailError Detalle {get;set;}
        }
        public class DetailError
        {
            public string Codigo { get; set; }
            public string Mensaje { get; set; }
            public string Descripcion { get; set; }
        }
        public class SendingblueSendersRespuesta
        {
            public long? id { get; set; }
            public bool? spfError { get; set; }
            public bool? dkimError { get; set; }
            public ErrorGenerico error { get; set; }
        }
        public class SendingblueObtenerSenders
        {
            public List<SendingblueObtenerSender> senders { get; set; }
            public ErrorGenerico error { get; set; }
        }

        public class SendingblueObtenerIp
        {
            public string ip { get; set; }
            public string domain { get; set; }
            public int weight { get; set; }
        }

        public class SendingblueObtenerSender
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public bool active { get; set; }
            public List<SendingblueObtenerIp> ips { get; set; }
        }


    }
}
