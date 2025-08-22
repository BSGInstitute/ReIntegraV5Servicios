using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppUsuarioCredencialRepository : IGenericRepository<TWhatsAppUsuarioCredencial>
    {
        #region Metodos Base
        TWhatsAppUsuarioCredencial Add(WhatsAppUsuarioCredencial entidad);
        TWhatsAppUsuarioCredencial Update(WhatsAppUsuarioCredencial entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppUsuarioCredencial> Add(IEnumerable<WhatsAppUsuarioCredencial> listadoEntidad);
        IEnumerable<TWhatsAppUsuarioCredencial> Update(IEnumerable<WhatsAppUsuarioCredencial> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        CredencialTokenExpiraDTO ValidarCredencialesUsuario(int idPersonal, int idPais);
        CredencialUsuarioLoginDTO ObtenerCredencialUsuarioLogin(int idPersonal);
        public CredencialUsuarioLoginDTO CredencialUsuarioLogin(int idPersonal);
        int InsertarWhatsAppUsuarioCredencial(TWhatsAppUsuarioCredencial filtro);
        IEnumerable<WhatsAppHostDatosDTO> ObtenerCredencialesHost();
    }
}
