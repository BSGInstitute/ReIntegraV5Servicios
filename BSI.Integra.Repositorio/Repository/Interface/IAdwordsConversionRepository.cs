using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    /// <summary>
    /// Interfaz: Repositorio de Conversiones de Google Ads
    /// Autor: Miguel
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

        /// <summary>
        /// Obtiene una subcuenta de Google Ads por su Customer ID
        /// </summary>
        Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaPorCustomerId(string customerId);

        /// <summary>
        /// Obtiene una subcuenta de Google Ads por su ID interno
        /// </summary>
        Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaPorId(int id);

        /// <summary>
        /// Obtiene todas las subcuentas activas de Google Ads
        /// </summary>
        Task<List<GoogleAdsSubcuentaDTO>> ObtenerSubcuentasActivas();

        /// <summary>
        /// Obtiene leads de Google sin subcuenta asignada para enriquecimiento
        /// </summary>
        Task<List<GoogleFormularioLeadgenDTO>> ObtenerLeadsSinSubcuentaAsignada(int limite);

        /// <summary>
        /// Actualiza la subcuenta asignada a un lead después de consultar Google Ads API
        /// </summary>
        Task ActualizarSubcuentaLead(int id, int idSubcuentaGoogle);
    }
}
