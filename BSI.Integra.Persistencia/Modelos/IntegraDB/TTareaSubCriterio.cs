using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla que almacena los subcriterios de evaluacion asociados a los criterios de tarea
    /// </summary>
    public partial class TTareaSubCriterio
    {
        public TTareaSubCriterio()
        {
            TTareaCriterioSubCriterios = new HashSet<TTareaCriterioSubCriterio>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del subcriterio de tarea
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion detallada del subcriterio de tarea
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Valor de escala de calificacion asignado al subcriterio
        /// </summary>
        public int Escala { get; set; }
        /// <summary>
        /// Indica si el subcriterio esta activo para su uso en evaluaciones
        /// </summary>
        public bool Activo { get; set; }
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

        public virtual ICollection<TTareaCriterioSubCriterio> TTareaCriterioSubCriterios { get; set; }
    }
}
