using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz: Repositorio de Conversiones de Google Ads
    /// Autor: Sistema
    /// Fecha: 2025-10-04
    /// </summary>
    public interface IAdwordsConversionRepository
    {
        /// <summary>
        /// Obtiene las credenciales activas de Google Ads API
        /// </summary>
        Task<AdwordsCredencialesDTO?> ObtenerCredenciales();

        /// <summary>
        /// Obtiene las conversiones pendientes de envío
        /// </summary>
        /// <param name="limite">Número máximo de conversiones a obtener</param>
        Task<List<ConversionQueueDTO>> ObtenerConversionesPendientes(int limite);

        /// <summary>
        /// Actualiza el estado de una conversión después del intento de envío
        /// </summary>
        Task ActualizarEstadoConversion(int id, string estado, string? respuesta, string? error);

        /// <summary>
        /// Registra un log en T_AdwordsLog
        /// </summary>
        Task RegistrarLog(string mensaje, bool esExito);

        /// <summary>
        /// Obtiene el estado actual de las conversiones en la cola
        /// </summary>
        Task<List<ConversionEstadoDTO>> ObtenerEstadoConversiones();
    }
}
