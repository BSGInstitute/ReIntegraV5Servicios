using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelRepLegItem
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key TCRM_SENTINEL
        /// </summary>
        public int? IdSentinel { get; set; }
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string TipoDocumento { get; set; } = null!;
        /// <summary>
        /// Numero de documento
        /// </summary>
        public string NumeroDocumento { get; set; } = null!;
        /// <summary>
        /// Nombres
        /// </summary>
        public string? Nombres { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string? ApellidoPaterno { get; set; }
        /// <summary>
        /// Apellido materno
        /// </summary>
        public string? ApellidoMaterno { get; set; }
        /// <summary>
        /// Nombre de la razon social
        /// </summary>
        public string? RazonSocial { get; set; }
        /// <summary>
        /// Cargo
        /// </summary>
        public string? Cargo { get; set; }
        /// <summary>
        /// Semaforo Actual
        /// </summary>
        public string SemaforoActual { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de la tabla Original al migrar
        /// </summary>
        public Guid? IdMigracion { get; set; }

        public virtual TSentinel? IdSentinelNavigation { get; set; }
    }
}
