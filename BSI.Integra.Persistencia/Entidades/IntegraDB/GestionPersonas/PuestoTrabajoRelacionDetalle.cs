using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PuestoTrabajoRelacionDetalle : BaseIntegraEntity
    {
        public int IdPuestoTrabajoRelacion { get; set; }
        public int? IdPuestoTrabajoDependencia { get; set; }
        public int? IdPuestoTrabajoPuestoAcargo { get; set; }
        public int? IdPersonalAreaTrabajo { get; set; }
    }
}
