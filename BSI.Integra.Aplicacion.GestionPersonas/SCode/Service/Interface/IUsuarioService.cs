using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.GestionPersonas.Service.Interface
{
    public interface IUsuarioService
    {
        UsuarioDTO ObtenerPorNombreUsuario(string usuario);
        Usuario ObtenerTotalPorUsuario(string usuario);
        IEnumerable<GestionUsuarioDTO> ObtenerTodo();
        IEnumerable<ComboDTO> ObtenerCombo();
        bool InsertarUsuario(IntegraUsuarioDTO dto, string usuario);
        bool ActualizarUsuario(IntegraUsuarioDTO dto, string usuario);
    }
}
