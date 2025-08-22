using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IWhatsAppMensajeEnviadoPostulanteRepository : IGenericRepository<TWhatsAppMensajeEnviadoPostulante>
    {
        #region Metodos Base
        TWhatsAppMensajeEnviadoPostulante Add(WhatsAppMensajeEnviadoPostulante entidad);
        TWhatsAppMensajeEnviadoPostulante Update(WhatsAppMensajeEnviadoPostulante entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppMensajeEnviadoPostulante> Add(IEnumerable<WhatsAppMensajeEnviadoPostulante> listadoEntidad);
        IEnumerable<TWhatsAppMensajeEnviadoPostulante> Update(IEnumerable<WhatsAppMensajeEnviadoPostulante> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        WhatsAppMensajeEnviadoPostulante? ObtenerPorId(int id);
        bool ValidarUltimaPlantillaEnviada(string plantilla, string numero);

        bool InsertarMensajesLogJsonEnvios(int? IdPostulante, string Numero, string Mensaje);

        //IEnumerable<WhatsAppMensajesPostulanteDTO> WhatsAppUltimoMensajeRecibidosChat(int IdPersonal);
    }
}
