using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Linkedin
{
    public class GrupoCampanasLinkedInDTO
    {
        [JsonProperty("runSchedule")]
        public RunSchedule? RunSchedule { get; set; }

        [JsonProperty("test")]
        public bool? Test { get; set; }

        [JsonProperty("changeAuditStamps")]
        public ChangeAuditStamps? ChangeAuditStamps { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("servingStatuses")]
        public List<string>? ServingStatuses { get; set; }

        [JsonProperty("backfilled")]
        public bool? Backfilled { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("account")]
        public string? Account { get; set; }

        [JsonProperty("status")]
        public string? Status { get; set; }
    }
    public class RunSchedule
    {
        [JsonProperty("start")]
        public long Start { get; set; }
    }

    public class Created
    {
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public class LastModified
    {
        [JsonProperty("actor")]
        public string Actor { get; set; }

        [JsonProperty("time")]
        public long Time { get; set; }
    }

    public class ChangeAuditStamps
    {
        [JsonProperty("created")]
        public Created Created { get; set; }

        [JsonProperty("lastModified")]
        public LastModified LastModified { get; set; }
    }


    public class GroupCampaignLinkedInDTO
    {
        public long? Id { get; set; }
        public  string? name {  get; set; }
        public string? servingStatuses { get; set; }
        public string? status {  get; set; }
        public DateTime? lastModified { get; set; }
        
    }

}
