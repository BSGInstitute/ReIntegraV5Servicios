using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla para registro de docentes postulantes
    /// </summary>
    public partial class TDocentePostulante
    {
        /// <summary>
        /// Identificador único del docente postulante (Llave primaria)
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
        /// Foreign Key que referencia a la ciudad de origen del docente postulante
        /// </summary>
        public int? IdCiudad { get; set; }
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TCiudad? IdCiudadNavigation { get; set; }
    }
}
