using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IAvatarRepository : IGenericRepository<TAvatar>
    {
        AvatarDTO ObtenerAvatar(string usuario);
    }
}
