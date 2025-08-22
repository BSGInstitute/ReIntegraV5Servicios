
using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;


namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampaniaGeneralSmsRepository
    {

        public bool InsertarCampaniaGeneralSms(StringDTO nombre, string Usuario);
        public CampaniaGeneralSmsDTO ObtenerCampaniaGeneralSms(IdDTO id);
        public bool ActualizarCampaniaGeneralSms(ActualizarCampaniaGeneralSmsDTO json);

        List<ObtenerCampaniaGeneralGrillaSmsDTO> ObtenerCampaniaGeneralGrillaSms();
        bool EliminarCampaniaGeneralSms(EliminarCampaniaGeneralSmsDTO json);
        List<ObtenerCampaniaGeneralDetalleSmsGrupoDTO> ObtenerCampaniaGeneralDetalleSms(IdDTO id);
        bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaSmsDTO json);
        bool EliminarCampaniaGeneralDetalleSms(EliminarCampaniaGeneralDetalleSmsDTO json);
        bool InsertarCampaniaGeneralDetalleSms(InsertarCampaniaGeneralDetalleSmsDTO json);
        bool ActualizarCamposCampaniaGeneralDetalleSms(ActualizarCamposCampaniaGeneralDetalleSmsDTO json);
        ObtenerConfiguracionCampaniaGeneralDetalleSmsDTO ObtenerConfiguracionCampaniaGeneralDetalleSms(IdDTO id);
        List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadSmsDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id);
        bool EliminarCampaniaGeneralDetalleResponsableSms(EliminarCampaniaGeneralDetalleResponsableSmsDTO json);
        ValorDevueltoDTO InsertarCampaniaGeneralDetalleResponsableSms(InsertarCampaniaGeneralDetalleResponsableSmsDTO json);
        bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueSmsDTO json);
        ComboCampaniaGeneralDetalleResponsableSmsDTO ObtenerComboCampaniaGeneralDetalleResponsableSms();
        ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue();
        List<AlumnoInformacionBasicaDTO> ObtenerDatosAlumno(List<int> ListaAlumnos);
        List<AlumnoSmsMasivoBaseDTO> ObtenerAlumnoConfiguradoPorPrioridad(PrioridadDatosSmsDTO obj);
        bool InsertarCampaniaGeneralDetalleExcelSms(InsertarCampaniaGeneralDetalleSmsDTO json);
        bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoSmsDTO json);
        List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue();
        List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id);
        ObtenerDatosPorPrioridadAsignadaDTO ObtenerDatosPorPrioridadAsignada(int IdCampaniaGeneralDetalleResponsableSms);
        bool InsertarCampaniaGeneralDetalleResponsableAlumnoSms(InsertarCampaniaGeneralDetalleResponsableAlumnoSmsDTO json);
        bool SumaChatValidoSms(SumaValidadorChatSmsDTO json);
        bool RestaChatValidoSms(SumaValidadorChatSmsDTO json);
        bool SumaChatInValidoSms(SumaValidadorChatSmsDTO json);
        bool RestaChatInValidoSms(SumaValidadorChatSmsDTO json);
        int ObtenerProgramaGeneral(int IdCentroCosto);
        public List<PlantillaSmsDato> ObtenerPlantillaSms();
        public List<ObtenerPrioridadesEnvioSmsDTO> ObtenerPrioridadesEnvioSms();
        public List<EmailAsesoresDto> ObtenerListaAsesores();
        public List<CampaniaGeneralDetalleResponsableAlumnoLogSmsDTO> ObtenerLogActivoCampaniaGeneralDetalleResponsableSms(int IdCampaniaGeneralDetalleResponsableSms);
        public bool EliminarLogSms(int Id, string Usuario);
        public int InsertarLogSms(int IdCampaniaGeneralDetalleResponsableSms, string HoraEnvio, string FechaInicioEnvioSms, string Usuario);
        public List<PreCampaniaGeneralDetalleResponsableAlumnoSmsDTO> PreListaSmsEnvioMasivo(int IdCampaniaGeneralDetalleResponsableSms);
        public DetalleCampaniaDTO ObtenerDetalleDeCampaniaSms(int IdcampaniaGeneralDetalleResponsableSms);
        public List<IdLogDTO> logsActivos(int IdCampaniaGeneralDetalleResponsableSms);
        public List<SmsEnviarMensajeDTO> ObtenerDataEnvio(int IdcampaniaGeneralDetalleResponsableSms);
        public IdDTO InsertarPlantillaSms(PlantillaSmsDTO datos);
        public bool InsertarDetalllePlantillaSms(DetallePlantillaSmsDTO datos);
        public List<ObtenerPlantillaSmsGrillaDTO> ObtenerPlantilla();
        public List<ObtenerDetallePlantillaSmsDTO> ObtenerDetallePlantilla(IdDTO id);
        public bool ActualizarPlantillaSms(ActualizarPlantillaSmsDTO datos);
        public bool EliminarPlantillaSms(IdDTO datos);
        public StringDTO GenerarUrlFormulariosLink(GenerarFormularioDTO datos, string usuario);
        public bool InsertarRespuestaSmsEnvio(respuestaMensajeSmsHook datos, string usuario);
        public bool InsertarAlumnoEnviado(InsertarResponsableAlumnoEnviadoSms datos, string usuario);
        public bool InsertarPruebaPlantillaSms(PruebaPlantillaSmsDTO datos);
        public bool InsertarAlumnoErroneo(InsertarResponsableAlumnoEnviadoSms datos, string usuario);
        public List<GrillaSms> ObtenerGrillaSms(int tab, int dia);
        public List<ChatSms> ObtenerChatPorAlumno(string celular);
        public List<DatosAlumno> ObtenerAlumnoPorCelular(string celular);
        public bool ValidarEnvioDuplicado(string CelularWhatsApp);
        public CondifuracionEnvioSmsDTO ObtenerConfiguracionEnvio();

    }
}