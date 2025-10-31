using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSolucionRespuestaDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
        public bool EsSolucionado { get; set; }
    }


    public class ProgramaGeneralProblemaFactorSolucionRespuestaEnvioDTO
    {
        public int Id { get; set; }
        public int IdOportunidad { get; set; }
        public int IdProgramaGeneralProblemaFactorSolucion { get; set; }
    }


}
