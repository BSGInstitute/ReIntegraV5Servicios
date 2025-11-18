using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Fases de evaluación de llamadas. El área se hereda de los Lineamientos asociados.
    /// </summary>
    public partial class TEvaluacionLlamadaFase
    {
        public TEvaluacionLlamadaFase()
        {
            TEvaluacionLlamadaCriterios = new HashSet<TEvaluacionLlamadaCriterio>();
        }

        /// <summary>
        /// Identificador único de la fase
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la fase de evaluación
        /// </summary>
        public string NombreFase { get; set; } = null!;
        /// <summary>
        /// Orden de presentación de la fase
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción detallada de la fase
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

        public virtual ICollection<TEvaluacionLlamadaCriterio> TEvaluacionLlamadaCriterios { get; set; }
    }
}
