using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class EsquemaEvaluacion_DetalleCalificacionDTO
    {
        public int IdCriterioEvaluacion { get; set; }
        public string CriterioEvaluacion { get; set; }
        public decimal Valor { get; set; }
        public int Ponderacion { get; set; }
        public int IdParametroEvaluacion { get; set; }
    }
}
