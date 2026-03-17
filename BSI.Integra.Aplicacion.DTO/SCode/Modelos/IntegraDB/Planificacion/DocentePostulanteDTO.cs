namespace BSI.Integra.Aplicacion.DTO.SCode.Modelos.IntegraDB.Planificacion
{
    /// <summary>
    /// DTO para el manejo de docentes postulantes
    /// </summary>
    public class DocentePostulanteDTO
    {
        /// <summary>
        /// Identificador único del docente postulante
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Nombre de la ciudad (para visualización)
        /// </summary>
        public string? NombreCiudad { get; set; }

        /// <summary>
        /// Nombre completo del docente postulante
        /// </summary>
        public string? NombreCompleto { get; set; }

        /// <summary>
        /// Estado de gestión del contacto (1 = General sin curso, 2 = Asignado a curso)
        /// </summary>
        public int? IdEstadoGestionContacto { get; set; }

        /// <summary>
        /// ID del centro de costo (curso) - requerido solo si IdEstadoGestionContacto = 2
        /// </summary>
        public int? IdCentroCosto { get; set; }
    }
}
