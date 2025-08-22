using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Configuracion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IIntegraAspNetUserRepository : IGenericRepository<TIntegraAspNetUser>
    {
        #region Metodos Base
        TIntegraAspNetUser Add(IntegraAspNetUser entidad);
        TIntegraAspNetUser Update(IntegraAspNetUser entidad);

        IEnumerable<TIntegraAspNetUser> Add(IEnumerable<IntegraAspNetUser> listadoEntidad);
        IEnumerable<TIntegraAspNetUser> Update(IEnumerable<IntegraAspNetUser> listadoEntidad);
        #endregion
        ComboDTO ObtenerIdentidadUsusario(string usuario);
        IntegraAspNetUser ObtenerIdPersonalPorUsuario(string usuario);
        AccesoIpConfiguracionDTO? ObtenerAccesoPorIp(string ipPublica);
        StringDTO ValidarReLogin(string usuario);
        StringDTO ActualizarReLogin(string usuario);
        IEnumerable<IntegraAspNetUserDTO> ObtenerPorUsuario(string usuarioNombre);
        List<ModuloAgrupacionDTO> ObtenerDatosParaModuloAgrupado(string usuario);
        IntegraAspNetUser ObtenerPorIdPersonal(int perId);
        IntegraAspNetUser ObtenerPorNombreUsuario(string nombreUsuario);
        bool ExistePorNombreUsuario(string nombreUsuario);

        string ObtenerEmailPorNombreUsuario(string nombreUsuario);
        bool InsertarIntegraAspNetUser(UserIntegraAspNetDTO dto, string creador);
        bool ActualizarIntegraAspNetUser(UserIntegraAspNetDTO dto, string modificador);
        DatoPersonalDTO ObtenerIdentidadUsusarioV2(string usuario);
        IntegraAspNetUser ObtenerPorId(int perId);
         string ObtenerEmailFiltro(string nombreUsuario);
    }
}
