using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPresentacionArgumentoDetalleSolucion : BaseIntegraEntity
    {
        public int IdProgramaGeneralPresentacionArgumento{ get; set; }
        public string? Detalle { get; set; } = null!;
        public string? Solucion { get; set; } = null!;
        public int? IdPgeneral { get; set; }
    }
}
