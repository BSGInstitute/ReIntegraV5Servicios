using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla alamacena las encuestas asignadas a programas y sesiones
    /// </summary>
    public partial class TEncuestaSesionPrograma
    {
        /// <summary>
        /// Pk de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de tabla T_PGeneral
        /// </summary>
        public int? IdPgeneral { get; set; }
        /// <summary>
        /// Id de tabla PEspecifico
        /// </summary>
        public int? IdPespecifico { get; set; }
        /// <summary>
        /// Id de tabla  T_PEspecificoSesion
        /// </summary>
        public int? IdPespecificoSesion { get; set; }
        /// <summary>
        /// Id de tabla T_EncuestaOnline
        /// </summary>
        public int? IdEncuestaOnline { get; set; }
        /// <summary>
        /// flag que  indica si la encuesta es obligatoria
        /// </summary>
        public bool? EncuestaObligatoria { get; set; }
        /// <summary>
        /// flag que  indica si la encuesta esta activa
        /// </summary>
        public bool? EncuestaActiva { get; set; }
        /// <summary>
        /// Indica si esta asignado a docente o alumno
        /// </summary>
        public bool? AsignadoPara { get; set; }
        /// <summary>
        /// Estado de registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario Creacion de registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario modificacion de registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// FEcha Creacion de registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha Modificiacion de registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// row version de registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
