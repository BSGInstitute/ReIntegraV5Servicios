using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Detalle de los rangos horarios asociados a un bloqueo de horario, indicando horas de inicio y fin, estado y auditoría.
    /// </summary>
    public partial class TBloqueHorarioDetalle
    {
        public TBloqueHorarioDetalle()
        {
            TPreferenciaComunicacionAcademicas = new HashSet<TPreferenciaComunicacionAcademica>();
        }

        /// <summary>
        /// Identificador único del detalle de bloqueo horario.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del bloqueo de horario al que pertenece el detalle.
        /// </summary>
        public int IdBloqueHorario { get; set; }
        /// <summary>
        /// Hora de inicio del rango de bloqueo horario.
        /// </summary>
        public TimeSpan HoraInicio { get; set; }
        /// <summary>
        /// Hora de fin del rango de bloqueo horario.
        /// </summary>
        public TimeSpan HoraFin { get; set; }
        /// <summary>
        /// Estado del registro de bloqueo horario (1 = Activo, 0 = Inactivo).
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
        /// Control de versión de fila para manejo de concurrencia.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TBloqueHorario IdBloqueHorarioNavigation { get; set; } = null!;
        public virtual ICollection<TPreferenciaComunicacionAcademica> TPreferenciaComunicacionAcademicas { get; set; }
    }
}
