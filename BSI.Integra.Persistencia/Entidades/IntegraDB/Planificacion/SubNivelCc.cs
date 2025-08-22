using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class SubNivelCc : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string Codigo { get; set; } = null!;
        public int IdAreaCc { get; set; }
    }
}
