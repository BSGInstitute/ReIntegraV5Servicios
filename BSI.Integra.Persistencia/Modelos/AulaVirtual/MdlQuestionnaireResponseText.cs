using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlQuestionnaireResponseText
    {
        public long Id { get; set; }
        public long ResponseId { get; set; }
        public long QuestionId { get; set; }
        public string Response { get; set; }
    }
}
