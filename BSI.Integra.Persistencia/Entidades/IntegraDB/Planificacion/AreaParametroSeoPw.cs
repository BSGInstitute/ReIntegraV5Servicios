using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class AreaParametroSeoPw : BaseIntegraEntity
    {
        public string Descripcion { get; set; }
        public int IdAreaCapacitacion { get; set; }
        public int IdParametroSeopw { get; set; }
    }
}
