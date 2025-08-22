using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PaginaWebPw : BaseIntegraEntity
    {
        public string Nombre { get; set; } = null!;
        public string ServidorVinculado { get; set; } = null!;
    }
}
