using BSI.Integra.Aplicacion.Base;

namespace BSI.Integra.Persistencia.Entidades.IntegraDB.Planificacion
{
    /// <summary>
    /// Entidad para el manejo de docentes postulantes
    /// </summary>
    public class DocentePostulante : BaseIntegraEntity
    {
        /// <summary>
        /// Primer nombre del docente postulante
        /// </summary>
        public string Nombre1 { get; set; } = null!;

        /// <summary>
        /// Segundo nombre del docente postulante
        /// </summary>
        public string? Nombre2 { get; set; }

        /// <summary>
        /// Apellido paterno del docente postulante
        /// </summary>
        public string ApellidoPaterno { get; set; } = null!;

        /// <summary>
        /// Apellido materno del docente postulante
        /// </summary>
        public string? ApellidoMaterno { get; set; }

        /// <summary>
        /// Número de documento de identidad del docente postulante
        /// </summary>
        public string NumeroDocumento { get; set; } = null!;

        /// <summary>
        /// Fecha de nacimiento del docente postulante
        /// </summary>
        public DateTime? FechaNacimiento { get; set; }

        /// <summary>
        /// Número de teléfono fijo del docente postulante
        /// </summary>
        public string? Telefono { get; set; }

        /// <summary>
        /// Número de celular del docente postulante
        /// </summary>
        public string? Celular { get; set; }

        /// <summary>
        /// Correo electrónico del docente postulante
        /// </summary>
        public string? Correo { get; set; }

        /// <summary>
        /// ID de la ciudad de origen del docente postulante
        /// </summary>
        public int? IdCiudad { get; set; }
    }
}
