using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Registra las preferencias de comunicación académica de los alumnos, indicando el medio de comunicación y el bloque horario permitido.
    /// </summary>
    public partial class TPreferenciaComunicacionAcademica
    {
        /// <summary>
        /// Identificador único de la preferencia de comunicación académica.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del alumno al que pertenece la preferencia de comunicación.
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Identificador del medio de comunicación preferido por el alumno (WhatsApp, llamada, correo, etc.).
        /// </summary>
        public int IdMedioComunicacion { get; set; }
        /// <summary>
        /// Estado de la preferencia de comunicación (1 = Activa, 0 = Inactiva).
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Fecha y hora en la que se creó el registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Usuario que realizó la creación del registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de la última modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Usuario que realizó la última modificación del registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Control de versión de fila para manejo de concurrencia y control de cambios.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TAlumno IdAlumnoNavigation { get; set; } = null!;
        public virtual TMedioComunicacion IdMedioComunicacionNavigation { get; set; } = null!;
    }
}
