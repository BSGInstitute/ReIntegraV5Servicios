using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la clasificación de los registros del alumno según el nivel y esquema del embudo.
    /// </summary>
    public partial class TRemarketingEmbudoHistorico
    {
        /// <summary>
        /// Identificador único del nivel de embudo. Clave primaria autoincremental.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foránea a [ia].[T_RemarketingEmbudoNivel].
        /// </summary>
        public int IdRemarketingEmbudoNivel { get; set; }
        /// <summary>
        /// Clave foránea a [mkt].[T_Alumno].
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Fecha del clasificación del registro en el nivel de embudo.
        /// </summary>
        public DateTime FechaClasificacion { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado).
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario creacion del registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion del registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha creacion del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TAlumno IdAlumnoNavigation { get; set; } = null!;
        public virtual TRemarketingEmbudoNivel IdRemarketingEmbudoNivelNavigation { get; set; } = null!;
    }
}
