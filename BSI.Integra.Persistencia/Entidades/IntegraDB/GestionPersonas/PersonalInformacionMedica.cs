using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalInformacionMedica : BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int? IdTipoSangre { get; set; }
        public string Alergia { get; set; }
        public string Precaucion { get; set; }
    }
}
