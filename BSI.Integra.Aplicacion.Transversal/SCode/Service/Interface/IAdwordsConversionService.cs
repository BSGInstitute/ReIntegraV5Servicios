using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    /// <summary>
    /// Interfaz: Servicio de Conversiones Offline de Google Ads
    /// Autor: Miguel
    /// Fecha: 2025-10-04
    /// </summary>
    public interface IAdwordsConversionService
    {
        /// <summary>
        /// Procesa y envía todas las conversiones pendientes a Google Ads
        /// </summary>
        Task<ConversionResultDTO> EnviarConversionesPendientes();

        /// <summary>
        /// Obtiene el estado actual de las conversiones en la cola
        /// </summary>
        Task<List<ConversionEstadoDTO>> ObtenerEstadoConversiones();

        /// <summary>
        /// Consulta la API de Google Ads para obtener la subcuenta de una campaña
        /// </summary>
        /// <param name="campaignId">ID de la campaña de Google Ads</param>
        Task<GoogleAdsSubcuentaDTO?> ObtenerSubcuentaAPI(string campaignId);
    }
}
