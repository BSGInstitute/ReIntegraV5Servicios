using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class TipoPagoCategoria : BaseIntegraEntity
    {
        public int IdCategoriaPrograma { get; set; }
        public int IdTipoPago { get; set; }
        public int IdModoPago { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
