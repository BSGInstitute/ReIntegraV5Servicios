using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Marketing.Service.Interface
{
    public interface IRegistroRecuperacionWhatsAppService
    {
        TRegistroRecuperacionWhatsApp Add(RegistroRecuperacionWhatsAppDTO entidad);
        TRegistroRecuperacionWhatsApp Update(RegistroRecuperacionWhatsAppDTO entidad);
        TRegistroRecuperacionWhatsApp Update(TRegistroRecuperacionWhatsApp entidad);
        bool Delete(int id, string usuario);
        IEnumerable<TRegistroRecuperacionWhatsApp> Add(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad);
        IEnumerable<TRegistroRecuperacionWhatsApp> Update(IEnumerable<RegistroRecuperacionWhatsAppDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);

        bool ActualizarCompletadoRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable);
        int ObtenerCantidadCaidaRecuperacionWhatsApp();
        int ObtenerCantidadWhatsAppPreprocesadoRealizado(int idCampaniaGeneralDetalle, int idPersonal, DateTime fechaInicio, DateTime fechaFin);
        bool ActualizarFalloRegistroWhatsApp(int idCampaniaGeneralDetalle, int idCampaniaGeneralDetalleResponsable);
        bool DesactivarCompletadoRegistroWhatsApp(string usuario);
        IEnumerable<RegistroRecuperacionWhatsAppDTO> GetBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter);
        TRegistroRecuperacionWhatsApp FirstBy(Expression<Func<TRegistroRecuperacionWhatsApp, bool>> filter);
        List<TRegistroRecuperacionWhatsApp> Insert(List<RegistroRecuperacionWhatsAppDTO> registroSeguimientoRecuperacion);
    }
}
