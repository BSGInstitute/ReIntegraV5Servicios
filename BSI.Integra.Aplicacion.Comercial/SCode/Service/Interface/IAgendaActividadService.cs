using BSI.Integra.Aplicacion.DTO;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Comercial;
using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion;
using BSI.Integra.Persistencia.Entidades.IntegraDB;

namespace BSI.Integra.Aplicacion.Comercial.Service.Interface
{
    public interface IAgendaActividadService
    {
        int ObtenerDiasSinContactoPorOportunidad(int idOportunidad);
        int ValidarVisualizacionDatosOportunidad(int idOportunidad, int idPersonal);
        PGeneralCabeceraSpeechAgendaDTO ObtenerCabeceraSpeech(int idOportunidad, int idCentroCosto);
        IEnumerable<PGeneralPublicoObjetivoParaAgendaDTO> ObtenerPublicoObjetivoPrograma(int idCentroCosto, int idOportunidad);
        IEnumerable<ProgramaGeneralCertificacionDetalleAgendaDTO> ObtenerRequisitosCertificacionProgramaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralMotivacionDetalleAgendaDTO> ObtenerArgumentosMotivacionProgramaPorIdOportunidad(int idOportunidad);
        OportunidadInformacionDTO ObtenerOportunidadInformacion(int idAlumno, int idClasificacionPersona);
        IEnumerable<CentroCostoVentaCruzadaDTO> ObtenerCentroCostoVentaCruzada(int idPGeneral);

