using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class TipoReclamoAlumno : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;   
        public virtual ICollection<Reclamo> Reclamos { get; set; }
    }
}
