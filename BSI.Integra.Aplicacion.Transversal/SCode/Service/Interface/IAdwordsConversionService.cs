using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GoogleAds;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
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
    }
}
