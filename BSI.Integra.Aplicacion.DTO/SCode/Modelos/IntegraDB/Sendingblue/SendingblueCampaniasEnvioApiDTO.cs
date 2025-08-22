using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendingblueCampaniasEnvioApiDTO
    {
        public class Recipients
        {
            public List<int> listIds { get; set; }
        }
        public class RecipientsCambiosSendinBlue
        {
            public List<int> lists { get; set; }
        }

        public class CrearCampaniaSendinblue
        {
            public Sender sender { get; set; }
            public string name { get; set; }
            public int templateId { get; set; }
            public string scheduledAt { get; set; }
            public string subject { get; set; }
            public string replyTo { get; set; }
            public string toField { get; set; }
            public Recipients recipients { get; set; }
        }

        public class Sender
        {
            public string name { get; set; }
            public string email { get; set; }
        }
        public class CrearCampaniaSendinblueABTest
        {
            public Sender sender { get; set; }
            public string name { get; set; }
            public int templateId { get; set; }
            public string scheduledAt { get; set; }
            public string subject { get; set; }
            public string replyTo { get; set; }
            public string toField { get; set; }
            public Recipients recipients { get; set; }
            public bool abTesting { get; set; }
            public string subjectA { get; set; }
            public string subjectB { get; set; }
            public int splitRule { get; set; }
            public string winnerCriteria { get; set; }
            public int winnerDelay { get; set; }
        }

        public class CrearCampaignEmailHtmlContentDTO
        {
            public Sender sender { get; set; }
            public string name { get; set; }
            public string htmlContent { get; set; }
            public string scheduledAt { get; set; }
            public string subject { get; set; }
            public string replyTo { get; set; }
            public string toField { get; set; }
            public Recipients recipients { get; set; }
        }

        public class CrearCampaniaSendinblueABTestHtmlContent
        {
            public Sender sender { get; set; }
            public string name { get; set; }
            public string htmlContent { get; set; }
            public string scheduledAt { get; set; }
            public string subject { get; set; }
            public string replyTo { get; set; }
            public string toField { get; set; }
            public Recipients recipients { get; set; }
            public bool abTesting { get; set; }
            public string subjectA { get; set; }
            public string subjectB { get; set; }
            public int splitRule { get; set; }
            public string winnerCriteria { get; set; }
            public int winnerDelay { get; set; }
        }


    }
}
