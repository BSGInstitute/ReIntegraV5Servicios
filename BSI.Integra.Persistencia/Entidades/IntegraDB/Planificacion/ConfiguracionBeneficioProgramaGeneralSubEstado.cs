using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ConfiguracionBeneficioProgramaGeneralSubEstado : BaseIntegraEntity
    {
        public int IdConfiguracionBeneficioPgneral { get; set; }
        public int IdSubEstadoMatricula { get; set; }
    }
}
