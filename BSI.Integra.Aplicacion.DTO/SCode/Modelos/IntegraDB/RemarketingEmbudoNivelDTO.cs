using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class RemarketingEmbudoNivelDTO : BaseIntegraEntity
    {
        public int IdRemarketingEmbudoEsquema { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string DescripcionGeneral { get; set; }
        public string DescripcionDetalle { get; set; }
        public string Orden { get; set; }
    }
}
