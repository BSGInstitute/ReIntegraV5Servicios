using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IResumenGrabacionOnlineRepository : IGenericRepository<TResumenGrabacionOnline>
    {
        IEnumerable<ResumenGrabacionOnlineDTO> ObtenerResumenGrabacionOnline();
        ResumenGrabacionOnlineDTO ObtenerResumenGrabacionOnlinePorId(int id);
        ProcesamientoTipoGenerarDTO ObtenerProcesamientoTipoGenerarPorId(int id);
    }
}
