using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Planificacion.SCode.Service.Interface
{
    public interface IGestionDocenteFlujoService
    {
        Task<int> InsertarAsync(GestionDocenteFlujoDTO dto);
        Task<bool> ActualizarAsync(GestionDocenteFlujoDTO dto);
        Task<bool> EliminarAsync(int id, string usuario);
        Task<List<GestionDocenteFlujoDTO>> ObtenerTodoAsync();
    }
}
