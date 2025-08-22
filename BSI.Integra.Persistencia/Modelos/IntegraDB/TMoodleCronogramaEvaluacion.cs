using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMoodleCronogramaEvaluacion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Id del Curso en Moodle
        /// </summary>
        public int IdCursoMoodle { get; set; }
        /// <summary>
        /// Id de la Evaluacion en Moodle
        /// </summary>
        public int? IdEvaluacionMoodle { get; set; }
        /// <summary>
        /// Nombre de la Evaluación en Moodle
        /// </summary>
        public string NombreEvaluacion { get; set; } = null!;
        /// <summary>
        /// Fecha de Estimada de Vencimiento de la Evaluacion
        /// </summary>
        public DateTime FechaEstimada { get; set; }
        /// <summary>
        /// Orden
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Version del Cronograma
        /// </summary>
        public int Version { get; set; }
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
    }
}
