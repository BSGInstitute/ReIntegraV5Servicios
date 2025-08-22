using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlQuestionnaireResponse
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public long Submitted { get; set; }
        public string Complete { get; set; }
        public long Grade { get; set; }
        public string Username { get; set; }
    }
}
