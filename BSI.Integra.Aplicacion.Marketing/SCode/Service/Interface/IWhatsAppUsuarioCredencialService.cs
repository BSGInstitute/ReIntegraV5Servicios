using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppUsuarioCredencialService
    {
        #region Metodos Base
        WhatsAppUsuarioCredencial Add(WhatsAppUsuarioCredencial entidad);
        WhatsAppUsuarioCredencial Update(WhatsAppUsuarioCredencial entidad);
        bool Delete(int id, string usuario);

        List<WhatsAppUsuarioCredencial> Add(List<WhatsAppUsuarioCredencial> listadoEntidad);
        List<WhatsAppUsuarioCredencial> Update(List<WhatsAppUsuarioCredencial> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion
        CredencialTokenExpiraDTO ValidarCredencialesUsuario(int idPersonal, int idPais);
        CredencialUsuarioLoginDTO CredencialUsuarioLogin(int idPersonal);
    }
}
