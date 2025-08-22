using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Modelos.AulaVirtual
{
    public partial class MdlUser
    {
        public long Id { get; set; }
        public string Auth { get; set; }
        public byte Confirmed { get; set; }
        public byte Policyagreed { get; set; }
        public byte Deleted { get; set; }
        public byte Suspended { get; set; }
        public long Mnethostid { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Idnumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public byte Emailstop { get; set; }
        public string Icq { get; set; }
        public string Skype { get; set; }
        public string Yahoo { get; set; }
        public string Aim { get; set; }
        public string Msn { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Institution { get; set; }
        public string Department { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Lang { get; set; }
        public string Theme { get; set; }
        public string Timezone { get; set; }
        public long Firstaccess { get; set; }
        public long Lastaccess { get; set; }
        public long Lastlogin { get; set; }
        public long Currentlogin { get; set; }
        public string Lastip { get; set; }
        public string Secret { get; set; }
        public long Picture { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public byte Descriptionformat { get; set; }
        public byte Mailformat { get; set; }
        public byte Maildigest { get; set; }
        public byte Maildisplay { get; set; }
        public byte Htmleditor { get; set; }
        public byte Autosubscribe { get; set; }
        public byte Trackforums { get; set; }
        public long Timecreated { get; set; }
        public long Timemodified { get; set; }
        public long Trustbitmask { get; set; }
        public string Imagealt { get; set; }
    }
}
