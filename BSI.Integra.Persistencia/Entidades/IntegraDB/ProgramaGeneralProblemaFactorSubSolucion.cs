using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSubSolucion : BaseIntegraEntity
    {
        public string Solucion { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public int Orden { get; set; }
        public int Nivel { get; set; }
    }
}
