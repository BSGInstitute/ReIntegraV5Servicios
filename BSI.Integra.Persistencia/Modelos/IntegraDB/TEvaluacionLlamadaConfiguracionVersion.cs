using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Versiones de configuración de evaluación de llamadas. El área se determina implícitamente por los Lineamientos y PuntosGenerales asociados.
    /// </summary>
    public partial class TEvaluacionLlamadaConfiguracionVersion
    {
        public TEvaluacionLlamadaConfiguracionVersion()
        {
            TEvaluacionLlamadaLineamientos = new HashSet<TEvaluacionLlamadaLineamiento>();
            TEvaluacionLlamadaPuntoGenerals = new HashSet<TEvaluacionLlamadaPuntoGeneral>();
        }

        /// <summary>
        /// Identificador único de la versión de configuración
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Descripción de la versión de configuración
        /// </summary>
        public string? DescripcionVersion { get; set; }
        /// <summary>
        /// Indica si esta versión es la vigente actualmente
        /// </summary>
        public bool EsVigente { get; set; }
        /// <summary>
        /// Comentarios adicionales sobre la versión
        /// </summary>
        public string? Comentario { get; set; }
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

        public virtual ICollection<TEvaluacionLlamadaLineamiento> TEvaluacionLlamadaLineamientos { get; set; }
        public virtual ICollection<TEvaluacionLlamadaPuntoGeneral> TEvaluacionLlamadaPuntoGenerals { get; set; }
    }
}
