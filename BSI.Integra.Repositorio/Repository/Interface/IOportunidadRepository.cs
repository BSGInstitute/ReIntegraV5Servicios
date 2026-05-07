using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Aplicacion.DTO.SCode;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Comercial;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;
using Google.Api.Ads.AdWords.v201809;
using static iText.Signatures.LtvVerification;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IOportunidadRepository : IGenericRepository<TOportunidad>
    {
        #region Metodos Base
        TOportunidad Add(Oportunidad entidad);
        TOportunidad Update(Oportunidad entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TOportunidad> Add(IEnumerable<Oportunidad> listadoEntidad);
        IEnumerable<TOportunidad> Update(IEnumerable<Oportunidad> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<Oportunidad> ObtenerOportunidad();
        IEnumerable<OportunidadComboDTO> ObtenerCombo();
        IEnumerable<OportunidadVentaCruzadaAgendaDTO> ObtenerVentaCruzadaParaAgendaPorIdClasificacionPersona(int idClasificacionPersona);
        IEnumerable<CentroCostoVentaCruzadaDTO> ObtenerCentroCostoVentaCruzada(int idPGeneral);
        IEnumerable<OportunidadHistorialAgendaDTO> ObtenerHistorialOportunidadesParaAgendaPorIdClasificacionPersona(int idClasificacionPersona);
        Task<OportunidadTiempoCapacitacionDTO> ObtenerTiempoCapacitacionPorIdOportunidadAsync(int idOportunidad);
        OportunidadCodigoFaseDTO ObtenerCodigoFasePorIdOportunidad(int idOportunidad);
        OportunidadCompuestoDTO ObtenerOportunidadCompuestoPorIdActividadDetalle(int idActividadDetalle);
        ResultadoVisualizarOportunidadDTO ValidarVisualizarAgendaPorIdOportunidad(int idOportunidad, int idPersonal);
        OportunidadBandejaCorreoDTO? ObtenerOportunidadBandejaCorreoPorIdAlumno(int idAlumno);
        Oportunidad? ObtenerPorId(int idOportunidad);
        Task<Oportunidad?> ObtenerPorIdAsync(int idOportunidad);
        int? ObtenerIdCentroCostoPorId(int idOportunidad);
        string ObtenerCronogramaPagoCompleto(int idOportunidad);
        Task<string> ObtenerCronogramaPagoCompletoAsync(int idOportunidad);
        Task<string> ObtenerCronogramaPagoCompletoChileAsync(int idOportunidad);
        string ObtenerMontoTotal(int idOportunidad);
        Task<string> ObtenerMontoTotalAsync(int idOportunidad);
        string ObtenerVersion(int idOportunidad);
        Task<string> ObtenerVersionAsync(int idOportunidad);
        OportunidadCompuestoDTO ObtenerDatosCompuestosPorIdOportunidad(int idOportunidad);
        Task<OportunidadCompuestoDTO> ObtenerDatosCompuestosPorIdOportunidadAsync(int idOportunidad);
        bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia);
        DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramacionAutomatica(int idOportunidad);
        ActividadTrabajadaDTO ValidarFaseOportunidad(int idOportunidad, int idFaseOportunidad, int idActividadDetalle);
        Task<ActividadTrabajadaDTO> ValidarFaseOportunidadAsync(int idOportunidad, int idFaseOportunidad, int idActividadDetalle);
        List<CentroCostoProbableDTO> ObtenerCentroCostoProbable(int idContacto, DateTime fechaActual);
        bool ValidarProbabilidadVentaCruzada(int? idProbabilidadRegistroPwActual);
        List<ValorIntDTO> ObtenerValidacionesProbabilidadVentaCruzada();
        OportunidadReasignarDTO ObtenerDatosOportunidadReasignacion(int idOportunidad);
        ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivo(int idOportunidad);
        ResultadoOportunidadesDTO ObtenerPorFiltroRegistrarOportunidad(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, int? idTipoProgramaCarrera);
        ResultadoOportunidadesDTO ObtenerPorFiltroRegistrarOportunidadSinAA(FiltrosRegistrarOportunidadDTO obj, PaginadorDTO paginador, string area, string tipoPersonal);
        int ObtenerCentroCostoPorCelularAlumno(string numero, int idPersonal);
        string ActualizarIdActividadDetalleUltima(int idActividadDetalle, int idOportunidad);
        Task ActualizarIdPersonalCoordinadorSeguimiento(int idOportunidad);
        string InsertarContadorReprogramacionManual(int idOportunidad, int contador);
        OportunidadBandejaCorreoDTO ObtenerOportunidadPorAsesorYAlumno(int idAlumno, int idPersonal, string numero);

        public ResultadoAsignacionManualFiltroTotalDTO ObtenerPorFiltroPaginaManual(AsignacionManualOportunidadFiltroDTO obj, FiltroKendoGridDTO filter, List<OperadoresComparacionDTO> operadorComparacion);

        OportunidadOperacionesFiltroDTO? ObtenerOportunidadOperacionesPorIdMatricula(int idMatriculaCabecera);
        OportunidadDTO GenerarOportunidadOperacionesConParametros(int idOportunidad, string usuario, int idCentroCosto, int idActividadCabecera, int idPersonal, int idMatriculaCabecera);
        bool EliminarOportunidadFisicaOperacionesV3V4(int idOportunidad);
        List<OportunidadISMDTO> ValidarOportunidadesISM(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular);
        bool ValidarOportunidadesISMPorIdAlumnoCelular(int idAlumno, string celular);
        List<OportunidadProbabilidadDTO> ValidarOportunidadesProbabilidad(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular);
        List<OportunidadCategoriaDTO> ValidarOportunidadesCategoria(int idAlumno, int idOportunidad, string celular);
        List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular);
        List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaIPMismoPG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular);
        List<OportunidadCategoriaDTO> ValidarOportunidadesCategoriaBNCITRNDiferentePG(int idAlumno, int idProgramaGeneral, int idOportunidad, string celular);
        bool ActualizarValidacionOportunidad(int idOportunidad, bool validacionCorrecta);
        List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidades(int idAlumno, int idClasificacionPersona);
        List<OportunidadHistorialDTO> CargarOportunidadHistorial(int idAlumno, int idClasificacionPersona);
        List<ObtenerSeguimientoAlumnoComentarioDTO> ObtenerComentariosOperaciones(int idOportunidad, int idTipoSeguimientoAlumnoCategoria);
        List<ObtenerSeguimientoPagosAlumnoComentarioDTO> ObtenerComentariosOperacionesPagosAcademicos(int idOportunidad);

        List<ValidarOportunidadWhatsAppDTO> ValidarOportunidadesWhatsApp(string numero, int idPGeneral);
        public ResultadosDTO ObtenerIdPersonalAsignadoChat(int idAlumno, int idCentroCosto);
        public List<AsignacionAutomaticaAsesorPosibilidadDTO> ObtenerAsesorParaCentroCosto(int idCentroCosto, int idSubCategoriaDato, int idPais, int probabilidad, int prioridad);
        OportunidadTabAgendaDTO ObtenerClasificacionTabAgenda(int idPersonal, string textoaBuscar, int tipo);
        OportunidadTabAgendaFichaDTO ObtenerClasificacionTabAgendaFicha(int idPersonal, int idMatriculaCabecera, int tipo);
        public object ObtenerPorFiltroRevertirFase(RevertirFaseFiltroDTO obj, PaginadorDTO paginador, List<GridFilterDTO> filter);
        bool TienePersonalOperaciones(int idOportunidad);
        public Oportunidad ObtenerOportunidadPorId(int idOportunidad);
        IdDTO ObtenerIdPersonalOperaciones(int idOportunidad);
        DatosOportunidadDocumentosCompuestoDTO ObtenerDatosCompuestosPorActividades(int idActividadDetalle);
        ResultadoFiltroAsignacionOportunidadDTO ObtenerPorFiltroPaginaManualOperaciones(PaginadorDTO paginador, AsignacionManualOportunidadOperacionesFiltroDTO filtro, GridFiltersDTO filterGrid, List<string> listaCodigoMatricula);
        EmailPersonalAlumnoDTO ObtenerEmailPorOportunidad(int idOportunidad);
        List<OportunidadVentaCruzadaDTO> ObtenerHistorialOportunidadesReporte(int idAlumno);
        List<OportunidadProblemaClienteDTO> ObtenerOportunidadProblemasCliente(int idOportunidad);
        ReporteSeguimientoOportunidadComplementosDTO ObtenerInformacionComplementariaReporteSeguimiento(int idOportunidad);
        List<OportunidadAnteriorDTO> ObtenerOportunidadesAnterioresAlumno(int idAlumno);
        List<OportunidadPendienteWhatsappDTO> ObtenerOportunidadesPendientesEnvioWhatsapp();
        DatosOportunidadReprogramacionManualOperacionesDTO ObtenerCasosReprogramacionManualOperacionesAlterno(int idOportunidad);
        DatosOportunidadReprogramacionAutomaticaDTO ObtenerDatosParaReprogramcionAutomatica(int idOportunidad);
        DatosOportunidadReprogramacionManualOperacionesNumReprogramacionesDTO ObtenerCalculoReprogramaciones(int idOportunidad);
        DatosOportunidadReprogramacionManualOperacionesSubEstadoDTO ObtenerSubEstadoAlumno(int idOportunidad);
        DatosAlumnoDTO ObtenerDatosAlumno(int idOportunidad);
        DatosAlumnoOportunidadDTO ObtenerDatosOportunidadAlumno(int idAlumno);
        OportunidadATCDTO ObtenerDatosOportunidadATCAlumno(int idAlumno,int idPersonal);
        PersonalAlumnoDTO ObtenerOportunidadPorNumero(string numero);
        PersonalAlumnoDTO ObtenerOportunidadPorNumero(int idCentroCosto, string numero);
        OportunidadTiempoCapacitacionDTO ObtenerTiempoCapacitacionPorIdOportunidad(int idOportunidad);
        List<Oportunidad> ObtenerPorIdAlumno(int idAlumno);
        List<RecomendacionDTO> ObtenerRecomendacionesPorIdActividadDetalle(int idActividadDetalle);
        FlagActualizarCorreoDTO VerificacionOportunidades(int idAlumnoPrincipal, int idAlumnoSecundario);
        FlagReasignacionDTO ResignacionOportunidades(int idAlumnoPrincipal, int idAlumnoSecundario);
        List<OportunidadProgramadaManualDTO> ObtenerProgramacionManualConsecutivos();
        ResultadoDTO LimpiarProgramacionManualConsecutivos();
        public OportunidadAnteriorAlternoDTO? ValidarOportundadVentaCruzada(int idOportunidad);
        int? ObtnerUltimoRN1(OportunidadAnteriorAlternoDTO oportunidad);
        void ActualizarSeguimientoWhatsAppOportunidad(int idOportunidad, int idActividad, bool estado);
        ActividadAgendaDTO? ObtenerDatosOportunidad(int idOportunidad);
        ActividadAgendaDTO? ObtenerOportunidadPredictivo(int idAlumno);
        ComboDTO? ObtenerProgramaGeneralPredictivo(int idOportunidadRN2);
        Task EliminarCronogramaOportunidad(int idOportunidad);

        ValorEtiquetaWhatsAppDTO? ObteneValoresEtiquetaWhatsApp(int idOportunidad);
        string InsertarEnviosWhatsappDiasSinContacto(int idOportunidad);

        ControlActividadAgendaDTO ObtenerReporteControlActividadesAgenda(int idAsesor);

        List<ResultadoBusquedaFichaAlumnoDTO> BuscarFichaPorCelular(string celular);
        List<ObtenerSeguimientoPagosAlumnoComentarioDosDTO> ObtenerComentariosOperacionesPagosAcademicos2(int idOportunidad);
        IEnumerable<ColorPerfilProgramaDTO> ObtenerColorPerfilProgramaPorIdOportunidad(int idOportunidad);
        List<CorreoNotificacionDTO> ObtenerCorreoNotificacion();
        public ProbabilidadOportunidadResumenDTO ObtenerProbabilidadModeloPredictivoMarketing(int idOportunidad, int tipo);
        public void ObtenerProbabilidadTodosProgramasPorAlumno(int idAlumno);
        public string InsertarArchivo(string nombreArchivo, string usuario);
        public List<ArchivoOportunidadDTO> ObtenerArchivosOportunidad();
        public void InsertarHistorialOportunidad(int idOportunidad, string usuario);
        public List<OportunidadMasivaDTO> ObtenerOportunidadesMasivas();
        public OportunidadConversionesDTO ObtenerInformacionOportunidadConversion(int idOportunidad);
        public OportunidadDetalleProbabilidadDTO ObtenerInformacionOportunidadProbabilidad(int idOportunidad);
        MetricasComparativasDiariasDTO ObtenerMetricasComparativasDiarias(int idAsesor, DateTime? fecha = null);
        MetricasActividadesATCDTO ObtenerMetricasActividadesATC(int idPersonal, DateTime? fecha = null);
        AlumnoCodigosDescuentosDTO ObtenerCodigoDescuentoAlumno(int idAlumno);
        bool ActualizarCentroCosto(int idCentroCosto, int idActividad);
        OportunidadFaseDTO ObtenerFaseUltimaOportunidadPorIdAlumno(int idAlumno);

        // RN2: llama a tdb.SP_ValidacionBloqueoAutomaticoRN2 y devuelve datos del alumno si aplica bloqueo, null si no
        ValidacionRn2SpResultDTO? ObtenerIdAlumnoPorValidacionRN2(int idOportunidad);
        // RN2: cuenta oportunidades del alumno vía tdb.SP_OportunidadesPorIdAlumno
        int ContarOportunidadesPorIdAlumno(int idAlumno, int idPersonalAsignado);
        // RN2: busca alumnos con celular o correo similar (LIKE) para detectar duplicados
        List<AlumnoSimilarRn2DTO> BuscarAlumnosSimilaresPorCelularOCorreo(string? telefono, string? correo);
        // RN2: verifica si alguno de los alumnos similares tiene oportunidades activas en fases RN2
        bool ExistenOportunidadesParaAlumnos(List<int> idAlumnos, int idPersonalAsignado);
        OportunidadEmpresaPagaDTO ObtenerEmpresaPagaPorCodigoMatricula(string codigoMatricula);

        IEnumerable<HistorialOportunidadPlanoDTO> ObtenerHistorialOportunidadesAlumno(int idAlumno);
    }
}