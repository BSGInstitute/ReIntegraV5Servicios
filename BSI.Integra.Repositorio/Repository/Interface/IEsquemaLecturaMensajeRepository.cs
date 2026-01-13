using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Lectura de Mensajes (Clasificaciones)
    /// </summary>
    public interface IEsquemaLecturaMensajeRepository
    {
        Task<int> InsertarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario);
        Task<int> ActualizarAsync(EsquemaLecturaMensajeRequestDTO entidad, string usuario);
        Task<int> EliminarAsync(int id, string usuario);
        Task<EsquemaLecturaMensajeDetalleDTO> ObtenerPorIdAsync(int id);
        Task<List<EsquemaLecturaMensajeDTO>> ListarPorEsquemaAsync(int idEsquemaWhatsAppAsignacion);
    }
}
