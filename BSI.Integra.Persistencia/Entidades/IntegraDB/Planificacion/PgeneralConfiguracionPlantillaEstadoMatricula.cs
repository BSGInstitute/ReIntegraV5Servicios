using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralConfiguracionPlantillaEstadoMatricula : BaseIntegraEntity
    {
        public int IdPgeneralConfiguracionPlantillaDetalle { get; set; }
        public int IdEstadoMatricula { get; set; }
    }
}
