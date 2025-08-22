using BSI.Integra.Aplicacion.DTO.SCode.Modelos.Wolkbox.WolkboxAgent;
using System.Net;

namespace BSI.Integra.Aplicacion.Servicios.Service.Interface.Wolkbox
{
    public interface IWolkboxAgentService
    {
        Task<(object resultado, HttpStatusCode statusCode)> CambiarEstadoAgente(int idPersonal, string status);
        Task<(object resultado, HttpStatusCode statusCode)> ColgarTipificarReady(int idPersonal, WolkboxTipificarDTO model);
        Task<(object resultado, HttpStatusCode statusCode)> Colgar(int idPersonal);
        Task<(object resultado, HttpStatusCode statusCode)> SilenciarMicrofono(int idPersonal);
        Task<(object resultado, HttpStatusCode statusCode)> MarcacionDTMF(int idPersonal, string dtmf_tones);
        Task<(object resultado, HttpStatusCode statusCode)> Marcar(int idPersonal, WolkboxMarcarDTO model);
        Task<(object resultado, HttpStatusCode statusCode)> MostrarOcultarBotones(int idPersonal, DisplayButtonDTO model);
        Task<(object resultado, HttpStatusCode statusCode)> PausarLlamada(int idPersonal);
    }
}
