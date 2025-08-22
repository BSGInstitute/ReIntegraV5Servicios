using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICustomAuthenticationManagerRepository
    {
        AspNetUserAutenticateDTO AutenticacionUsuarioPortal(string UserName, string UsClave);
    }
}
