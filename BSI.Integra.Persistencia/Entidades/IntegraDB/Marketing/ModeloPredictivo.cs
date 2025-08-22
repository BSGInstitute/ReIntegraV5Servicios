using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Marketing
{
    public class ModeloPredictivo : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public decimal PeIntercepto { get; set; }
        public int PeEstado { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
