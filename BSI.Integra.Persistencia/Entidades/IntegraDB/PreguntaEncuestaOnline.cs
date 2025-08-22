using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PreguntaEncuestaOnline : BaseIntegraEntity
    {

        public int IdPreguntaEncuesta { get; set; }
        public int IdEncuestaOnline { get; set; }
        public bool Estado { get; set; }
    }
    
}
