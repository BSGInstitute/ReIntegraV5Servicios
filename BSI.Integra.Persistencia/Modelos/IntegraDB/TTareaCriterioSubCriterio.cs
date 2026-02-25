using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla pivote que relaciona los criterios de tarea con sus subcriterios (relacion N:N)
    /// </summary>
    public partial class TTareaCriterioSubCriterio
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        public int IdTareaCriterio { get; set; }
        public int IdTareaSubCriterio { get; set; }
        /// <summary>
        /// Creado o eliminado
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
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[]? RowVersion { get; set; }

        public virtual TTareaCriterio IdTareaCriterioNavigation { get; set; } = null!;
        public virtual TTareaSubCriterio IdTareaSubCriterioNavigation { get; set; } = null!;
    }
}
