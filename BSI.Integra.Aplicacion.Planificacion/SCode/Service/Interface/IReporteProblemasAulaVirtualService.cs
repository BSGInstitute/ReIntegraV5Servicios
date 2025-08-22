using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.Service.Interface
{
    public interface IReporteProblemasAulaVirtualService
    {
        ReporteProblemaAulaVirtuaCombolDTO ObtenerCombos();
        IEnumerable<ReporteProblemasAulaVirtualResultadoDTO> ReporteProblemasAulaVirtual(ReporteProblemasAulaVirtualFiltroDTO filtro);
    }
}
