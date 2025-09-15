using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Comercial
{
    public class TransicionFaseCriterioOportunidad : BaseIntegraEntity
    {
        public int IdTransicionFaseOportunidad { get; set; }
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
    }
}
