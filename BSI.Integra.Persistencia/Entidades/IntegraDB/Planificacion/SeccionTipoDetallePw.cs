using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class SeccionTipoDetallePw : BaseIntegraEntity
    {
        public int IdSeccionPw { get; set; }
        public string NombreTitulo { get; set; } = null!;
        public int? IdSeccionTipoContenido { get; set; }
    }
}
