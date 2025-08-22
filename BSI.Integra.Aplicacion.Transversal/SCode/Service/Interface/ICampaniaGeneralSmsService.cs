using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
//using static BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.FiltroTipoCampaniaGeneralSmsDTO;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface ICampaniaGeneralSmsService
    {
        public bool InsertarCampaniaGeneralSms(StringDTO nombre, string Usuario);
        public CampaniaGeneralSmsDTO ObtenerCampaniaGeneralSms(IdDTO id);
        public bool ActualizarCampaniaGeneralSms(ActualizarCampaniaGeneralSmsDTO json);
        List<ObtenerCampaniaGeneralGrillaSmsDTO> ObtenerCampaniaGeneralGrillaSms();
        bool EliminarCampaniaGeneralSms(EliminarCampaniaGeneralSmsDTO json);
        List<ObtenerCampaniaGeneralDetalleSmsDTO> ObtenerCampaniaGeneralDetalleSms(IdDTO id);
        bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaSmsDTO json);
        bool EliminarCampaniaGeneralDetalleSms(EliminarCampaniaGeneralDetalleSmsDTO json);
        bool InsertarCampaniaGeneralDetalleSms(InsertarCampaniaGeneralDetalleSmsDTO json);
        bool ActualizarCamposCampaniaGeneralDetalleSms(ActualizarCamposCampaniaGeneralDetalleSmsDTO json);
        ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO ObtenerConfiguracionCampaniaGeneralDetalleSms(IdDTO id);
        ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAgrupadoSmsDTO ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id);
        bool EliminarCampaniaGeneralDetalleResponsableSms(EliminarCampaniaGeneralDetalleResponsableSmsDTO json);
        bool InsertarCampaniaGeneralDetalleResponsableSms(InsertarCampaniaGeneralDetalleResponsableSmsDTO json, string usuario);
        bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueSmsDTO json);
        ComboCampaniaGeneralDetalleResponsableSmsDTO ObtenerComboCampaniaGeneralDetalleResponsableSms();
        ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue();
        bool InsertarCampaniaGeneralDetalleExcelSms(InsertarCampaniaGeneralDetalleSmsDTO json);
        bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoSmsDTO json);
        List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue();
        List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id);
        public List<PlantillaSmsDato> ObtenerPlantillaSms();
        public bool EjecutarCampaniaGeneralEnvioSms();
        public IdDTO InsertarPlantillaSms(PlantillaSmsDTO datos);
        public bool InsertarDetalllePlantillaSms(DetallePlantillaSmsDTO datos);
        public List<ObtenerPlantillaSmsGrillaDTO> ObtenerPlantilla();
        public List<ObtenerDetallePlantillaSmsDTO> ObtenerDetallePlantilla(IdDTO id);
        public bool ActualizarPlantillaSms(ActualizarPlantillaSmsDTO datos);
        public bool EliminarPlantillaSms(IdDTO datos);
        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario);
        public List<GrillaSms> ObtenerGrillaSms(int tab, int dia);
        public List<ChatSms> ObtenerChatPorAlumno(string celular);
        public List<DatosAlumno> ObtenerAlumnoPorCelular(string celular);
        public CondifuracionEnvioSmsDTO ObtenerConfiguracionEnvio();

    }
}