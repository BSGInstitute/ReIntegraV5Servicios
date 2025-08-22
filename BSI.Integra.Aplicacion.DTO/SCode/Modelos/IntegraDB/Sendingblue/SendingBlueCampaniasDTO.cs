using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Sendingblue
{
    public class SendingBlueCampaniasDTO
    {
        public class GetEmailCampaignsDTO
        {
            public string? type { get; set; }
            public string? status { get; set; }
            public string? startDate { get; set; }
            public string? endDate { get; set; }
            public long? limit { get; set; }
            public long? offset { get; set; }
        }

        public class Params
        {
            public string FNAME { get; set; }
            public string LNAME { get; set; }
        }

        public class Recipients
        {
            public List<long?> exclusionListIds { get; set; }
            public List<long?> listIds { get; set; }
        }

        public class SetEmailCampaignsDTO
        {
            public string tag { get; set; }
            public Sender sender { get; set; }
            public string name { get; set; }
            public string htmlContent { get; set; }
            public string htmlUrl { get; set; }
            public int? templateId { get; set; }
            public string scheduledAt { get; set; }
            public string subject { get; set; }
            public string replyTo { get; set; }
            public string toField { get; set; }
            public Recipients recipients { get; set; }
            public string attachmentUrl { get; set; }
            public bool? inlineImageActivation { get; set; }
            public bool? mirrorActive { get; set; }
            public string footer { get; set; }
            public string header { get; set; }
            public string utmCampaign { get; set; }
            public Params _params { get; set; }
            public bool sendAtBestTime { get; set; }
            public bool abTesting { get; set; }
            public string subjectA { get; set; }
            public string subjectB { get; set; }
            public int splitRule { get; set; }
            public string winnerCriteria { get; set; }
            public int winnerDelay { get; set; }
            public bool ipWarmupEnable { get; set; }
            public int initialQuota { get; set; }
            public int increaseRate { get; set; }
            public string unsubscriptionPageId { get; set; }
            public string updateFormId { get; set; }
        }

        public class Sender
        {
            public string name { get; set; }
            public string email { get; set; }
            public int id { get; set; }
        }
        public class CrearContactosListaDto
        {
            public List<string> email { get; set; }
            public long idList { get; set; }
        }

        public class ListaIdsDtos
        {
            public string? listIds { get; set; }
        }

    }
}
