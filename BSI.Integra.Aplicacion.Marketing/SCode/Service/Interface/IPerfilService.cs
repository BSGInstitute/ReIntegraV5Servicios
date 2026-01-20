using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    public interface IPerfilService
    {
        Task<int> InsertarAsync(PerfilRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(PerfilRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<List<PerfilDTO>> ListarAsync();
    }
}
