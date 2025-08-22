using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WhatsAppMensajeEnviadoAutomaticoDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp.WhatsAppMensajeEnviadoAutomaticoDTO;
using WhatsAppResultadoConjuntoListaDTO = BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsAppResultadoConjuntoListaDTO;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IRegistroRecuperacionWhatsAppRepository
    {

        #region Metodos Base
        TRegistroRecuperacionWhatsApp Add(RegistroRecuperacionWhatsAppDTO entidad);
        TRegistroRecuperacionWhatsApp Update(RegistroRecuperacionWhatsAppDTO entidad);
        TRegistroRecuperacionWhatsApp Update(TRegistroRecuperacionWhatsApp entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRegistroRecuperacionWhatsApp> Add(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad);
        IEnumerable<TRegistroRecuperacionWhatsApp> Update(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        bool ActualizarCompletadoRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable);
        int ObtenerCantidadCaidaRecuperacionWhatsApp();
        int ObtenerCantidadWhatsAppPreprocesadoRealizado(int idCampaniaGeneralDetalle, int idPersonal, DateTime fechaInicio, DateTime fechaFin);
        bool ActualizarFalloRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable);
        bool DesactivarCompletadoRegistroWhatsApp(string usuario);
        IEnumerable<RegistroRecuperacionWhatsAppDTO> GetBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter);
        TRegistroRecuperacionWhatsApp FirstBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter);
        List<TRegistroRecuperacionWhatsApp> Insert(List<RegistroRecuperacionWhatsAppDTO> registroSeguimientoRecuperacion);
        public List<AsesoresMktDTO> ListaAsesoresMarketing();


    }
}
