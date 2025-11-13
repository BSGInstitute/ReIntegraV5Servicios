using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Relaciona las transiciones entre fases de oportunidad con los criterios de calificación que condicionan dichas transiciones.
    /// </summary>
    public partial class TTransicionFaseCriterioOportunidad
    {
        /// <summary>
        /// Clave primaria del registro de relación.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador de la transición de fases a la que pertenece el criterio.
        /// </summary>
        public int IdTransicionFaseOportunidad { get; set; }
        /// <summary>
        /// Identificador del criterio de calificación de fase asociado a la transición.
        /// </summary>
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
        /// <summary>
        /// Campo de auditoría Estado (eliminación lógica) del registro.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoría Usuario Creación del registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoría Usuario Modificación del registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoría Fecha Creación del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo de auditoría Fecha Modificación del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de control de concurrencia (RowVersion).
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Identificador de migración utilizado para procesos de sincronización.
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TCriterioCalificacionFaseOportunidad IdCriterioCalificacionFaseOportunidadNavigation { get; set; } = null!;
        public virtual TTransicionFaseOportunidad IdTransicionFaseOportunidadNavigation { get; set; } = null!;
    }
}
