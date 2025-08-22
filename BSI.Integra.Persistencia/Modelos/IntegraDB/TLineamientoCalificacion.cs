using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena los lineamientos de evaluacion de llamada
    /// </summary>
    public partial class TLineamientoCalificacion
    {
        /// <summary>
        /// Identificador único del lineamiento
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador del criterio al que pertenece el lineamiento
        /// </summary>
        public int IdCriterioCalificacionLlamada { get; set; }
        /// <summary>
        /// Identificador de la criticidad asociada al lineamiento
        /// </summary>
        public int IdCriticidadCalificacion { get; set; }
        /// <summary>
        /// Nombre del lineamiento
        /// </summary>
        public string NombreLineamiento { get; set; } = null!;
        /// <summary>
        /// Orden del lineamiento
        /// </summary>
        public int? Orden { get; set; }
        /// <summary>
        /// Descripción del lineamiento
        /// </summary>
        public string? Descripcion { get; set; }
        /// <summary>
        /// Herramienta de analisis usada
        /// </summary>
        public string? HerramientaAnalisis { get; set; }
        /// <summary>
        /// Número de versión del lineamiento (control de cambios)
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// Indica si la versión del lineamiento está vigente (1) o no (0)
        /// </summary>
        public bool? EsVigente { get; set; }
        /// <summary>
        /// Fecha Inicio siendo vigente
        /// </summary>
        public DateTime? FechaVigenciaInicio { get; set; }
        /// <summary>
        /// Fecha Fin siendo vigente
        /// </summary>
        public DateTime? FechaVigenciaFin { get; set; }
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

        public virtual TCriterioCalificacionLlamadum IdCriterioCalificacionLlamadaNavigation { get; set; } = null!;
        public virtual TCriticidadCalificacion IdCriticidadCalificacionNavigation { get; set; } = null!;
    }
}
