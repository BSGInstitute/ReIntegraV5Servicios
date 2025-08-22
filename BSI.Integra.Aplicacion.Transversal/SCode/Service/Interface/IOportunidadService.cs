using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using Microsoft.AspNetCore.Http;
using static BSI.Integra.Aplicacion.Base.Enums.Enums;

namespace BSI.Integra.Aplicacion.Transversal.Service.Interface
{
    public interface IOportunidadService
    {
        #region Metodos Base
        Oportunidad Add(Oportunidad entidad);
        Oportunidad Update(Oportunidad entidad);
        bool Delete(int id, string usuario);

        List<Oportunidad> Add(List<Oportunidad> listadoEntidad);
        List<Oportunidad> Update(List<Oportunidad> listadoEntidad);
        bool Delete(List<int> listadoIds, string usuario);
        #endregion

        IEnumerable<Oportunidad> ObtenerOportunidad();
        IEnumerable<OportunidadComboDTO> ObtenerCombo();
        IEnumerable<OportunidadVentaCruzadaAgendaDTO> ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(int idClasificacionPersona);
        IEnumerable<OportunidadHistorialAgendaDTO> ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(int idClasificacionPersona);
        OportunidadCompuestoDTO ObtenerOportunidadCompuestoPorIdActividadDetalle(int idActividadDetalle);
        ResultadoVisualizarOportunidadDTO ValidarVisualizarAgendaPorIdOportunidad(int idOportunidad, int idPersonal);
        Oportunidad ObtenerPorId(int idOportunidad);
        string ObtenerCronogramaPagoCompleto(int idOportunidad);
        string ObtenerMontoTotal(int idOportunidad);
        string ObtenerVersion(int idOportunidad);
        OportunidadCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad);
        bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia);
        DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramacionAutomatica(int idOportunidad);
        ActividadTrabajadaDTO ValidarFaseOportunidad(int idOportunidad, int idFaseOportunidad, int idActividadDetalle);
        bool ExistePorId(int idOportunidad);
        ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivo(int idOportunidad);
        object ObtenerPorFiltroRegistrarOportunidad(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador);
        object ObtenerPorFiltroRegistrarOportunidadV2(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, string usuario, string area, string tipoPersonal);
        void EnviarCorreoOportunidad(int idOportunidad);
        int ObtenerCentroCostoPorCelularAlumno(string numero, int idPersonal);
        OportunidadBandejaCorreoDTO ObtenerOportunidadPorAsesorYAlumno(int idAlumno, int idPersonal, string numero);
        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManual(AsignacionManualOportunidadFiltroDTO obj, FiltroKendoGridDTO filter, List<OperadoresComparacionDTO> operadorComparacion);
        public void ProgramaActividad(bool CheckSegProg);
        public void FinalizarActividades(bool mantenerestadooportunidadNuevaEntidad, string Usuario);
        OportunidadOperacionesFiltroDTO ObtenerOportunidadOperacionesPorParametros(int idMatriculaCabecera);
        OportunidadDTO GenerarOportunidadOperacionesConParametros(int idOportunidad, string usuario, int idCentroCosto, int idActividadCabecera, int idPersonal, int idMatriculaCabecera);
        void InsertarOportunidadClasificacionOperaciones(int idOportunidad);
        bool EliminarOportunidadFisicaOperacionesV3V4(int idOportunidad);
        List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidades(int idAlumno, int idClasificacionPersona);
        List<OportunidadHistorialDTO> CargarOportunidadHistorial(int idAlumno, int idClasificacionPersona);
        List<ObtenerSeguimientoAlumnoComentarioDTO> ObtenerComentariosOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria);
        List<ValidarOportunidadWhatsAppDTO> ValidarOportunidadesWhatsApp(string numero, int idPGeneral);
        Oportunidad MapeoBoDTO(OportunidadBoDTO objetoBO);
        OportunidadTabAgendaDTO ObtenerClasificacionTabAgenda(int idPersonal, string textoaBuscar, int tipo);
        public object ObtenerPorFiltroRevertirFase(RevertirFaseFiltroDTO obj, PaginadorDTO paginador, List<GridFilterDTO> filter);
        void GenerarOportunidadOperaciones();
        bool TienePersonalOperaciones(int idOportunidad);
        IdDTO ObtenerIdPersonalOperaciones(int idOportunidad);
        DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorActividades(int idActividadDetalle);
        public object RevertirOportunidad(RevertirFaseOportunidadFiltroDTO Obj);
        ResultadoFiltroAsignacionOportunidadDTO ObtenerPorFiltroPaginaManualOperaciones(PaginadorDTO paginador, AsignacionManualOportunidadOperacionesFiltroDTO filtro, GridFiltersDTO filterGrid, List<string> listaCodigoMatricula);
        EmailPersonalAlumnoDTO ObtenerEmailPorOportunidad(int idOportunidad);
        List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidadesReporte(int idAlumno);
        List<OportunidadProblemaClienteDTO> ObtenerOportunidadProblemasCliente(int idOportunidad);
        ReporteSeguimientoOportunidadComplementosDTO ObtenerInformacionComplementariaReporteSeguimiento(int idOportunidad);
        List<OportunidadAnteriorDTO> ObtenerOportunidadesAnterioresAlumno(int idAlumno);
        DatosOportunidadReprogramacionManualOperacionesDTO ObtenerCasosReprogramacionManualOperacionesAlterno(int idOportunidad);
        DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramcionAutomatica(int idOportunidad);
        DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO ObtenerCalculoReprogramaciones(int idOportunidad);
        DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO ObtenerSubEstadoAlumno(int idOportunidad);
        DatosAlumnoDTO ObtenerDatosAlumno(int idOportunidad);
        DatosAlumnoOportunidadDTO ObtenerDatosOportunidadAlumno(int idAlumno);
        void CrearOportunidad(ref OportunidadBoDTO oportunidad, bool flagVentaCruzada, TipoPersona tipoPersona);
        int ObtenerAsesorVentaCruzada(int idAlumno);
        void ActualizarAsignacionOportunidad(int IdOportunidad, int idAsesorReasignacion, int IdCentroCosto, int IdAlumno, string Usuario);
        List<Oportunidad> ObtenerPorIdAlumno(int idAlumno);
        string CapturaDeOportunidades(int idAlumno);
        bool ProcesarOportunidadesPortalWeb();
        string CrearOportunidadesPortalWeb(int idAsignacion);
        List<OportunidadProgramadaManualDTO> CalcularProgramacionManualConsecutivos();
        public int ValidarFormulario(int IdAsignacionAutomaticaTemp, int OrigenDato);

        public int CrearOportunidadWebHookFacebook(int IdAsignacionAutomatica);
        public bool RegistrarLogError(RegistroLogDTO RegistroLog);
        public int ProcesarFormularioNuevoPortal(string IdRegistroPortalWeb, int IdPagina);
        bool InsertarEnviosWhatsappDiasSinContacto(int idOportunidad);
        bool ActualizarAlumnoCrearOportunidadVentas(RegistroOportunidadAlumnoDTO formulario);
        bool CrearOportunidadCrearAlumnoVentas(RegistroOportunidadAlumnoDTO formulario);
        object ProcesarOportunidadesMkt(IFormFile file, string usuario);
        List<InformacionBaseOportunidad> ProcesarInformacionOportunidades(List<InformacionBaseOportunidad> datos, string usuario);
        List<InformacionBaseOportunidad> ProcesarInformacionOportunidadesLinkedIn(List<InformacionBaseOportunidad> datos, string usuario);
        List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO> ObtenerComentariosOperacionesPagosAcademicos2(int idOportunidad);
        public List<int> CrearOportunidadesWebHookFacebookLista(List<int> idAsignacionAutomaticaList);
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivoMarketing(int idOportunidad, int tipo);
        public void ObtenerProbabilidadTodosProgramasPorAlumno(int idAlumno);
        object ProcesarOportunidadesMktVersionLinkedIn(IFormFile file, string usuario);
        public List<ArchivoOportunidadDTO> ObtenerArchivosOportunidad();
        public void CrearOportunidadMarketing(ref OportunidadBoDTO Oportunidad);




    }
}
