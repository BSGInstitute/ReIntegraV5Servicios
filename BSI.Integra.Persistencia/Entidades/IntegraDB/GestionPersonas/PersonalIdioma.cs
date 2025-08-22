using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalIdioma:BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public int IdIdioma { get; set; }
        public int IdNivelIdioma { get; set; }
        public int IdCentroEstudio { get; set; }
        public int? IdPersonalArchivo { get; set; }
    }
}
