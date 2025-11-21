using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralWhatsAppDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICampaniaGeneralWhatsAppService
    {
        #region Metodos Base
        CampaniaGeneralWhatsApp Add(CampaniaGeneralWhatsApp entidad);
        CampaniaGeneralWhatsApp Update(CampaniaGeneralWhatsApp entidad);
        bool Delete(int id, string usuario);

        List<CampaniaGeneralWhatsApp> Add(List<CampaniaGeneralWhatsApp> listadoEntidad);
        List<CampaniaGeneralWhatsApp> Update(List<CampaniaGeneralWhatsApp> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion


        public bool InsertarCampaniaGeneralWhatsApp(StringDTO nombre, string Usuario);
        public CampaniaGeneralWhatsAppDTO ObtenerCampaniaGeneralWhatsApp(IdDTO id);
        public bool ActualizarCampaniaGeneralWhatsApp(ActualizarCampaniaGeneralWhatsAppDTO json);
        List<ObtenerCampaniaGeneralGrillaWhatsAppDTO> ObtenerCampaniaGeneralGrillaWhatsApp();
        bool EliminarCampaniaGeneralWhatsApp(EliminarCampaniaGeneralWhatsAppDTO json);
        List<ObtenerCampaniaGeneralDetalleWhatsAppDTO> ObtenerCampaniaGeneralDetalleWhatsApp(IdDTO id);
        bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaDTO json);
        bool EliminarCampaniaGeneralDetalleWhatsApp(EliminarCampaniaGeneralDetalleWhatsAppDTO json);
        bool InsertarCampaniaGeneralDetalleWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json);
        bool ActualizarCamposCampaniaGeneralDetalleWhatsApp(ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO json);
        ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(IdDTO id);
        ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id);
        bool EliminarCampaniaGeneralDetalleResponsableWhatsApp(EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO json);
        bool InsertarCampaniaGeneralDetalleResponsableWhatsApp(InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO json, string usuario);
        bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueDTO json);
        ComboCampaniaGeneralDetalleResponsableWhatsAppDTO ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp();
        ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue();
        bool InsertarCampaniaGeneralDetalleExcelWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json);
        bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoDTO json);

        List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue();
        List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id);
        bool SumaChatValidoWhatsApp(SumaValidadorChatDTO json);
        bool RestaChatValidoWhatsApp(SumaValidadorChatDTO json);
        bool SumaChatInValidoWhatsApp(SumaValidadorChatDTO json);
        bool RestaChatInValidoWhatsApp(SumaValidadorChatDTO json);
        bool SumaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json);
        bool RestaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json);
        ObtenerComboRespuestaWhatsAppp ObtenerComboRespuestaWhatsAppp();
        bool EnvioMensajePorPlantilla(WhatsAppPlantillaDTO json, string numeroIdentificador = null);
        bool EnvioMensajePorTexto(WhatsAppMensajeTextoDTO json, string numeroIdentificador = null);
        public DiasWhatsappDTO ObtenerDiasPorPrioridadWhatsapp(IdDTO id);
        public ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(IdDiasWhatsappDTO datos);
        public bool EnvioMensajePorTextoFacebook(WhatsAppMensajeTextoFacebookDTO json);
        public string ObtenerUltimoMensajeCampaniaEnviado(string celularAlumno);
        public string ObtenerNumeroIdentificadorWhatsAppPorIdPersonal(int IdPersonal);
    }
}
