using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppUsuarioRepository : IGenericRepository<TWhatsAppUsuario>
    {
        #region Metodos Base
        TWhatsAppUsuario Add(WhatsAppUsuario entidad);
        TWhatsAppUsuario Update(WhatsAppUsuario entidad);
        bool Delete(int id, string usuario);
        #endregion
        public List<WhatsAppPersonalDTO> ObtenerListaPersonal();
        public List<WhatsAppUsuarioListaGrillaDTO> ObtenerCredencialesUsuario();
        public WhatsAppUsuarioDTO ObtenerCredencialUsuarioPorId(int idUsuario);
        public WhatsAppUsuarioDTO UsuarioWhatsAppValido(int idPersonal);
        WhatsAppUsuario ObtenerPorId(int id);
    }
}