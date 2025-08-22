using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TExamenRealizadoRespuestaEvaluador
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fk T_ExamenAsignadoEvaluador
        /// </summary>
        public int IdExamenAsignadoEvaluador { get; set; }
        /// <summary>
        /// Fk T_Pregunta
        /// </summary>
        public int IdPregunta { get; set; }
        /// <summary>
        /// Fk T_RespuestaPregunta
        /// </summary>
        public int IdRespuestaPregunta { get; set; }
        /// <summary>
        /// Texto respuesta
        /// </summary>
        public string? TextoRespuesta { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TExamenAsignadoEvaluador IdExamenAsignadoEvaluadorNavigation { get; set; } = null!;
        public virtual TPreguntum IdPreguntaNavigation { get; set; } = null!;
        public virtual TRespuestaPreguntum IdRespuestaPreguntaNavigation { get; set; } = null!;
    }
}
