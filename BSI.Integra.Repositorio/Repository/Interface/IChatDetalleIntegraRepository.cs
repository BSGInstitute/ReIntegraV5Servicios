using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB;
using BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB;
using BSI.Integra.Persistencia.Entidades.IntegraDB;
using BSI.Integra.Persistencia.Modelos.IntegraDB;

namespace BSI.Integra.Repositorio.Repository.Interface
{
    public interface IChatDetalleIntegraRepository : IGenericRepository<TChatDetalleIntegra>
    {
        #region Metodos Base
        TChatDetalleIntegra Add(ChatDetalleIntegra entidad);
        TChatDetalleIntegra Update(ChatDetalleIntegra entidad);
        bool Delete(int id, string usuario);

        IEnumerable<TChatDetalleIntegra> Add(IEnumerable<ChatDetalleIntegra> listadoEntidad);
        IEnumerable<TChatDetalleIntegra> Update(IEnumerable<ChatDetalleIntegra> listadoEntidad);
        bool Delete(IEnumerable<int> listadoIds, string usuario);
        #endregion
        IEnumerable<ChatDetalleIntegraDTO> ObtenerChatDetalleIntegra();
        IEnumerable<ChatDetalleIntegraComboDTO> ObtenerCombo();
        HistorialChatRecibidosDTO ObtenerHistorialChatRecibidos(int idPersonal, int idAlumno);
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccion(int idInteraccion);
        CursoOportunidadDTO ObtenerCursoOportunidad(int idOportunidad);
        List<ChatHistorialComercialDTO> ObtenerDetalleChatPorIdAlumno(int idAlumno,int idPGeneral);
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno);

        bool FinalizarChatAtc(int idMatriculaCabecera,string userName);
        bool FinalizarChatComercial(int idOportunidad, string userName);
        List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera);
        List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno);
        ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente);
        List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera);
        List<WhatsAppHistorialMensajesOperacionesDTO> ObtenerHistorialChatDetalleIntegraFlotante(int idMatriculaCabecera);
        IEnumerable<PreguntaEvaluacion2DTO> ObtenerPreguntasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot);
        IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorPregunta(int idPregunta);
        IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot);
        IEnumerable<VersionFormularioDTO> ObtenerVersionesFormularioActivas();
        IEnumerable<TipoEntradaDTO> ObtenerTiposEntradaActivos();
        IEnumerable<ChatbotMensajeDTO> ObtenerChatPorAlumno(int idAlumno);
        InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompleta(int idChatbotPortalHiloChat, int idVersionFormularioEvaluacionChatbot, string usuarioCreacion, int? idSolicitudProblema, int idMedioComunicacion, int idOriginal, string respuestasSeleccionadasJson = null, string respuestasTextoJson = null, string problemasIdentificadosJson = null);
        IEnumerable<ChatbotMensajeDTO> ObtenerChatPorPortalSegmento(string IdContactoPortalSegmento);
        IEnumerable<ChatbotAlumnoChatPaginadoDTO> ObtenerHilosChatConAlumnos(DateTime? fechaInicio, DateTime? fechaFin, int pageNumber, int pageSize, string? codigoMatricula);
        IEnumerable<ChatbotHiloChatPorSegmentoDTO> ObtenerHilosChatPorSegmento();
        IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdFormularioAplicadoChatbot);
        IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicadoWhatsapp(int idChatbotWhatsAppHiloChat);
        InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompletaWhatsapp(int idMedioComunicacion, int idOriginal, int idVersionFormularioEvaluacionChatbot, string usuarioCreacion, int? idSolicitudProblema, string respuestasSeleccionadasJson = null, string respuestasTextoJson = null, string problemasIdentificadosJson = null);
        IEnumerable<int> ObtenerIdsPreguntaPorIdsRespuesta(IEnumerable<int> idsRespuesta);
        IEnumerable<HiloChatPaginadoDTO> ObtenerHilosPaginadosPorAlumno(int idAlumno, DateTime fechaInicio, DateTime fechaFin, int pageNumber, int pageSize);
        IEnumerable<SolicitudPorHiloChatDTO> ObtenerSolicitudesPorHiloChat(int idHiloChat, int idChatbotTipo);
        List<ChatActivoDetalleIntegraDTO> ObtenerChatsAcademicosHabilitadosCoordinadora(int IdCoordinadorAcademico, bool EsOnline);

        int? ObtenerIdMatriculaCabecera(int idAlumno, int idPEspecifico);
        List<VideoAulaVirtualDTO> ObtenerVideosAulaVirtual(int idMatriculaCabecera);
        List<EncuestaRealizadaDTO> ObtenerEncuestasRealizadas(int idMatriculaCabecera);
        List<TareaRealizadaDTO> ObtenerTareasRealizadas(int idMatriculaCabecera);
        List<int> ObtenerIdsPEspecificoSesion(int idPEspecifico);
        List<ActividadAtcDTO> ObtenerCuestionariosPorPEspecifico(int idPEspecifico);
        List<ActividadAtcDTO> ObtenerTareasPorPEspecifico(int idPEspecifico);
        List<ActividadRecursoSesionDocenteDTO> ObtenerActividadesRecursoSesionDocente(int idPEspecificoSesion);
        DatoPerfilProyectoDTO ObtenerDatoPerfilProyecto(int idMatriculaCabecera);
        ConfigurarEvaluacionTrabajoV2DTO ObtenerConfigurarEvaluacionTrabajo(int idProyecto);
        List<InstruccionDocumentoSeccionDTO> ObtenerInstruccionesDocumentoSeccion(int idPGeneral, int idDocumento);
        bool AmpliarFechaCuestionario(int idActividad, string fecha);
        bool AmpliarFechaTarea(int idActividad, string fecha);
        int? ObtenerIdPEspecificoPorSesion(int idPEspecificoSesion);
        List<SesionAsistenciaDTO> ObtenerAsistenciaPorMatricula(int idMatriculaCabecera, int idPEspecifico);
        bool RegistrarAsistenciaMatricula(int idMatriculaCabecera, int idPEspecificoSesion);
        IEnumerable<ChatbotMensajeWhatsAppAtcDTO> ObtenerChatWhatsAppAtcPorAlumno(int idAlumno);
    }
}