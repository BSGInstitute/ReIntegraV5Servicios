using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class AgendaTipoUsuario : BaseIntegraEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
