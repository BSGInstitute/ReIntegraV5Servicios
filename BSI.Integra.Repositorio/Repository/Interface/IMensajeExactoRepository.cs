using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz para repositorio de Mensajes Exactos (catálogo global)
    /// </summary>
    public interface IMensajeExactoRepository
    {
        /// <summary>
        /// Inserta un nuevo mensaje exacto
        /// </summary>
        /// <param name="entidad">Datos del mensaje exacto</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>Id del mensaje exacto creado</returns>
        Task<int> InsertarAsync(MensajeExactoRequestDTO entidad, string usuario);

        /// <summary>
        /// Actualiza un mensaje exacto existente
        /// </summary>
        /// <param name="entidad">Datos del mensaje exacto</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>Número de filas afectadas</returns>
        Task<int> ActualizarAsync(MensajeExactoRequestDTO entidad, string usuario);

        /// <summary>
        /// Elimina lógicamente un mensaje exacto
        /// </summary>
        /// <param name="id">Id del mensaje exacto</param>
        /// <param name="usuario">Usuario que realiza la operación</param>
        /// <returns>Número de filas afectadas</returns>
        Task<int> EliminarAsync(int id, string usuario);

        /// <summary>
        /// Lista todos los mensajes exactos activos
        /// </summary>
        /// <returns>Lista de mensajes exactos</returns>
        Task<List<MensajeExactoDTO>> ListarAsync();
    }
}
