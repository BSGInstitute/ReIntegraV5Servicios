using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas
{
    public class PersonalHistorialMedico:BaseIntegraEntity
    {
        public int IdPersonal { get; set; }
        public string Enfermedad { get; set; }
        public string DetalleEnfermedad { get; set; }
        public string Periodo { get; set; }
    }
}
