using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Puntos generales de evaluación. Almacena IdPersonalAreaTrabajo e IdConfiguracionVersion. NULL en versión = editable, NOT NULL = congelado.
    /// </summary>
    public partial class TEvaluacionLlamadaPuntoGeneral
    {
        /// <summary>
        /// Identificador único del punto general
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del punto general de evaluación
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Orden de presentación del punto general
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción detallada del punto general
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Identificador del área de trabajo asociada
        /// </summary>
        public int IdPersonalAreaTrabajo { get; set; }
        /// <summary>
        /// Identificador de la versión de configuración. NULL indica que el registro es editable, NOT NULL indica que está congelado
        /// </summary>
        public int? IdEvaluacionLlamadaConfiguracionVersion { get; set; }
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

        public virtual TEvaluacionLlamadaConfiguracionVersion? IdEvaluacionLlamadaConfiguracionVersionNavigation { get; set; }
        public virtual TPersonalAreaTrabajo IdPersonalAreaTrabajoNavigation { get; set; } = null!;
    }
}
