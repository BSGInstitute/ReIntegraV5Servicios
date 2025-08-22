using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendingblueContactosDTO
    {
        public class SendingContactosDTO
        {
            public string email { get; set; }
            public Dictionary<string, string> atributos { get; set; }
            public List<long?> listIds { get; set; }
            public List<long?> unlinkListIds { get; set; }
            public bool? emailBlacklisted { get; set; }
            public bool? smsBlacklisted { get; set; }
            public bool updateEnabled { get; set; }
            public List<string>? smtpBlacklistedSender { get; set; }
        }
        public class SendingContactosCrearRESTDTO
        {
            public string email { get; set; }
            public Dictionary<string, string> attributes { get; set; }
            public List<long?> listIds { get; set; }
            public List<long?> unlinkListIds { get; set; }
            public bool? emailBlacklisted { get; set; }
            public bool? smsBlacklisted { get; set; }
            public bool updateEnabled { get; set; }
            public List<string>? smtpBlacklistedSender { get; set; }
        }
        public class SendingContactosRESTDTO
        {
            public Dictionary<string, string> attributes { get; set; }
            public List<long?> listIds { get; set; }
            //public List<long?> unlinkListIds { get; set; }
            public bool? emailBlacklisted { get; set; }
            public bool? smsBlacklisted { get; set; }
        }
        public class SendingContactosDTOSQL
        {
            public string email { get; set; }
            public string atributos { get; set; }
            public List<long?> listIds { get; set; }
            public List<long?> unlinkListIds { get; set; }
            public bool? emailBlacklisted { get; set; }
            public bool? smsBlacklisted { get; set; }
            public bool updateEnabled { get; set; }
            public List<string> smtpBlacklistedSender { get; set; }
        }
        public class CreateContactosDTOSQL
        {
            public string email { get; set; }
            public string atributos { get; set; }
            public List<long?> listIds { get; set; }
            public List<long?> unlinkListIds { get; set; }
            public bool? emailBlacklisted { get; set; }
            public bool? smsBlacklisted { get; set; }
            public bool updateEnabled { get; set; }
            public List<string> smtpBlacklistedSender { get; set; }
        }
        public class ListUpdate
        {
            public long listId { get; set; }
            public long? folderId { get; set; }
            public string name { get; set; }
        }
        public class eliminarContacto
        {
            public string email { get; set; }
        }

        public class agregarListaContactosDTO
        {
            public string listaEmails { get; set; }
            public int nuevoIdLista { get; set; }
        }
    }
}