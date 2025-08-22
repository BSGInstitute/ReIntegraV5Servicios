using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendingblueSendersDTO
    {
        public class Ip
        {
            public string ip { get; set; }
            public string domain { get; set; }
            public int weight { get; set; }
        }

        public class SengindblueSenders
        {
            public string name { get; set; }
            public string email { get; set; }
            public List<Ip>? ips { get; set; }
        }
    }
}
