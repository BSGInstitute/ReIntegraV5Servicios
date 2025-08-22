using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlUserEnrolments
    {
        public long Id { get; set; }
        public long Status { get; set; }
        public long Enrolid { get; set; }
        public long Userid { get; set; }
        public long Timestart { get; set; }
        public long Timeend { get; set; }
        public long Modifierid { get; set; }
        public long Timecreated { get; set; }
        public long Timemodified { get; set; }
    }
}
