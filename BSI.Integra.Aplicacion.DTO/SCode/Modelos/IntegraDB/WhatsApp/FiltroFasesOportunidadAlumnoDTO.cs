using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp
{
    public class FiltroFasesOportunidadAlumnoDTO    
    {
        public int? IdAlumno { get; set; }
        public string FasesOportunidad { get; set; }
        public DateTime HoraMinima { get; set; }
        public int ConsiderarEnviados { get; set; }

    }
}
