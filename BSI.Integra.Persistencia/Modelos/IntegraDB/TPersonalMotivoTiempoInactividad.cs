using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalMotivoTiempoInactividad
    {
        /// <summary>
        /// PK de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK de T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// FK de T_MotivoInactividad
        /// </summary>
        public int IdMotivoInactividad { get; set; }
        /// <summary>
        /// Fecha Inicio de Periodo de Inactividad
        /// </summary>
        public DateTime? FechaInicio { get; set; }
        /// <summary>
        /// Fecha Fin de Periodo de Inactividad
        /// </summary>
        public DateTime? FechaFin { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha de creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public int? IdMigracion { get; set; }

        public virtual TMotivoInactividad IdMotivoInactividadNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
    }
}
