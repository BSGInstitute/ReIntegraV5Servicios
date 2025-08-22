using BSI.Integra.Aplicacion.Base;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class PEspecificoConsumo : BaseIntegraEntity
    {
        public int? IdPespecificoSesion { get; set; }
        public int? IdHistoricoProductoProveedor { get; set; }
        public decimal Cantidad { get; set; }
        public string Factor { get; set; }
        public Guid? IdMigracion { get; set; }
    }
}
