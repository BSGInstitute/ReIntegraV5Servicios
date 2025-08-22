namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB
{
    public class PEspecificoMatriculaAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPespecifico { get; set; }
        public int IdPEspecificoRecuperacion { get; set; } //programa especifico que va a ser recuperado
        public int Grupo { get; set; } //Grupo al que pertence el curso
        public int IdAlumno { get; set; } //IdAlumno
        public int IdOportunidad { get; set; }//Oportunidad del alumno
        public string Usuario { get; set; }
    }

    public class PEspecificoCriterioDetalleEntregableDelAlumno
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int IdCriterioEvaluacion { get; set; }
        public int IdSesionEntregable { get; set; }
        public string Titulo { get; set; }
        public DateTime? FechaProgramada { get; set; }
        public DateTime? FechaProgramadaSecundaria { get; set; }
        public DateTime? FechaEntregada { get; set; }
        public string EstadoEntrega { get; set; }
        public int PuntajeMaximo { get; set; }
        public int PuntajeMaximoSecundario { get; set; }
        public int Calificacion { get; set; }

    }
        public class PEspecificoMatriculaAlumnoAgendaDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string TipoMatricula { get; set; }
        public bool Estado { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string Promedio { get; set; }
        public int TipoPrograma { get; set; }
    }
    public class CodigoMatriculaPEspecificoDTO
    {
        public string CodigoMatricula { get; set; }
        public string PEspecifico { get; set; }
        public string VersionPrograma { get; set; }
    }

    public class EditarCongelamientoPEspecificoMatriculaAlumnoDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPEspecifico { get; set; }
        public int idEsquemaEvaluacionGeneral { get; set; }
    }
    public class DatosCursoMatriculaDTO
    {
        public int idMatriculaCabecera { get; set; }
        public int idPEspecifico { get; set; }
        public string nombrePEspecifico { get; set; }
        public int idPGeneral { get; set; }
        public string nombrePGeneral { get; set; }
        public int idCentroCosto { get; set; }
        public string nombreCentroCosto { get; set; }
    }
    
}
