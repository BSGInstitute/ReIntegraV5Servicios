using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Define criterios adicionales que condicionan la transición entre fases de oportunidad.
    /// </summary>
    public partial class TCriterioCalificacionFaseOportunidad
    {
        public TCriterioCalificacionFaseOportunidad()
        {
            TLineamientoCalificacionFases = new HashSet<TLineamientoCalificacionFase>();
            TTransicionFaseCriterioOportunidads = new HashSet<TTransicionFaseCriterioOportunidad>();
        }

        /// <summary>
        /// Clave primaria del criterio.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Orden del criterio (entero ≥ 1, puede repetirse).
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Nombre del criterio.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del criterio.
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria Estado (eliminacion logica) del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Campo de auditoria Usuario Creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria Usuario Modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Campo de auditoria Fecha Creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Campo de auditoria Fecha Modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de auditoria RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual ICollection<TLineamientoCalificacionFase> TLineamientoCalificacionFases { get; set; }
        public virtual ICollection<TTransicionFaseCriterioOportunidad> TTransicionFaseCriterioOportunidads { get; set; }
    }
}
