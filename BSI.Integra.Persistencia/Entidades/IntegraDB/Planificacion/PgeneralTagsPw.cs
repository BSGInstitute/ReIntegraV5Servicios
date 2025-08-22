using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralTagsPw : BaseIntegraEntity
    {
        public int? IdPgeneral { get; set; }
        public int IdTagPW { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
