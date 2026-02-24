using BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas;
using System.ComponentModel.DataAnnotations;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class ChatDetalleIntegraDTO
    {
        public int Id { get; set; }
        public int? IdInteraccionChatIntegra { get; set; }
        public string? NombreRemitente { get; set; }
        public string IdRemitente { get; set; } = null!;
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public int? IdChatDetalleIntegraArchivo { get; set; }
        public string UsuarioCreacion { get; set; } = null!;
        public string UsuarioModificacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class ChatDetalleIntegraComboDTO
    {
        public int Id { get; set; }
        public string? NombreRemitente { get; set; }
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
    }
    public class HistorialChatRecibidosDTO
    {
        public int IdInteraccionChat { get; set; }
        public string NombreRemitente { get; set; }
        public string Ubicacion { get; set; }
        public string Mensaje { get; set; }
        public int IdAsesor { get; set; }
        public DateTime? Fecha { get; set; }
        public string Chatsession { get; set; }
    }

    public class HistorialChatDetalleIntegraDTO
    {
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdRemitente { get; set; }
    }
    public class ChatHistorialComercialDTO
    {
        public int IdInteraccionChatIntegra { get; set; }
        public string NombreRemitente { get; set; }
        public string? Mensaje { get; set; }
        public DateTime Fecha { get; set; }
        public string? IdRemitente { get; set; }
        public bool? MensajeOfensivo { get; set; }
        public bool Estado { get; set; }
        public string? IdContactoPortalSegmento { get; set; }
        public int? IdPGeneral { get; set; }
        public string? IdFaseOportunidadPortalWeb { get; set; }
        public int? IdAlumno { get; set; }
    }

    public class CursoOportunidadDTO
    {
        public int IdOportunidad { get; set; }
        public int IdCentroCosto { get; set; }
        public int IdPespecifico { get; set; }
        public int IdProgramaGeneral { get; set; }
    }
    public class HistorialChatEntradaDTO
    {
        public int valor { get; set; }
        public bool esOnline { get; set; }
    }

    public class DatosSesionChatDTO
    {
        public int Id { get; set; }
        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdEstadoChat { get; set; }
        public string IdChatSession { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool? Leido { get; set; }
        public bool? EsAcademico { get; set; }
        public bool? EsSoporteTecnico { get; set; }
        public int IdMatriculaCabecera { get; set; }
    }
    public class DatosSesionChatComercialDTO
    {
        public int Id { get; set; }
        public int? IdChatIntegraHistorialAsesor { get; set; }
        public int IdAlumno { get; set; }
        public int? IdEstadoChat { get; set; }
        public string IdChatSession { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public bool? Leido { get; set; }
        public bool? EsAcademico { get; set; }
        public bool? EsSoporteTecnico { get; set; }
        public int? IdMatriculaCabecera { get; set; }
    }
    public class ChatActivoDetalleIntegraDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public string Mensaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdRemitente { get; set; }
        public string Emisor { get; set; }
        public bool EsChatBot { get; set; }
        public int Pendientes { get; set; }
    }

    public class PreguntaEvaluacion2DTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public int IdVersionFormularioEvaluacionChatbot { get; set; }
        public int IdTipoEntradaEvaluacionChatbot { get; set; }
        public string TipoEntrada { get; set; }
        public bool EsRequerido { get; set; }
        public List<RespuestaEvaluacionDTO> Respuestas { get; set; } = new List<RespuestaEvaluacionDTO>();
    }

    public class RespuestaEvaluacionDTO
    {
        public int Id { get; set; }
        public string Respuesta { get; set; }
        public int Orden { get; set; }
        public bool Estado { get; set; }
        public int IdPreguntaEvaluacionChatbot { get; set; }
        public int IdTipoEntradaEvaluacionChatbot { get; set; }
    }
    public class VersionFormularioDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Origen { get; set; }
        public int Version { get; set; }
        public bool Estado { get; set; }
    }

    public class TipoEntradaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
    }

    public class ObtenerPreguntasRequestDTO
    {
        public int IdVersionFormularioEvaluacionChatbot { get; set; }
    }

    public class ObtenerRespuestasRequestDTO
    {
        public int IdPregunta { get; set; }
    }

    public class ObtenerChatRequestDTO
    {
        public int IdAlumno { get; set; }
    }

    public class ObtenerChatRequest2DTO
    {
        public string IdContactoPortalSegmento { get; set; }
    }

    public class ChatbotMensajeDTO
    {
        public int IdChatbotPortalHiloChat { get; set; }
        public int? IdAlumno { get; set; }
        public bool EsUsuario { get; set; }
        public string Contenido { get; set; }
        public string IdContactoPortalSegmento { get; set; }
    }

    public class InsertarRespuestaEvaluacionCompletaRequestDTO
    {
        [Required]
        public int IdChatbotPortalHiloChat { get; set; }

        [Required]
        public int IdVersionFormularioEvaluacionChatbot { get; set; }

        [Required]
        [MaxLength(50)]
        public string UsuarioCreacion { get; set; }
        public int? IdSolicitudProblema { get; set; }
        public List<RespuestaSeleccionadaDTO> RespuestasSeleccionadas { get; set; } = new List<RespuestaSeleccionadaDTO>();
        public List<RespuestaTextoDTO> RespuestasTexto { get; set; } = new List<RespuestaTextoDTO>();
        public List<ProblemaIdentificadoDTO> ProblemasIdentificados { get; set; } = new List<ProblemaIdentificadoDTO>();
    }

    public class InsertarRespuestaEvaluacionResultadoDTO
    {
        public int IdFormularioAplicado { get; set; }
        public int TotalRespuestasInsertadas { get; set; }
        public int TotalProblemasIdentificados { get; set; }
        public int Success { get; set; }
    }

    public class InsertarRespuestaEvaluacionCompletaResponseDTO
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int IdFormularioAplicado { get; set; }
        public int TotalRespuestasInsertadas { get; set; }
        public int TotalProblemasIdentificados { get; set; }
    }

    public class RespuestaSeleccionadaDTO
    {
        public int IdRespuestaEvaluacionChatbot { get; set; }
    }

    public class RespuestaTextoDTO
    {
        public int IdPreguntaEvaluacionChatbot { get; set; }
        public string RespuestaTexto { get; set; }
    }

    public class ProblemaIdentificadoDTO
    {
        public int IdRespuestaEvaluacionChatbot { get; set; }
    }

    public class ChatbotHiloChatPorAlumnoDTO
    {
        public int IdChatbotPortalHiloChat { get; set; }
        public int? IdAlumno { get; set; }
        public string NombreAlumno { get; set; }
        public int? IdMatriculaCabecera { get; set; }
        public string CodigoMatricula { get; set; }
        public int? IdEstado_matricula { get; set; }
        public string EstadoMatricula { get; set; }
        public int? IdSubEstadoMatricula { get; set; }
        public int? CodigoAreaDerivacion { get; set; }
        public bool Cerrado { get; set; }
        public bool Derivado { get; set; }
        public bool? DerivacionCerrado { get; set; }
        public bool? EsCalificadoFormulario { get; set; }
        public string SubEstadoMatricula { get; set; }
        public DateTime FechaCreacion { get; set; }

    }

    public class ChatbotHiloChatPorSegmentoDTO
    {
        public int Id { get; set; }
        public string IdContactoPortalSegmento { get; set; }
        public int? CodigoAreaDerivacion { get; set; }
        public bool Cerrado { get; set; }
        public bool Derivado { get; set; }
        public bool? DerivacionCerrado { get; set; }

        public bool? EsCalificadoFormulario { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class ObtenerRespuestasClienteRequestDTO
    {
        public int IdChatbotPortalHiloChat { get; set; }
    }

    public class RespuestaClienteDTO
    {
        public int IdPregunta { get; set; }
        public string NombrePregunta { get; set; }
        public int OrdenPregunta { get; set; }
        public string TipoEntradaNombre { get; set; }
        public int? IdRespuestaEvaluacion { get; set; }
        public string RespuestaPredefinida { get; set; }
        public int? OrdenRespuesta { get; set; }
        public string RespuestaCliente { get; set; }
        public bool EsTextoLibre { get; set; }
        public bool EsProblemaIdentificado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    // === DTOs Chatbot ATC - Obtener Actividades ===

    public class ObtenerActividadesAtcRequestDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
    }

    public class AmpliarFechaEntregaRequestDTO
    {
        public int IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividad { get; set; }
        public string Fecha { get; set; }
        public string TipoActividad { get; set; }
    }

    public class ObtenerAsistenciaAtcRequestDTO

    public class VideoAulaVirtualDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdAlumno { get; set; }
        public int? IdPGeneralPadre { get; set; }
        public int? IdPGeneralHijo { get; set; }
        public int? OrdenSeccion { get; set; }
        public int? IdPEspecificoHijo { get; set; }
        public int? VideosTerminados { get; set; }
        public int? VideosTotal { get; set; }
    }

    public class EncuestaRealizadaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public int? IdPGeneralPadre { get; set; }
        public int? IdPGeneralHijo { get; set; }
        public int? IdPEspecificoHijo { get; set; }
        public int? ExamenProgramados { get; set; }
        public int? ExamenRealizado { get; set; }
        public bool? Completado { get; set; }
    }

    public class TareaRealizadaDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int? IdPEspecifico { get; set; }
        public int IdAlumno { get; set; }
        public int? IdPGeneralPadre { get; set; }
        public int? IdPGeneralHijo { get; set; }
        public int? IdPEspecificoHijo { get; set; }
        public int? TareasProgramadas { get; set; }
        public int? TareasRealizadas { get; set; }
        public bool? Completado { get; set; }
    }

    public class ActividadRecursoSesionDocenteDTO
    {
        public int? Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public bool? Publicado { get; set; }
        public int? AsignadoPara { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public DateTime? FechaEntregaSecundaria { get; set; }
    }


    public class DatoPerfilProyectoDTO
    {
        public string ProyectoAplicacion { get; set; }
        public int? IdProyecto { get; set; }
    }

    public class ConfigurarEvaluacionTrabajoV2DTO
    {
        public int Id { get; set; }
        public int IdTipoEvaluacionTrabajo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? IdDocumentoPw { get; set; }
        public int? IdPGeneral { get; set; }
    }

    public class InstruccionDocumentoSeccionDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public int? OrdenWeb { get; set; }
        public string ZonaWeb { get; set; }
    }


    public class SesionAsistenciaDTO
    {
        public int? IdPEspecificoSesion { get; set; }
        public string NombreSesion { get; set; }
        public DateTime? FechaSesion { get; set; }
        public bool? Asistio { get; set; }
    }


    public class ActividadAtcDTO
    {
        public int ActividadId { get; set; }
        public string ActividadNombre { get; set; }
        public DateTime? ActividadFechaEntrega { get; set; }
        public DateTime? ActividadFechaEntregaSecundaria { get; set; }
        public string ActividadEstado { get; set; }
        public string TipoActividad { get; set; }
    }

    public class ObtenerActividadesAtcResponseDTO
    {
        public List<VideoAulaVirtualDTO> Videos { get; set; } = new List<VideoAulaVirtualDTO>();
        public List<EncuestaRealizadaDTO> Encuestas { get; set; } = new List<EncuestaRealizadaDTO>();
        public List<TareaRealizadaDTO> Tareas { get; set; } = new List<TareaRealizadaDTO>();
        public List<ActividadRecursoSesionDocenteDTO> ActividadesOnline { get; set; } = new List<ActividadRecursoSesionDocenteDTO>();
        public DatoPerfilProyectoDTO Proyecto { get; set; }
        public ConfigurarEvaluacionTrabajoV2DTO ProyectoConfiguracion { get; set; }
        public List<InstruccionDocumentoSeccionDTO> ProyectoInstrucciones { get; set; } = new List<InstruccionDocumentoSeccionDTO>();
    }

    public class AmpliarFechaEntregaResponseDTO
    {
        public string Mensaje { get; set; }
        public Dictionary<string, string> Error { get; set; }
    }

}
