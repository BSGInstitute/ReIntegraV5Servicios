using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ProgramaGeneralProblemaFactorDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } 
    }



    public class ProgramaGeneralProblemaFactorCompletoDTO
    {
        public IEnumerable<ProgramaGeneralProblemaFactorDTO> ProblemaFactor { get; set; }
        public IEnumerable<ProgramaGeneralProblemaFactorDetalleDTO> ProblemaFactorDetalle { get; set; }
        public IEnumerable<ProgramaGeneralProblemaFactorSolucionDTO> ProblemaFactorSolucion { get; set; }

    }
}
