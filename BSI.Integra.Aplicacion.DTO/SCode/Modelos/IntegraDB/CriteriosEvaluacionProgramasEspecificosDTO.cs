using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class CriteriosEvaluacionProgramasEspecificosDTO
    {
        public IEnumerable<AreaCapacitacionFiltroDTO> listaArea { get; set; }
        public IEnumerable<SubAreaCapacitacionFiltroDTO> listaSubArea { get; set; }
        public IEnumerable<ProgramaGeneralSubAreaFiltroDTO> listaProgramaGeneral { get; set; }
        public IEnumerable<PEspecificoProgramaGeneralFiltroDTO> listaProgramaEspecifico { get; set; }
        public IEnumerable<CentroCostoProgramaEspecificoFiltroDTO> listaCentroCosto { get; set; }
        public IEnumerable<ComboDTO> listaCentroCostoPersonalizado { get; set; }
        public IEnumerable<FiltroDTO> listaEstadoProgramaEspecifico { get; set; }
        public IEnumerable<FiltroDTO> listaCiudad { get; set; }
    }
}
