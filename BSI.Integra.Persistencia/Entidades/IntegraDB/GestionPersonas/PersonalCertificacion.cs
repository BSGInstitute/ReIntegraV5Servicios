using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalCertificacion :BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public string Programa { get; set; }
        public string Institucion { get; set; }
        public DateTime FechaCertificacion { get; set; }
        public int? IdPersonalArchivo { get; set; }
        public int? IdCentroEstudio { get; set; }
    }
}
