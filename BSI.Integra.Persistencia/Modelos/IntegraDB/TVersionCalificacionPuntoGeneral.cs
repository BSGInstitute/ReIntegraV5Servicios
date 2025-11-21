using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Puntos generales congelados por versión de configuración de evaluación
    /// </summary>
    public partial class TVersionCalificacionPuntoGeneral
    {
        /// <summary>
        /// (PK) Primary Key del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del punto general
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Orden del punto
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción del punto general
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Estado del registro
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
        /// Control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// (FK) Referencia a la versión de configuración de evaluación de llamadas
        /// </summary>
        public int? IdEvaluacionLlamadaConfiguracionVersion { get; set; }

        public virtual TEvaluacionLlamadaConfiguracionVersion? IdEvaluacionLlamadaConfiguracionVersionNavigation { get; set; }
    }
}
