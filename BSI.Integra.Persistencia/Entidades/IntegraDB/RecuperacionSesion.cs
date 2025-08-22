using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class RecuperacionSesion : BaseIntegraEntity
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecificoSesion { get; set; }
        public int? IdMigracion { get; set; }
    }
}
