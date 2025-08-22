using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ParametroEvaluacionDTO
    {
        public int? Id { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public int Ponderacion { get; set; }
    }
}
