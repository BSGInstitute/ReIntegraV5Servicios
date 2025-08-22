using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class FormaCalculoEvaluacion : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public bool EsSuma { get; set; }
        public bool EsPromedio { get; set; }
        public virtual ICollection<TEsquemaEvaluacion> TEsquemaEvaluacions
        {
            get; set;
        }
    }
}
