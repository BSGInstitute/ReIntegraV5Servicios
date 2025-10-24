using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSubSolucionAsignada : BaseIntegraEntity
    {
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; }
    }
}
