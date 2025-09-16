using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define las transiciones permitidas entre fases de oportunidad, incluyendo su control de auditoría.
    /// </summary>
    public partial class TTransicionFaseOportunidad
    {
        public TTransicionFaseOportunidad()
        {
            TTransicionFaseCriterioOportunidads = new HashSet<TTransicionFaseCriterioOportunidad>();
        }

        /// <summary>
        /// Clave primaria de la transición entre fases.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Fase de oportunidad origen de la transición.
        /// </summary>
        public int IdFaseOportunidadOrigen { get; set; }
        /// <summary>
        /// Fase de oportunidad destino de la transición.
        /// </summary>
        public int IdFaseOportunidadDestino { get; set; }
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

        public virtual TFaseOportunidad IdFaseOportunidadDestinoNavigation { get; set; } = null!;
        public virtual TFaseOportunidad IdFaseOportunidadOrigenNavigation { get; set; } = null!;
        public virtual ICollection<TTransicionFaseCriterioOportunidad> TTransicionFaseCriterioOportunidads { get; set; }
    }
}
