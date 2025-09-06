using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Especifica lineamientos o pautas de evaluacion asociados a un criterio de transicion de fase.
    /// </summary>
    public partial class TLineamientoCalificacionFase
    {
        /// <summary>
        /// Clave primaria del lineamiento.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Criterio asociado.
        /// </summary>
        public int IdCriterioCalificacionFaseOportunidad { get; set; }
        /// <summary>
        /// Orden del lineamiento (entero ≥ 1, puede repetirse).
        /// </summary>
        public int Orden { get; set; }
        /// <summary>
        /// Criticidad asociada (FK a com.T_CriticidadCalificacion).
        /// </summary>
        public int IdCriticidadCalificacion { get; set; }
        /// <summary>
        /// Nombre del lineamiento.
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripción del lineamiento.
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
        /// Campo de auditoria IdMigracion del registro
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TCriterioCalificacionFaseOportunidad IdCriterioCalificacionFaseOportunidadNavigation { get; set; } = null!;
        public virtual TCriticidadCalificacion IdCriticidadCalificacionNavigation { get; set; } = null!;
    }
}
