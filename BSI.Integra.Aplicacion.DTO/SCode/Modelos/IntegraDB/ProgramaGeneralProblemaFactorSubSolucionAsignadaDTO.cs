using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaFactorSubSolucionAsignadaDTO
    {
        public int Id { get; set; }
        public int IdProgramaGeneralProblemaDetalle { get; set; }
        public int IdProgramaGeneralProblemaFactorSubSolucion { get; set; } 
    }
}
