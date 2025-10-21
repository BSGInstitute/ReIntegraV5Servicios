using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSubSolucionDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public string Solucion { get; set; }
        public int Orden { get; set; }
        public int Nivel { get; set; }
    }
}
