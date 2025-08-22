using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPerfilEscalaProbabilidad : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string Nombre { get; set; }
        public double ProbabilidadInicial { get; set; }
        public double ProbabilidadActual { get; set; }
        public int Orden { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
