using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    /// <summary>
    /// Interfaz para servicio de Mensajes Exactos (catálogo global)
    /// </summary>
    public interface IMensajeExactoService
    {
        Task<int> InsertarAsync(MensajeExactoRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(MensajeExactoRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<List<MensajeExactoDTO>> ListarAsync();
    }
}
