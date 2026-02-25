using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla pivote que relaciona los criterios de tarea con las configuraciones de evaluación de trabajo
    /// </summary>
    public partial class TTareaCriterioConfiguracion
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_CriterioTarea
        /// </summary>
        public int IdTareaCriterio { get; set; }
        /// <summary>
        /// Identificador de la tabla pla.T_ConfigurarEvaluacionTrabajo
        /// </summary>
        public int IdConfigurarEvaluacionTrabajo { get; set; }
        /// <summary>
        /// Creado o eliminado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TConfigurarEvaluacionTrabajo IdConfigurarEvaluacionTrabajoNavigation { get; set; } = null!;
        public virtual TTareaCriterio IdTareaCriterioNavigation { get; set; } = null!;
    }
}
