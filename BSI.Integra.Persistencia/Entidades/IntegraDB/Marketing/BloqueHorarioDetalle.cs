using BSI.Integra.Aplicacion.Base;
using Org.BouncyCastle.Asn1.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    public class BloqueHorarioDetalle : BaseIntegraEntity
    {
        public int IdBloqueHorario { get; set; }
        public Time HoraInicio { get; set; }
        public Time HoraFin { get; set; }
    }
}
