using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PlantillaPais : BaseIntegraEntity
    {
        public int IdPlantilla { get; set; }
        public int IdPais { get; set; }
    }
}
