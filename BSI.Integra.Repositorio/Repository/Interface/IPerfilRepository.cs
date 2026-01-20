using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Perfiles (catálogo global)
    /// </summary>
    public interface IPerfilRepository
    {
        Task<int> InsertarAsync(PerfilRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(PerfilRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<List<PerfilDTO>> ListarAsync();
    }
}
