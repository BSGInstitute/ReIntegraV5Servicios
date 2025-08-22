using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class SubAreaParametroSeoPw : BaseIntegraEntity
    {
        public string Descripcion { get; set; } = null!;
        public int IdSubAreaCapacitacion { get; set; }
        public int IdParametroSeoPw { get; set; }
    }
}
