using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los criterios de evaluacion de llamada
    /// </summary>
    public partial class TCriterioCalificacionLlamadum
    {
        public TCriterioCalificacionLlamadum()
        {
            TLineamientoCalificacions = new HashSet<TLineamientoCalificacion>();
        }

        /// <summary>
        /// Identificador único del criterio
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la fase a la que pertenece el criterio
        /// </summary>
        public int IdFaseCalificacion { get; set; }
        /// <summary>
        /// Nombre del criterio
        /// </summary>
        public string NombreCriterio { get; set; } = null!;
        /// <summary>
        /// Orden de criterio
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción del criterio
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (activo o inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión del registro para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TFaseCalificacion IdFaseCalificacionNavigation { get; set; } = null!;
        public virtual ICollection<TLineamientoCalificacion> TLineamientoCalificacions { get; set; }
    }
}
