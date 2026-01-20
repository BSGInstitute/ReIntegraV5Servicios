using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Esquema Principal
    /// </summary>
    public interface IEsquemaWhatsAppAsignacionRepository
    {
        Task<int> InsertarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<EsquemaWhatsAppAsignacionDTO> ObtenerPorIdAsync(int id);
        Task<List<EsquemaWhatsAppAsignacionDTO>> ListarAsync();
    }
}
