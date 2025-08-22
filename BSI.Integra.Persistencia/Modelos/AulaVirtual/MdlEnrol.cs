using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlEnrol
    {
        public long Id { get; set; }
        public string Enrol { get; set; }
        public long Status { get; set; }
        public long Courseid { get; set; }
        public long Sortorder { get; set; }
        public string Name { get; set; }
        public long? Enrolperiod { get; set; }
        public long? Enrolstartdate { get; set; }
        public long? Enrolenddate { get; set; }
        public byte? Expirynotify { get; set; }
        public long? Expirythreshold { get; set; }
        public byte? Notifyall { get; set; }
        public string Password { get; set; }
        public string Cost { get; set; }
        public string Currency { get; set; }
        public long? Roleid { get; set; }
        public long? Customint1 { get; set; }
        public long? Customint2 { get; set; }
        public long? Customint3 { get; set; }
        public long? Customint4 { get; set; }
        public long? Customint5 { get; set; }
        public long? Customint6 { get; set; }
        public long? Customint7 { get; set; }
        public long? Customint8 { get; set; }
        public string Customchar1 { get; set; }
        public string Customchar2 { get; set; }
        public string Customchar3 { get; set; }
        public decimal? Customdec1 { get; set; }
        public decimal? Customdec2 { get; set; }
        public string Customtext1 { get; set; }
        public string Customtext2 { get; set; }
        public string Customtext3 { get; set; }
        public string Customtext4 { get; set; }
        public long Timecreated { get; set; }
        public long Timemodified { get; set; }
    }
}
