using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Fases congeladas por versión de configuración
    /// </summary>
    public partial class TVersionFaseCalificacion
    {
        public TVersionFaseCalificacion()
        {
            TVersionCriterioCalificacions = new HashSet<TVersionCriterioCalificacion>();
        }

        /// <summary>
        /// (PK) Primary Key del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la fase
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Orden dentro de la versión
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción de la fase
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro (activo o inactivo)
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
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
        /// Control de concurrencia (RowVersion)
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la configuración de versión de evaluación de llamada
        /// </summary>
        public int? IdEvaluacionLlamadaConfiguracionVersion { get; set; }

        public virtual TEvaluacionLlamadaConfiguracionVersion? IdEvaluacionLlamadaConfiguracionVersionNavigation { get; set; }
        public virtual ICollection<TVersionCriterioCalificacion> TVersionCriterioCalificacions { get; set; }
    }
}
