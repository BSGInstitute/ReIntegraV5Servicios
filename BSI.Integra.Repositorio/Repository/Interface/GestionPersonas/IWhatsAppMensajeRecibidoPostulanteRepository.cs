using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Entidades.IntegraDB.GestionPersonas;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface.GestionPersonas
{
    public interface IWhatsAppMensajeRecibidoPostulanteRepository : IGenericRepository<TWhatsAppMensajeRecibidoPostulante>
    {
        #region Metodos Base
        TWhatsAppMensajeRecibidoPostulante Add(WhatsAppMensajeRecibidoPostulante entidad);
        TWhatsAppMensajeRecibidoPostulante Update(WhatsAppMensajeRecibidoPostulante entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TWhatsAppMensajeRecibidoPostulante> Add(IEnumerable<WhatsAppMensajeRecibidoPostulante> listadoEntidad);
        IEnumerable<TWhatsAppMensajeRecibidoPostulante> Update(IEnumerable<WhatsAppMensajeRecibidoPostulante> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<WhatsAppMensajesPostulanteDTO> WhatsAppUltimoMensajeRecibidosChat(int IdPersonal);
        Boolean ValidarMensajeRecibido24Horas(string numero);
        List<WhatsAppHistorialPostulanteMensajesDTO> ListaHistorialMensajeChat(int idPersonal, string numero, string area);




    }
}
