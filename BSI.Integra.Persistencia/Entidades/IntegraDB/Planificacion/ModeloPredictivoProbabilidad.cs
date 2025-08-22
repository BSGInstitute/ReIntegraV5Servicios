using BSI.Integra.Aplicacion.Base;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ModeloPredictivoProbabilidad : BaseIntegraEntity
    {

        public int IdModeloPredictivoTipo { get; set; }

        public int Tipo { get; set; }

        public int IdOportunidad { get; set; }

        public decimal Probabilidad { get; set; }

        public virtual TOportunidad IdOportunidadNavigation { get; set; } = null!;
    }
}
