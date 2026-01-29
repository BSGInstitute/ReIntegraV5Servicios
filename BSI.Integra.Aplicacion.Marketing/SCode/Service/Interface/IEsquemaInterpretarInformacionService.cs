using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    public interface IEsquemaInterpretarInformacionService
    {
        Task<int> InsertarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<EsquemaInterpretarInformacionDetalleDTO> ObtenerPorIdAsync(int id);
        Task<List<EsquemaInterpretarInformacionDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion);
    }
}
