using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class CriterioEvaluacionTipoPrograma : BaseIntegraEntity
    {
        public int IdCriterioEvaluacion { get; set; }
        public int IdTipoPrograma { get; set; }
    }
}
