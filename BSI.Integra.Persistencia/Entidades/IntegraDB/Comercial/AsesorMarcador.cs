using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class AsesorMarcador :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public bool MarcadorActivo { get; set; }
    }
}
