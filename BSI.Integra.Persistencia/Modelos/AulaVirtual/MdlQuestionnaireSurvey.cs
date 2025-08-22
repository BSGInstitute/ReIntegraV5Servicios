using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlQuestionnaireSurvey
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string Realm { get; set; }
        public long Status { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Subtitle { get; set; }
        public string Info { get; set; }
        public string Theme { get; set; }
        public string ThanksPage { get; set; }
        public string ThankHead { get; set; }
        public string ThankBody { get; set; }
    }
}
