using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAvatarService
    {
        AvatarDTO ObtenerAvatar(string usuario);
    }
}
