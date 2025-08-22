using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlQuestionnaire
    {
        public long Id { get; set; }
        public long Course { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public short Introformat { get; set; }
        public long Qtype { get; set; }
        public string Respondenttype { get; set; }
        public string RespEligible { get; set; }
        public byte RespView { get; set; }
        public long Opendate { get; set; }
        public long Closedate { get; set; }
        public byte Resume { get; set; }
        public byte Navigate { get; set; }
        public long Grade { get; set; }
        public long Sid { get; set; }
        public long Timemodified { get; set; }
        public byte Completionsubmit { get; set; }
        public byte Autonum { get; set; }
    }

}
