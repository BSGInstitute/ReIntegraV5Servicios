using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Marketing.Configuracion;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.Marketing.Configuracion
{
    public interface IConfiguracionExternaRepository
    {
        Task<InteraccionPatchDTO> ObtenerEsquemaInteraccionAsync(int idChatbotEsquema);
    }
}