        IEnumerable<ProgramaGeneralProblemaDetalleAgendaDTO> ObtenerProgramaGeneralProblemaDetallePorIdOportunidad(int idOportunidad);
        IEnumerable<CorreoInteraccionV2AgendaDTO> ObtenerCorreoInteraccionV2EnviadosPorPersonal(int idAlumno, int idPersonal);
        IEnumerable<CompetidorOportunidadAgendaDTO> ObtenerCompetidorPorIdOportunidad(int idOportunidad);
        OportunidadPrerequisitoBeneficioCompetidorDTO ObtenerPrerequisitosBeneficiosCompetidoresPorIdOportunidad(int idOportunidad);
        IEnumerable<OportunidadLogHistorialComentariosDTO> ObtenerHistorialComentariosPorIdOportunidad(int idOportunidad);
        List<ReporteSeguimientoNWActividadDTO?> ObtenerHistorialInteraccionesPorIdOportunidad(int idOportunidad);
        List<ReporteSeguimientoNWActividadAlternoDTO?> ObtenerHistorialInteraccionesPorIdOportunidad3cx(int idOportunidad);
        (List<PreguntaFrecuenteSeccionesDTO> Data, List<PGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentes(int idCentroCosto, int idOportunidad);
        (List<PreguntaFrecuenteSeccionesDTO> Data, List<ProgramaGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentesCambio(int idCentroCosto, int idPrograma, int idOportunidad);
        (List<PreguntaFrecuenteSeccionesV2DTO> Data, List<ProgramaGeneralModeloCertificadoDTO> ModeloCertificado) ObtenerPreguntasFrecuentesCambioV2(int idCentroCosto, int idPrograma, int idOportunidad);
        List<ArbolOcurrenciaDTO> ObtenerArbolOcurrencias(int idActividadCabecera, int idOcurrenciaActividadPadre);
        IEnumerable<AlumnoLogAgendaFechaStringDTO> ObtenerHistorialModificacionAlumnoPorIdAlumno(int idAlumno);
        IEnumerable<DocumentoLegalV3DTO> ObtenerDocumentoLegal(int idAreaPersonal, string rol, int idAlumno);
        IEnumerable<PlantillaClaveValorAreaEtiquetaDTO> ObtenerPlantillasPorIdFaseOportunidad(int idFaseOportunidad);
        SeguimientoAsesorDTO ObtenerSeguimientoAsesor(int idAsesor, int idCategoriaOrigen, int estadoPantalla);
        DocumentoAgendaDetalleDTO ObtenerDocumentosPorIdActividadDetalle(int idActividadDetalle);
        (AlumnoInformacionDTO, SueldoPromedioDTO, ResultadoVisualizarOportunidadDTO) ObtenerDatosAlumno(int idClasificacionPersona, int idOportunidad, int idPersonal);
        IEnumerable<PlantillaWhatsAppAgendaDTO> ObtenerPlantillaWhatsApp();
        StringDTO ObtenerProbabilidadSueldoOportunidad(int idOportunidad, int idPais);
        //ValorEtiquetaDTO ObtenerValorEtiqueta(int idCentroCosto, int idFaseOportunidad, int idOportunidad);
        CargarInformacionProgramaAutomaticoRespuestaDTO ObtenerInformacionProgramav1(Dictionary<string, string> filtro);
        ObtenerConfiguracionesDTO ObtenerConfiguraciones();
        IOrderedEnumerable<FiltroDTO> ObtenerPlantillaPorFase(int idFaseOportunidad, int idPersonalAreaTrabajo);
        bool ValidarRN2(int idContacto, int idCentroCosto, int idOcurrencia);
        string ObtenerFechaHoraActividadReprogramacionAutomatica(int idOportunidad, string codigoFase, int idOcurrencia);
        List<HojaActividadesDTO> ObtenerHojaActividadesPorIdOcurrenciaAlterno(int idOcurrencia);
        ActualizarSentinelAlumnoDTO ActualizarSentinelAlumno(string dni, int idContacto, string usuario);
        SentinelDatosCabeceraDTO ObtenerSemaforoSentinelAlumno(int idAlumno);
        bool ActualizarTiempoCapacitacion(OportunidadTiempoCapacitacionDTO oportunidadTiempoCapacitacionDTO);
        Alumno ActualizarAlumno(AlumnoActualizarDTO alumno, string usuario, string areaTrabajo);
        SentinelDatosContactoDTO ObtenerDatoSentinelAlumno(int idAlumno);
        string ObtenerInformacionPrograma(Dictionary<string, string> filtros);
        List<ResumenProgramaV2DTO> ObtenerResumenProgramasV2(Dictionary<string, string> filtros);
        CorreoAlumnoSpeechDTO ObtenerCorreosEnviadosSpeech(string correoReceptor, string messageId);
        List<ComboDTO> ObtenerCompetidores();
        List<DocumentoOportunidadInsertadoDTO> ObtenerDocumentosPorIdOportunidad(int idOportunidad);
        SpeechBienvenidaDespedidaDTO ObtenerIdSpeechBienvenidaDespedida(int idActividadDetalle);
        ContactoConfiguracionDTO ObtenerConfiguracionContacto(int idTipoDato);
        ReferidoConfiguracionDTO ObtenerConfiguracionReferidos();
        string? ObtenerFechaFinalizacionMatricula(int idMatriculaCabecera);
        int ObtenerCantidadMensajesPorUsername(string userName);
        DataCreditoDataDTO ActualizarInformacionDataCredito(string documento, int idAlumno, string usuario);
        List<PGeneralComboDTO> ObtenerComboProgramaGeneral();
        bool EnviarIndividualSMSPorOcurrencia(int idOportunidad, int idOcurrencia);
        ReporteIncidenciaDTO CargarReporteIncidencia(FiltroReporteIncidenciaDTO filtro);
        SolciitudBeneficioDTO AprobarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudAprobado);
        SolciitudBeneficioDTO RechazarSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario, int IdEstadoSolicitudRechazado, int IdEstadoMatriculaCabeceraBeneficio);
        SolciitudBeneficioDTO RestablecerSolicitudBeneficio(int IdMatriculaCabeceraBeneficio, string Usuario);
        ValorEtiquetaWhatsAppDTO ObtenerValoresEtiquetaWhatsapp(int idOportunidad);
        ControlActividadAgendaDTO ObtenerReporteControlActividadesAgenda(int idAsesor);
        MetricasComparativasDiariasDTO ObtenerMetricasComparativasDiarias(int idAsesor, DateTime? fecha = null);
        List<ResultadoBusquedaFichaAlumnoDTO> BuscarFichaPorCelular(string celular);
        int ObtenerIdSkillPorCelular(string celular);
        IEnumerable<ColorPerfilProgramaDTO> ObtenerColorPerfilProgramaPorIdOportunidad(int idOportunidad);
        IEnumerable<ProgramaGeneralPresentacionArgumentoDetalleAgendaDTO> ObtenerProgramaGeneralPresentacionArgumentoDetallePorIdOportunidad(int idOportunidad);
    }
}