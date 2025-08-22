using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    public class ModeloPredictivoEscalaProbabilidad : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int Orden { get; set; }
        public string Nombre { get; set; }
        public decimal ProbabilidaIinicial { get; set; }
        public decimal ProbabilidadActual { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
