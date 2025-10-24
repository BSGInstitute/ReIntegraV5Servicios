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
        List<ChatDetalleIntegra> ObtenerDetalleChatPorIdInteraccionControlMensajesSoporte(int idAlumno);

        bool FinalizarChatAtc(int idMatriculaCabecera,string userName);
        bool FinalizarChatComercial(int idOportunidad, string userName);
        List<DatosSesionChatDTO> GetIdUltimaInteraccion(int idMatriculaCabecera);
        List<DatosSesionChatComercialDTO> GetIdUltimaInteraccionComercial(int idAlumno);
        ChatDetalleIntegra ObtenerPorIntegraChatYRemintente(int idInteraccionChatIntegra, string idRemitente);
        List<HistorialChatDetalleIntegraDTO> ObtenerHistorialChatDetalleIntegra(int idMatriculaCabecera);
        IEnumerable<PreguntaEvaluacion2DTO> ObtenerPreguntasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot);
        IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorPregunta(int idPregunta);
        IEnumerable<RespuestaEvaluacionDTO> ObtenerRespuestasPorVersionFormulario(int IdVersionFormularioEvaluacionChatbot);
        IEnumerable<VersionFormularioDTO> ObtenerVersionesFormularioActivas();
        IEnumerable<TipoEntradaDTO> ObtenerTiposEntradaActivos();
        IEnumerable<ChatbotMensajeDTO> ObtenerChatPorAlumno(int idAlumno);
        InsertarRespuestaEvaluacionResultadoDTO InsertarRespuestaEvaluacionCompleta(int idChatbotPortalHiloChat, int idVersionFormularioEvaluacionChatbot, string usuarioCreacion, string respuestasSeleccionadasJson = null, string respuestasTextoJson = null, string problemasIdentificadosJson = null);
        IEnumerable<ChatbotMensajeDTO> ObtenerChatPorPortalSegmento(string IdContactoPortalSegmento);
        IEnumerable<ChatbotHiloChatPorAlumnoDTO> ObtenerHilosChatConAlumnos();
        IEnumerable<ChatbotHiloChatPorSegmentoDTO> ObtenerHilosChatPorSegmento();
        IEnumerable<RespuestaClienteDTO> ObtenerRespuestasUsuarioPorFormularioAplicado(int IdFormularioAplicadoChatbot);
    }
}