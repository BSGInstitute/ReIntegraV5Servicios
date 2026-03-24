using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using System.Threading.Tasks;

namespace BSI.Integra.Aplicacion.Marketing.SCode.Service.Interface.Marketing.Configuracion
{
    public interface IConfiguracionExternaService
    {
        Task<ConfiguracionApiResponseDTO> SincronizarEsquemaInteraccionAsync(int idChatbotEsquema);
    }
}
