using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenComportamiento
    {
        /// <summary>
        /// Clave primario del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// hace referencia a si todas las preguntas del examen tiene que ser repondida obligatoriamente
        /// </summary>
        public bool PreguntaObligatoria { get; set; }
        /// <summary>
        /// primary key de gp.T_EvaluacionFeedback
        /// </summary>
        public int? IdEvaluacionFeedbackAprobado { get; set; }
        /// <summary>
        /// primary key de gp.T_EvaluacionFeedback
        /// </summary>
        public int? IdEvaluacionFeedbackDesaprobado { get; set; }
        /// <summary>
        /// primary key de gp.T_EvaluacionFeedback
        /// </summary>
        public int? IdEvaluacionFeedbackCancelado { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
