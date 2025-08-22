using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IWhatsAppUsuarioService
    {
        #region Metodos Base
        WhatsAppUsuario Add(WhatsAppUsuario entidad);
        WhatsAppUsuario Update(WhatsAppUsuario entidad);
        bool Delete(int id, string usuario);
        #endregion
        public List<WhatsAppPersonalDTO> ObtenerListaPersonal();
        public List<WhatsAppUsuarioListaGrillaDTO> ObtenerCredencialesUsuario();
        WhatsAppUsuario ObtenerPorId(int id);
    }
}
