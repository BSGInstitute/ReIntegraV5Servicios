using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ParametroEvaluacion : BaseIntegraEntity
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdEscalaCalificacion { get; set; } 
        [StringLength(200)]
        public string Nombre { get; set; } = null!;
        public int Ponderacion { get; set; }
    }
}
