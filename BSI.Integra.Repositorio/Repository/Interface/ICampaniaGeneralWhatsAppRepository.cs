using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.WhatsApp;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;


namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface ICampaniaGeneralWhatsAppRepository : IGenericRepository<TCampaniaGeneralWhatsApp>
    {
        #region Metodos Base
        TCampaniaGeneralWhatsApp Add(CampaniaGeneralWhatsApp entidad);
        TCampaniaGeneralWhatsApp Update(CampaniaGeneralWhatsApp entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TCampaniaGeneralWhatsApp> Add(IEnumerable<CampaniaGeneralWhatsApp> listadoEntidad);
        IEnumerable<TCampaniaGeneralWhatsApp> Update(IEnumerable<CampaniaGeneralWhatsApp> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion

        public bool InsertarCampaniaGeneralWhatsApp(StringDTO nombre, string Usuario);
        public CampaniaGeneralWhatsAppDTO ObtenerCampaniaGeneralWhatsApp(IdDTO id);
        public bool ActualizarCampaniaGeneralWhatsApp(ActualizarCampaniaGeneralWhatsAppDTO json);

        List<ObtenerCampaniaGeneralGrillaWhatsAppDTO> ObtenerCampaniaGeneralGrillaWhatsApp();
        bool EliminarCampaniaGeneralWhatsApp(EliminarCampaniaGeneralWhatsAppDTO json);
        List<ObtenerCampaniaGeneralDetalleWhatsAppGrupoDTO> ObtenerCampaniaGeneralDetalleWhatsApp(IdDTO id);
        bool ActualizarActivarMasivoPorCampania(ActualizarActivarMasivoPorCampaniaDTO json);
        bool EliminarCampaniaGeneralDetalleWhatsApp(EliminarCampaniaGeneralDetalleWhatsAppDTO json);
        bool InsertarCampaniaGeneralDetalleWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json);
        bool ActualizarCamposCampaniaGeneralDetalleWhatsApp(ActualizarCamposCampaniaGeneralDetalleWhatsAppDTO json);
        ObtenerConfiguracionCampaniaGeneralDetalleWhatsAppDTO ObtenerConfiguracionCampaniaGeneralDetalleWhatsApp(IdDTO id);
        List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridad(IdDTO id);
        bool EliminarCampaniaGeneralDetalleResponsableWhatsApp(EliminarCampaniaGeneralDetalleResponsableWhatsAppDTO json);
        ValorDevueltoDTO InsertarCampaniaGeneralDetalleResponsableWhatsApp(InsertarCampaniaGeneralDetalleResponsableWhatsAppDTO json);
        bool ProcesarDataPorPrioridadSendinblue(ProcesarDataPorPrioridadSendinblueDTO json);
        ComboCampaniaGeneralDetalleResponsableWhatsAppDTO ObtenerComboCampaniaGeneralDetalleResponsableWhatsApp();
        ObtenerComboCampaniasSendinBlueDTO ObtenerComboCampaniasSendinBlue();
        List<AlumnoInformacionBasicaDTO> ObtenerDatosAlumno(List<int> ListaAlumnos);
        List<AlumnoWhatsAppMasivoBaseDTO> ObtenerAlumnoConfiguradoPorPrioridad(PrioridadDatosDTO obj);
        bool InsertarCampaniaGeneralDetalleExcelWhatsApp(InsertarCampaniaGeneralDetalleWhatsAppDTO json);
        bool ProcesarDataPorPrioridadExcel(ProcesarDataPorPrioridadExcelAlumnoDTO json);
        List<ObtenerComboCentroCostoCampaniasSendinBlueDTO> ObtenerComboCentroCostoCampaniasSendinBlue();

        List<ReporteInteraccionCampaniaGeneralDetalleDTO> ReporteInteraccionCampaniaGeneralDetalle(IdDTO id);
        ObtenerDatosPorPrioridadAsignadaDTO ObtenerDatosPorPrioridadAsignada(int IdCampaniaGeneralDetalleResponsableWhatsApp);
        bool InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsApp(InsertarCampaniaGeneralDetalleResponsableAlumnoWhatsAppDTO json);
        bool SumaChatValidoWhatsApp(SumaValidadorChatDTO json);
        bool RestaChatValidoWhatsApp(SumaValidadorChatDTO json);
        bool SumaChatInValidoWhatsApp(SumaValidadorChatDTO json);
        bool RestaChatInValidoWhatsApp(SumaValidadorChatDTO json);
        bool SumaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json);
        bool RestaOportunidadWhatsApp(SumaOportunidadWhatsAootDTO json);
        int ObtenerProgramaGeneral(int IdCentroCosto);
        ObtenerComboRespuestaWhatsAppp ObtenerComboRespuestaWhatsAppp();
        string ObtenerPlantillaWhatsApp(int IdPlantilla);

        public List<ProgramaprobabilidadDTO> ObtenerProgramaProbabilidadAlumno(int idAlumno, int top);
        public NombreDTO OntenerUltimoProgramaInteresado(int idAlumno);
        public DiasWhatsappDTO ObtenerDiasPorPrioridadWhatsapp(IdDTO id);
        public List<ObtenerCampaniaGeneralDetalleResponsablePorPrioridadDTO> ObtenerCampaniaGeneralDetalleResponsablePorPrioridadAlterno(IdDiasWhatsappDTO datos);
        public string ObtenerUltimoMensajeCampaniaEnviado(string celularAlumno);

    }
}
