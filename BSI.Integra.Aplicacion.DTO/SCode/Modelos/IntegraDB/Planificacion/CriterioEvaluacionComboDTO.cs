using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CriterioEvaluacionComboDTO
    {
  
        public IEnumerable<ComboDTO> TipoPersona { get; set; }
        public IEnumerable<ComboDTO> ModalidadCurso { get; set; }
        public IEnumerable<ComboDTO> CriterioEvaluacionCategorium { get; set; }
        public IEnumerable<ComboDTO> EscalaCalificacion { get; set; }
        public IEnumerable<ComboDTO> FormaCalculoEvaluacion { get; set; }
        public IEnumerable<ComboDTO> FormaCalificacionEvaluacion { get; set; }
        public IEnumerable<ComboDTO> TipoPrograma { get; set; }
    }
}
