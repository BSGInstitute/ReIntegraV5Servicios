using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PEspecificoCriterioEvaluacion : BaseIntegraEntity
    {
        public int IdPEspecificoEsquema { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int Ponderacion { get; set; }
    }
}
