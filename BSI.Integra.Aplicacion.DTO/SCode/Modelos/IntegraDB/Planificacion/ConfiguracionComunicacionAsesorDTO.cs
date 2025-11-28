using System;

namespace BSI.Integra.Aplicacion.DTO.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para matrícula activa obtenida desde IdAlumno
    /// </summary>
    public class MatriculaActivaAsesorDTO
    {
        public int IdMatriculaCabecera { get; set; }
        public int IdPGeneral { get; set; }
        public string? EstadoMatricula { get; set; }
        public DateTime? FechaMatricula { get; set; }
        public string? CodigoMatricula { get; set; }
    }

    /// <summary>
    /// DTO del catálogo de horarios de contacto
    /// </summary>
    public class CatalogoHorarioContactoAsesorDTO
    {
        public int IdHorarioContacto { get; set; }
        public string? TipoHorario { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string? IntervaloTexto { get; set; }
        public int OrdenVisualizacion { get; set; }
        public bool Estado { get; set; }
    }

    /// <summary>
    /// DTO para obtener configuración del alumno (para mostrar en frontend)
    /// </summary>
    public class ConfiguracionComunicacionAsesorDTO
    {
        public int Id { get; set; }
        public int IdMatriculaCabecera { get; set; }
        public int IdPGeneral { get; set; }
        public int IdHorarioContacto { get; set; }
        public string? TipoHorario { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string? IntervaloTexto { get; set; }
        public int OrdenVisualizacion { get; set; }
        public bool MedioWhatsApp { get; set; }
        public bool MedioLlamada { get; set; }
        public bool MedioCorreo { get; set; }
        public bool Estado { get; set; }
        public string? UsuarioCreacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    /// <summary>
    /// DTO para insertar/actualizar configuración desde asesor
    /// </summary>
    public class GuardarConfiguracionComunicacionAsesorDTO
    {
        public int IdAlumno { get; set; }
        public int IdHorarioContacto { get; set; }
        public bool MedioWhatsApp { get; set; }
        public bool MedioLlamada { get; set; }
        public bool MedioCorreo { get; set; }
        public bool Estado { get; set; }
        public string? Usuario { get; set; }
    }

    /// <summary>
    /// DTO de respuesta para el frontend
    /// </summary>
    public class ResponseConfiguracionComunicacionAsesorDTO
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Datos { get; set; }
    }
}