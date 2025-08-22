using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CriterioEvaluacion : BaseIntegraEntity
    {
        [StringLength(50)]
        public string Nombre { get; set; } = null!;
        public int IdCriterioEvaluacionCategoria { get; set; }
        public int? IdFormaCalculoEvaluacion { get; set; }
        public int? IdFormaCalificacionEvaluacion { get; set; }
        public int? IdFormaCalculoEvaluacionParametro { get; set; }
        public List<CriterioEvaluacionModalidadCurso>  CriterioEvaluacionModalidadCurso  { get; set; }
        public List<CriterioEvaluacionTipoPersona>  CriterioEvaluacionTipoPersona  { get; set; }
        public List<CriterioEvaluacionTipoPrograma>  CriterioEvaluacionTipoPrograma  { get; set; } 
        public List<ParametroEvaluacion> ParametroEvaluacion { get; set; }
    }
}
