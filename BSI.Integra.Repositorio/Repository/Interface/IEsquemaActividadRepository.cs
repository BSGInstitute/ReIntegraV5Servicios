using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Actividad (vinculación número WhatsApp con esquema)
    /// </summary>
    public interface IEsquemaActividadRepository
    {
        Task<int> InsertarAsync(EsquemaActividadRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaActividadRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<int> ActualizarEstadoAsync(EsquemaActividadEstadoDTO entidad, string usuario);
        Task<List<EsquemaActividadDTO>> ListarAsync();
    }
}
