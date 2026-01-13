using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface
{
    public interface IEsquemaRespuestaService
    {
        Task<int> InsertarAsync(EsquemaRespuestaRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaRespuestaActualizarDTO entidad, string usuario);
        Task<List<EsquemaRespuestaDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion);
    }
}
