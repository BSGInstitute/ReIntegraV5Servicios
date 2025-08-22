using BSI.Integra.Aplicacion.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    public class ProgramaGeneralPresentacionArgumento : BaseIntegraEntity
    {
        public int IdPgeneral { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool EsVisibleAgenda { get; set; }
        public List<ProgramaGeneralPresentacionArgumentoDetalleSolucion> ProgramaGeneralPresentacionArgumentoDetalleSolucion { get; set; } = new List<ProgramaGeneralPresentacionArgumentoDetalleSolucion>();
        public List<ProgramaGeneralPresentacionArgumentoModalidad> ProgramaGeneralPresentacionArgumentoModalidad { get; set; } = new List<ProgramaGeneralPresentacionArgumentoModalidad>();
    }
}
