using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICustomAuthenticationManagerService
    {
        //AspNetUserTokenDTO Authenticate(string username, string password);
        AspNetUserAutenticateDTO Authenticate(string username, string password);
        IDictionary<string, string> Tokens { get; }
    }
}
