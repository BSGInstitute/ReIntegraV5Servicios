using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Criterios de evaluación por fase. El área se hereda de los Lineamientos asociados.
    /// </summary>
    public partial class TEvaluacionLlamadaCriterio
    {
        public TEvaluacionLlamadaCriterio()
        {
            TEvaluacionLlamadaLineamientos = new HashSet<TEvaluacionLlamadaLineamiento>();
        }

        /// <summary>
        /// Identificador único del criterio
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la fase a la que pertenece el criterio
        /// </summary>
        public int IdEvaluacionLlamadaFase { get; set; }
        /// <summary>
        /// Nombre del criterio de evaluación
        /// </summary>
        public string NombreCriterio { get; set; } = null!;
        /// <summary>
        /// Orden de presentación del criterio
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción detallada del criterio
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro por última vez
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de versión de fila para concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TEvaluacionLlamadaFase IdEvaluacionLlamadaFaseNavigation { get; set; } = null!;
        public virtual ICollection<TEvaluacionLlamadaLineamiento> TEvaluacionLlamadaLineamientos { get; set; }
    }
}
