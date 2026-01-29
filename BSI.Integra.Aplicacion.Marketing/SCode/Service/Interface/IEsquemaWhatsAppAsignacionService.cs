using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    public interface IEsquemaWhatsAppAsignacionService
    {
        Task<int> InsertarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaWhatsAppAsignacionRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<EsquemaWhatsAppAsignacionDTO> ObtenerPorIdAsync(int id);
        Task<List<EsquemaWhatsAppAsignacionDTO>> ListarAsync();
    }
}
