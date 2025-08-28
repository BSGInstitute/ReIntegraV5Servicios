using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Google.Protobuf.WellKnownTypes.Field.Types;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    public class ReporteEncuestasInicialSincronicoDTO
    {
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string AsesoraAcademica { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Pregunta7 { get; set; }
        public string Pregunta8 { get; set; }
        public string Pregunta9 { get; set; }
        public string Pregunta10 { get; set; }
        public string Pregunta11 { get; set; }
        public string Pregunta12 { get; set; }
        public string Pregunta13 { get; set; }

        public string ComentarioAlumno { get; set; }

    }

    public class ReporteEncuestasIntermediaSincronicoDTO
    {
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string AsesoraAcademica { get; set; }
        public string Resultados { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Promedio1 { get; set; }
        public string Observaciones1 { get; set; }
        public string Pregunta7 { get; set; }
        public string Pregunta8 { get; set; }
        public string Pregunta9 { get; set; }
        public string Pregunta10 { get; set; }
        public string Pregunta11 { get; set; }
        public string Pregunta12 { get; set; }
        public string Pregunta13 { get; set; }
        public string Promedio2 { get; set; }
        public string Observaciones2 { get; set; }
        public string Pregunta14 { get; set; }
        public string Pregunta15 { get; set; }
        public string Promedio3 { get; set; }
        public string Observaciones3 { get; set; }
        public string Pregunta16 { get; set; }
        public string Pregunta17 { get; set; }
        public string Pregunta18 { get; set; }
        public string Pregunta19 { get; set; }
        public string Pregunta20 { get; set; }
        public string Pregunta21 { get; set; }
        public string Pregunta22 { get; set; }
        public string Promedio4 { get; set; }
        public string Observaciones4 { get; set; }
        public string Pregunta23 { get; set; }
        public string Promedio5 { get; set; }
        public string Observaciones5 { get; set; }
        public string Resumen { get; set; }
        public string ResumenDetalle { get; set; }
        public string ComentarioAlumno { get; set; }
    }

    public class ReporteEncuestasFinalSincronicoDTO
    {
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string AsesoraAcademica { get; set; }
        public string Resultados { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Promedio1 { get; set; }
        public string Observaciones1 { get; set; }
        public string Pregunta7 { get; set; }
        public string Pregunta8 { get; set; }
        public string Pregunta9 { get; set; }
        public string Pregunta10 { get; set; }
        public string Pregunta11 { get; set; }
        public string Pregunta12 { get; set; }
        public string Pregunta13 { get; set; }
        public string Promedio2 { get; set; }
        public string Observaciones2 { get; set; }
        public string Pregunta14 { get; set; }
        public string Pregunta15 { get; set; }
        public string Promedio3 { get; set; }
        public string Observaciones3 { get; set; }
        public string Pregunta16 { get; set; }
        public string Pregunta17 { get; set; }
        public string Pregunta18 { get; set; }
        public string Pregunta19 { get; set; }
        public string Pregunta20 { get; set; }
        public string Pregunta21 { get; set; }
        public string Pregunta22 { get; set; }
        public string Promedio4 { get; set; }
        public string Observaciones4 { get; set; }
        public string Pregunta23 { get; set; }
        public string Promedio5 { get; set; }
        public string Observaciones5 { get; set; }
        public string Resumen { get; set; }
        public string ResumenDetalle { get; set; }
        public string Pregunta24 { get; set; }
        public string Pregunta25 { get; set; }
        public string Pregunta26 { get; set; }
        public string Pregunta27 { get; set; }
        public string Pregunta28 { get; set; }
        public string Pregunta29 { get; set; }
        public string Pregunta30 { get; set; }
        public string ComentarioAlumno { get; set; }
    }
    public class ReporteEncuestaFiltroSincronicoDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsProgramasGenerales { get; set; }
        public List<int>? IdsProgramasEspecificos { get; set; }
        public List<int>? IdsDocentes { get; set; }
        public int IdVersionEncuesta { get; set; }
    }
    public class ReporteEncuestasDocenteDTO
    {
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string Pregunta1 { get; set; }
        public string Pregunta2 { get; set; }
        public string Pregunta3 { get; set; }
        public string Pregunta4 { get; set; }
        public string Pregunta5 { get; set; }
        public string Pregunta6 { get; set; }
        public string Pregunta7 { get; set; }
        public string Pregunta8 { get; set; }
        public string Pregunta9 { get; set; }
        public string Pregunta10 { get; set; }
        public string Pregunta11 { get; set; }
        public string Pregunta12 { get; set; }
        public string Pregunta13 { get; set; }
        public string Pregunta14 { get; set; }
        public string Pregunta15 { get; set; }
    }
    public class filtroTestimonioDTO
    {
		public int idPEspecifico { get; set; }
        public int modalidad { get; set; }
	}
    public class filtroValoracionDTO
    {
        public int? idPEspecifico { get; set; }
        public int? idPGeneral { get; set; }
    }
    public class ReportePGeneralTestimonio
	{

		public string? NombreAlumno { get; set; }
		public int? IdPEspecifico { get; set; }
		public int? IdObservacionesC5 { get; set; }
		public string? ObservacionesC5 { get; set; }
		public int? IdRespuesta26 { get; set; }
		public string? Respuesta26 { get; set; }
		public int? IdRespuesta27 { get; set; }
		public string? Respuesta27 { get; set; }
		public int? IdRespuestaBeneficio { get; set; }
		public string? RespuestaBeneficio { get; set; }
		public int? VersionEncuesta { get; set; }
		public string? NombreVersion { get; set; }
		public string? IdRespuestasAsociada { get; set; }
		public string? IdRespuestasTestimonio { get; set; }
        public string? PromedioTotal { get; set; }
        public string? Testimonio { get; set; }

		public bool? VisiblePW { get; set; }
	}
    public class FiltroRespuestaCombo
    {
        public string? IdRespuestasAsociada { get; set; }
        public int? Version { get; set; }
    }
    public class TestimonioInsertarDTO
    {
        public string? Testimonio { get; set; }
        public List<ComboDTO> IdRespuesta { get; set; }
        public int VisiblePW { get; set; }
        public int Modalidad { get; set; }
        public List<ComboDTO> ListaRespuestas { get; set; }
    }
    public class TestimonioEncuestaObtenerDTO
    {
        public int Id { get; set; }
        public string? Testimonio { get; set; }
        public int? IdPEspecificoSesionEncuestaAlumnoRespuesta { get; set; }
        public int? IdExamenRealizadoRespuestaAulaVirtual { get; set; }
        public bool VisiblePW { get; set; }
        public bool Estado { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
    public class TestimonioEntidadActualizarDTO
    {
        public string? Testimonio { get; set; }
        public int Id { get; set; }
        public int VisiblePW { get; set; }
        public int Modalidad { get; set; }

    }

    public class TestimonioEntidadDTO
    {
        public string? Testimonio { get; set; }
        public int IdRespuesta { get; set; }
        public int VisiblePW { get; set; }
        public int Modalidad { get; set; }

    }
    public class  ReportePGeneralValoracion
    {
        public string? NombrePEspecifico { get; set; }
        public int? IdPEspecifico { get; set; }
        public string? Promedio1 { get; set; }
        public string? IdsPromedio1 { get; set; }
        public string? Promedio2 { get; set; }
        public string? IdsPromedio2 { get; set; }
        public string? Promedio3 { get; set; }
        public string? IdsPromedio3 { get; set; }
        public string? Promedio4 { get; set; }
        public string? IdsPromedio4 { get; set; }
        public string? Promedio5 { get; set; }
        public string? IdsPromedio5 { get; set; }
        public int? Modalidad { get; set; }
        public string? PromedioTotal { get; set; }
        public bool? visiblePw { get; set; }
        public int? NroEncuesta { get; set; }
    }

    public class ReporteEncuestasSincronicoPorVersionDTO
    {
        public int? OrdenPregunta { get; set; }
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string AsesoraAcademica { get; set; }
        public string ComentarioAlumno { get; set; }
        public int IdPEspecificoSesionEncuestaAlumno { get; set; }
        public int IdPreguntaEncuesta { get; set; }
        public int IdPEspecificoSesionEncuestaAlumnoRespuesta { get; set; }
        public string Pregunta { get; set; }
        public string Valor { get; set; }
        public int IdPreguntaRespuestaEncuesta { get; set; }
        public int IdPreguntaEncuestaOnline { get; set; }
    }

    public class ReporteEncuestaAgrupadoDTO
    {
        public string CentroCostoPrograma { get; set; }
        public string ProgramaGeneral { get; set; }
        public string ProgramaEspecifico { get; set; }
        public string CentroCostoCurso { get; set; }
        public string CursoGeneral { get; set; }
        public string CursoEspecifico { get; set; }
        public string Docente { get; set; }
        public DateTime FechaRealizada { get; set; }
        public DateTime FechaIngreso { get; set; }
        public string CodigoMatricula { get; set; }
        public string NombreAlumno { get; set; }
        public string Correo { get; set; }
        public string AsesoraAcademica { get; set; }
        public int IdPEspecificoSesionEncuestaAlumno { get; set; }
        public string ComentarioAlumno { get; set; }
        public List<PreguntaAgrupadaDTO> RegistroPreguntas { get; set; }
    }

    public class PreguntaAgrupadaDTO
    {
        public int IdPreguntaEncuesta { get; set; }
        public int IdPreguntaEncuestaOnline { get; set; }
        public string Pregunta { get; set; }
        public List<RespuestaAgrupadaDTO> Respuestas { get; set; }
    }

    public class RespuestaAgrupadaDTO
    {
        public string Valor { get; set; }
        public int IdPreguntaRespuestaEncuesta { get; set; }
    }

    public class ReporteEncuestaFiltroSincronicoPorVersionDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public List<int>? IdsProgramasGenerales { get; set; }
        public List<int>? IdsProgramasEspecificos { get; set; }
        public List<int>? IdsDocentes { get; set; }
        public int Version {  get; set; }
    }

    public class ValoracionesActualizarDTO
    {
        public List<int> IdRespuestas { get; set; }
        public int Modalidad { get; set; }
        public int VisiblePw { get; set; }
    }

    public class ListaPreguntaAgrupadaDTO {
        public int Id { get; set; }
        public string Enunciado { get; set; }
        public int? IdTipoRespuesta { get; set; }
        public int? IdPreguntaTipo { get; set; }
        public int? MinutosPorPregunta { get; set; }
        public bool? RespuestaAleatoria { get; set; }
        public bool? ActivarFeedBackRespuestaCorrecta { get; set; }
        public bool? ActivarFeedBackRespuestaIncorrecta { get; set; }
        public bool? MostrarFeedbackInmediato { get; set; }
        public bool? MostrarFeedbackPorPregunta { get; set; }
        public int? NumeroMaximoIntento { get; set; }
        public bool? ActivarFeedbackMaximoIntento { get; set; }
        public string? MensajeFeedback { get; set; }
        public List<int?> ListaExamen { get; set; }
        public List<string> ComponenteExamen { get; set; }
        public int? IdTipoRespuestaCalificacion { get; set; }
        public int? FactorRespuesta { get; set; }
        public int? IdPreguntaCategoria { get; set; }
    }
}
