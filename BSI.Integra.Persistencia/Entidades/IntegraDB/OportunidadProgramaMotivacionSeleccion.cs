using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB
{
    public class OportunidadProgramaMotivacionSeleccion :BaseIntegraEntity
    {
        public int IdOportunidad { get; set; }
        public int IdProgramaMotivacion { get; set; }
        public int Prioridad { get; set; }
    }
}
