using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class FeedbackConfigurarGrupoPreguntaDTO
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public string Nombre { get; set; }
    }
    public class FeedbackComboDTO
    {
        public IEnumerable<ComboDTO> ProgramasGenerales { get; set; }
        public IEnumerable<PEspecificoPGeneralFiltroDTO> ProgramasEspecificos { get; set; }
        public IEnumerable<ComboDTO> FeedbackConfigurados { get; set; }
    }
    public class RegistroFeedbackConfigurarGrupoPreguntaDTO
    {
        public int Id { get; set; }
        public int IdFeedbackConfigurar { get; set; }
        public IEnumerable<int> ConfiguracionFeedbackProgramaGeneral { get; set; }
        public IEnumerable<int> ConfiguracionProgramaEspecifico { get; set; }
    }
}
