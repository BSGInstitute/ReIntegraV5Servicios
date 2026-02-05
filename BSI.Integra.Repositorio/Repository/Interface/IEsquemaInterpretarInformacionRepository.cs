using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Interpretar Información del Contacto
    /// </summary>
    public interface IEsquemaInterpretarInformacionRepository
    {
        Task<int> InsertarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaInterpretarInformacionRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<EsquemaInterpretarInformacionDetalleDTO> ObtenerPorIdAsync(int id);
        Task<List<EsquemaInterpretarInformacionDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion);
    }
}
