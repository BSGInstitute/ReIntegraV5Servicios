using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.CampaniaGeneralDetalleSubAreaWhatsapp;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using System.Linq.Expressions;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionEnvioDetalleRepository : IGenericRepository<TWhatsAppConfiguracionEnvioDetalle>
    {

        #region Metodos Base
        TWhatsAppConfiguracionEnvioDetalle Add(WhatsAppConfiguracionEnvioDetalleDTO entidad);
        TWhatsAppConfiguracionEnvioDetalle Update(WhatsAppConfiguracionEnvioDetalleDTO entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TWhatsAppConfiguracionEnvioDetalle> Add(IEnumerable<WhatsAppConfiguracionEnvioDetalleDTO> listadoEntidad);
        IEnumerable<TWhatsAppConfiguracionEnvioDetalle> Update(IEnumerable<WhatsAppConfiguracionEnvioDetalleDTO> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public MensajesWhatsAppRespondidosDTO ObtenerMensajesWhatsAppRespondidos(FiltroMensajesWhatsAppRespondidosDTO filtro);
        public int InsertarWhatsAppConfiguracionEnvioDetalle(WhatsAppConfiguracionEnvioDetalle filtro);
        public List<ObtenerReporteMensajesWhatsAppPorTipoDTO> ObtenerReporteMensajesWhatsApp(ReporteMensajesWhatsAppFiltrosDTO filtro);
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosPorArea(ReporteMensajesWhatsAppPorAreaFiltrosDTO filtro);
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivos(ReporteMensajesWhatsAppFiltrosDTO filtro);
        public List<ReporteWhatsAppEnvioMasivoDTO> GenerarReporteMensajesMasivosConjuntoLista(ReporteWhatsAppMasivoFiltrosDTO filtro);




    }
}