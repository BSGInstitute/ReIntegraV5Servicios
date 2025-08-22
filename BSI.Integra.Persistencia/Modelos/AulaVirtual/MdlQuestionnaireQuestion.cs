using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlQuestionnaireQuestion
    {
        public long Id { get; set; }
        public long SurveyId { get; set; }
        public string Name { get; set; }
        public long TypeId { get; set; }
        public long? ResultId { get; set; }
        public long Length { get; set; }
        public long Precise { get; set; }
        public long Position { get; set; }
        public string Content { get; set; }
        public string Required { get; set; }
        public string Deleted { get; set; }
        public long Dependquestion { get; set; }
        public long Dependchoice { get; set; }
    }
}
