using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RemarketingEmbudoHistorico : BaseIntegraEntity
    {
        public int IdRemarketingEmbudoNivel { get; set; }
        public int IdAlumno { get; set; }
        public DateTime FechaClasificacion { get; set; }
    }
}
