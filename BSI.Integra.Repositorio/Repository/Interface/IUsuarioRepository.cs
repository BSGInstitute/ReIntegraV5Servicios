using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IUsuarioRepository : IGenericRepository<TUsuario>
    {
        TUsuario Add(Usuario entidad);
        TUsuario Update(Usuario entidad);
        bool Delete(int id, string usuario);
        UsuarioDTO ObtenerPorNombreUsuario(string usuario);
        Usuario ObtenerTotalPorUsuario(string usuario);
        IEnumerable<GestionUsuarioDTO> ObtenerTodo();
        IEnumerable<ComboDTO> ObtenerComboRol();
        Usuario ObtenerPorIdPersonal(int idPersonal);
    }
}
