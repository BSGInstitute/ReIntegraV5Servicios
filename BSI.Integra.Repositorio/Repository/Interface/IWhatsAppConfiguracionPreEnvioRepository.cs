using BSI.Integra.Aplicacion.DTO.Modelos;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IWhatsAppConfiguracionPreEnvioRepository
    {
        #region Metodos Base
        public TWhatsAppConfiguracionPreEnvio Add(WhatsAppConfiguracionPreEnvioDTO entidad);

        public TWhatsAppConfiguracionPreEnvio Update(WhatsAppConfiguracionPreEnvioDTO entidad);
        public TWhatsAppConfiguracionPreEnvio Update(TWhatsAppConfiguracionPreEnvio entidad);

        public bool Delete(int id, string usuario);
        public IEnumerable<TWhatsAppConfiguracionPreEnvio> Add(IEnumerable<WhatsAppConfiguracionPreEnvioDTO> listadoEntidad);
        public IEnumerable<TWhatsAppConfiguracionPreEnvio> Update(IEnumerable<WhatsAppConfiguracionPreEnvioDTO> listadoEntidad);
        public bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public IEnumerable<TWhatsAppConfiguracionPreEnvio> GetBy(Expression<Func<TWhatsAppConfiguracionPreEnvio, bool>> filter);
        public TWhatsAppConfiguracionPreEnvio FirstById(int id);
        public bool Insert(IEnumerable<TWhatsAppConfiguracionPreEnvio> listadoBO);
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle);
        public List<WhatsAppConfiguracionPreEnvioDTO> ListasWhatsAppEnvioAutomaticoMasivoPreProcesadaCampaniaGeneral(int cantidad, int idCampaniaGeneralDetalle, int idPersonal);
        public List<VistaWhatsAppConfiguracionPreEnvioDTO> ListasVisualizarWhatsAppEnvioAutomaticoMasivoPreProcesada(int IdConjuntoListaDetalle);
        public RegistroSeguimientoPreProcesoListaWhatsAppDTO RegistroSeguimientoPreProcesoListaWhatsApp(int IdConjuntoLista);
        public bool InsertarConfiguracionPreEnvioRepositorioMailingGeneral(List<WhatsAppConfiguracionPreEnvioBO> listaNuevoWhatsAppConfiguracionPreEnvio);
        bool RegistraPreValidacionCampaniaGeneral(List<WhatsAppResultadoCampaniaGeneralDTO> RegistrosProcesados, int IdPGeneral, int IdPlantilla);

        public List<PreCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO> PreListaWhatsAppEnvioMasivo(int IdCampaniaGeneralDetalleResponsableWhatsApp);
        public DetalleCampaniaDTO ObtenerDetalleDeCampaniaWhatsApp(int IdcampaniaGeneralDetalleResponsableWhatsApp);
        public List<IdLogDTO> logsActivos(int IdCampaniaGeneralDetalleResponsableWhatsApp);

        public Task<List<IdLogDTO>>logsActivosAsync(int IdCampaniaGeneralDetalleResponsableWhatsApp);
        public bool ValidarEnvioDuplicado(string CelularWhatsApp, int Dias);
        public bool ValidarDesuscritos(string CelularWhatsApp);
        public List<DetallePlantillasDTO> ObtenerDetallePlantillaWhatsApp(int idPlantilla);
        public bool InsertarCampaniaGeneralDetalleResponsableAlumnoEnviadoWhatsApp(string json, string WaId, int IdCampaniaGeneralDetalleResponsableLogWhatsApp);
        public bool InsertarMensajeEnviadoErroneoWhatsappLog(MensajeEnviadoErroneoWhatsappLogDTO datos);
    }
}
