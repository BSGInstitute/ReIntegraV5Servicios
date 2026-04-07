using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class PEspecificoSesionEstadoObservacion : BaseIntegraEntity
    {
        public string Descripcion { get; set; } = null!;
        public int IdPEspecificoSesionEstado { get; set; }
    }

}
