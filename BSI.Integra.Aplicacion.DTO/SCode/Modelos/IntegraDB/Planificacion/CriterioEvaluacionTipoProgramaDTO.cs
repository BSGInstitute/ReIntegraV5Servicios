using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class CriterioEvaluacionTipoProgramaDTO
    {
        public int? Id { get; set; } 
        public int IdCriterioEvaluacion { get; set; } 
        public int IdTipoPrograma { get; set; }
    }
}
