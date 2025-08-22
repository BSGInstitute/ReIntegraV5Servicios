
namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.GestionPersonas
{
    public class ExamenAsignadoEvaluadorDTO
    {
        public int Id { get; set; }
        public int IdPersonal { get; set; }
        public int IdPostulante { get; set; }
        public int IdExamen { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public bool EstadoExamen { get; set; }
        public int? IdMigracion { get; set; }
        public virtual ICollection<ExamenRealizadoRespuestaEvaluadorDTO> ExamenRealizadoRespuestaEvaluadors { get; set; }
    }
    public class EvaluacionesAsignadasEvaluadorDTO
    {
        public int Id { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdExamen { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int IdEvaluacion { get; set; }
        public string Evaluacion { get; set; }
        public bool? MostrarEvaluacionAgrupado { get; set; }
        public bool? MostrarEvaluacionPorGrupo { get; set; }
        public bool? MostrarEvaluacionPorComponente { get; set; }
        public bool EstadoExamen { get; set; }
        public bool RequiereTiempo { get; set; }
        public int? DuracionMinutos { get; set; }
        public string Instrucciones { get; set; }
    }
    public class PreguntaTestDTO
    {
        public int IdEvaluacion { get; set; }
        public int? IdGrupoComponenteEvaluacion { get; set; }
        public int IdExamenAsignado { get; set; }
        public int IdExamen { get; set; }
        public int IdPostulante { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int NroOrdenPregunta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string PreguntaTipo { get; set; }
        public int IdTipoRespuesta { get; set; }
        public string TipoRespuesta { get; set; }
    }
    public class PreguntaTestAgrupadoDTO
    {
        public int IdEvaluacion { get; set; }
        public int IdProcesoSeleccion { get; set; }
        public int IdPostulante { get; set; }
        public bool ExamenRequiereTiempo { get; set; }
        public int ExamenDuracionMinutos { get; set; }
        public string Instrucciones { get; set; }
        public List<PreguntaTestAgrupadoDetalleDTO> ListaPreguntas { get; set; }
    }

    public class PreguntaTestAgrupadoDetalleDTO
    {
        public int IdExamenAsignado { get; set; }
        public int IdExamen { get; set; }
        public int IdPregunta { get; set; }
        public string EnunciadoPregunta { get; set; }
        public int NroOrdenPregunta { get; set; }
        public int IdPreguntaTipo { get; set; }
        public string PreguntaTipo { get; set; }
        public int IdTipoRespuesta { get; set; }
        public string TipoRespuesta { get; set; }
        public List<RespuestasTestDTO> ListaRespuestas { get; set; }
        public List<RespuestaRealizadaDTO> ListaRespuestasRealizada { get; set; }
    }
    public class RespuestasTestDTO
    {
        public int IdPregunta { get; set; }
        public int IdRespuesta { get; set; }
        public int NroOrden { get; set; }
        public string EnunciadoRespuesta { get; set; }
    }
    public class RespuestaRealizadaDTO
    {
        public int Id { get; set; }
        public int IdExamenAsignadoEvaluador { get; set; }
        public int IdPregunta { get; set; }
        public int IdRespuestaPregunta { get; set; }
        public string TextoRespuesta { get; set; }
    }
    public class RespuestaTestDTO
    {
        public int IdExamenAsignado { get; set; }
        public List<RespuestaTestAgrupadaDTO> ListaRespuestas { get; set; }
    }

    public class RespuestaTestAgrupadaDTO
    {
        public int IdExamen { get; set; }
        public int IdRespuesta { get; set; }
        public int IdPregunta { get; set; }
        public string TextoRespuesta { get; set; }
        public bool Flag { get; set; }
    }
    public class RespuestaEvaluacionEvaluadorDTO
    {
        public List<RespuestaDetalleDTO> ListaRespuestasEvaluador { get; set; }
        public int IdEstadoEvaluacionEvaluador { get; set; }
        public int IdProcesoSeleccionEvaluacionEvaluador { get; set; }
        public int IdExamenEvaluacionEvaluador { get; set; }
        public int IdPostulanteEvaluacionEvaluador { get; set; }
    }
    public class RespuestaDetalleDTO
    {
        public int IdExamen { get; set; }
        public int IdRespuesta { get; set; }
        public int IdPregunta { get; set; }
        public int IdExamenAsignado { get; set; }
        public string TextoRespuesta { get; set; }
        public bool Flag { get; set; }
    }
    public class EvaluacionPortalPostulante
    {
        public int? IdPostulante { get; set; }
        public int? IdProcesoSeleccion { get; set; }
        public int? IdExamen { get; set; }
        public int? IdPespecifico { get; set; }
        public int? IdProgramaGeneral { get; set; }
        public int? IdAlumno { get; set; }
        public int? CantidadConfigurado { get; set; }
        public int? CantidadResuelto { get; set; }
        public decimal? PuntajeCurso { get; set; }
        public int? VersionCentil { get; set; }
    }
}
