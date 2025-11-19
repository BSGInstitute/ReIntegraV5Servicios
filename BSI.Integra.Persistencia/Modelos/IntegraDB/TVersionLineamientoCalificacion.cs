using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Lineamientos congelados por versión
    /// </summary>
    public partial class TVersionLineamientoCalificacion
    {
        /// <summary>
        /// (PK) Primary Key del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del lineamiento
        /// </summary>
        public string NombreLineamiento { get; set; } = null!;
        /// <summary>
        /// Orden en la lista
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción del lineamiento
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Referencia al criterio congelado
        /// </summary>
        public int IdVersionCriterioCalificacion { get; set; }
        /// <summary>
        /// (FK) Referencia a la criticidad congelada en la versión
        /// </summary>
        public int? IdVersionCriticidadCalificacion { get; set; }

        public virtual TVersionCriterioCalificacion IdVersionCriterioCalificacionNavigation { get; set; } = null!;
        public virtual TVersionCriticidadCalificacion? IdVersionCriticidadCalificacionNavigation { get; set; }
    }
}
