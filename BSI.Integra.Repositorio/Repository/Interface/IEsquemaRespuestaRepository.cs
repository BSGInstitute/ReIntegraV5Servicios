using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Respuestas (matriz de parámetros)
    /// </summary>
    public interface IEsquemaRespuestaRepository
    {
        Task<int> InsertarAsync(EsquemaRespuestaRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaRespuestaActualizarDTO entidad, string usuario);
        Task<List<EsquemaRespuestaDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion);
    }
}
