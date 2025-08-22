using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalCese : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int? IdMotivoCese { get; set; }
        public DateTime FechaCese { get; set; }
    }
}
