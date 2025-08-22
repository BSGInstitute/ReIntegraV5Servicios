using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IWhatsAppConfiguracionLogEjecucionService
    {
        IEnumerable<WhatsAppConfiguracionLogEjecucion> GetBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter);
        WhatsAppConfiguracionLogEjecucion FirstById(int id);
        WhatsAppConfiguracionLogEjecucion FirstBy(Expression<Func<TWhatsAppConfiguracionLogEjecucion, bool>> filter);
        bool Insert(WhatsAppConfiguracionLogEjecucion objetoBO);
        bool Insert(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO);
        bool Update(WhatsAppConfiguracionLogEjecucion objetoBO);
        bool Update(IEnumerable<WhatsAppConfiguracionLogEjecucion> listadoBO);
        void AsignacionId(TWhatsAppConfiguracionLogEjecucion entidad, WhatsAppConfiguracionLogEjecucion objetoBO);
        TWhatsAppConfiguracionLogEjecucion MapeoEntidad(WhatsAppConfiguracionLogEjecucion objetoBO);
        int VerificadEnvioDuplicado(string Celular);
        int InsertarWhatsappConfiguracionLogEjecucion(WhatsAppConfiguracionLogEjecucion filtro);
        bool ActualizarWhatsappConfiguracionLogEjecucionFechaFin(WhatsAppConfiguracionLogEjecucion filtro);
        int ObtenerLogActivo(int IdWhasAppConfiguracionLogEjecucion);
        int obtenerOtrosLogActivos(int idwhatsappConfiguracionEnvio);

    }
}
