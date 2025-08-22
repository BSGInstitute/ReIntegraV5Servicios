using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class ConfiguracionResumenGrabacionOnline : BaseIntegraEntity
    {
        public int IdPEspecificoSesion { get; set; }
        public int IdResumenGrabacionOnline { get; set; }
    }
}
