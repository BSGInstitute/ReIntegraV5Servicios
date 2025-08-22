using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PlantillaRevisionPw : BaseIntegraEntity
    {
        public int IdPlantillaPw { get; set; }
        public int IdRevisionNivelPw { get; set; }
        public int IdPersonal { get; set; }
    }
}
