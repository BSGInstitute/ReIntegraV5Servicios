using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Interaccion.Service.Interface
{
    /// Service: ILlamadasWhatsAppService
    /// Autor: WhatsApp Business Calling API integration
    /// Fecha: 2026-05-08
    /// <summary>
    /// Servicio de lectura del historial de llamadas de WhatsApp Business Calling.
    /// </summary>
    public interface ILlamadasWhatsAppService
    {
        LlamadasHistorialResultadoDTO ObtenerHistorial(LlamadasHistorialFiltroDTO filtro);
    }
}
