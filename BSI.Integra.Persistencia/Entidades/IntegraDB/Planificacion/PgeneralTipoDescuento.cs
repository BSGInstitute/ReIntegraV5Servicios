using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PgeneralTipoDescuento : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public int? IdTipoDescuento { get; set; }
        public bool? FlagPromocion { get; set; }
    }
}