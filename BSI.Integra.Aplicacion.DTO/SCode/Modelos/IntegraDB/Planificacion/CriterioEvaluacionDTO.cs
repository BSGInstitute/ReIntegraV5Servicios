using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CriterioEvaluacionDTO
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public int IdCriterioEvaluacionCategoria { get; set; }
        public int? IdFormaCalculoEvaluacion { get; set; }
        public int? IdFormaCalificacionEvaluacion { get; set; }
        public int? IdFormaCalculoEvaluacionParametro { get; set; }
        public List<CriterioEvaluacionTipoProgramaDTO>? CriterioEvaluacionTipoPrograma { get; set; }
        public List<CriterioEvaluacionModalidadCursoDTO>? CriterioEvaluacionModalidadCurso { get; set; }
        public List<CriterioEvaluacionTipoPersonaDTO>? CriterioEvaluacionTipoPersona { get; set; }
        public List<ParametroEvaluacionDTO>? ParametroEvaluacion { get; set; }
    }
}
