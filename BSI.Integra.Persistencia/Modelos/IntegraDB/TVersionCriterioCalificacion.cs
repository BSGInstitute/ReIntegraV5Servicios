using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Criterios congelados por versión
    /// </summary>
    public partial class TVersionCriterioCalificacion
    {
        public TVersionCriterioCalificacion()
        {
            TVersionLineamientoCalificacions = new HashSet<TVersionLineamientoCalificacion>();
        }

        /// <summary>
        /// (PK) Primary Key del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del criterio
        /// </summary>
        public string NombreCriterio { get; set; } = null!;
        /// <summary>
        /// Orden del criterio
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción del criterio
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
        /// Referencia a la fase congelada
        /// </summary>
        public int IdVersionFaseCalificacion { get; set; }

        public virtual TVersionFaseCalificacion IdVersionFaseCalificacionNavigation { get; set; } = null!;
        public virtual ICollection<TVersionLineamientoCalificacion> TVersionLineamientoCalificacions { get; set; }
    }
}
