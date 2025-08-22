using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Operaciones.Service.Interface
{
    public interface IResumenGrabacionOnlineService
    {
        IEnumerable<ResumenGrabacionOnlineDTO> ObtenerResumenGrabacionOnline();
        ResumenGrabacionOnlineDTO ObtenerResumenGrabacionOnlinePorId(int id);
        ProcesamientoTipoGenerarDTO ObtenerProcesamientoTipoGenerarPorId(int id);
    }
}
