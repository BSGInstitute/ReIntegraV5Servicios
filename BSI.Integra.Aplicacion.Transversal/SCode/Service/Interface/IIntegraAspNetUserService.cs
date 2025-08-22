using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IIntegraAspNetUserService
    {
        void ValidarAcceso(string ipPublica);
        StringDTO ValidarReLogin(string usuario);
        StringDTO ActualizarReLogin(string usuario);
        IEnumerable<IntegraAspNetUserDTO> ObtenerPorUsuario(string usuarioNombre);
        List<ModuloAgrupacionDTO> ObtenerDatosParaModuloAgrupado(string usuario);
        IntegraAspNetUser ObtenerPorId(int perId);
        IntegraAspNetUser ObtenerPorNombreUsuario(string nombreUsuario);
        string ObtenerEmailPorNombreUsuario(string nombreUsuario);

    }
}
