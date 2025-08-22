using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class MoodleWebServiceRegistrarMatriculaDTO
    {
        public int userid { get; set; }
        public int courseid { get; set; }
        public int roleid { get; set; }
        public long timestart { get; set; }
        public long timeend { get; set; }
    }

}
