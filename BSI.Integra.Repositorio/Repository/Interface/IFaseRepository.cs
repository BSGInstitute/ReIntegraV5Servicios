using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Fases (catálogo global)
    /// </summary>
    public interface IFaseRepository
    {
        Task<int> InsertarAsync(FaseRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(FaseRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<List<FaseDTO>> ListarAsync();
    }
}
