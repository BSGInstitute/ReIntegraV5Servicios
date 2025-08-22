using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSdtLincreItem
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Sentinel
        /// </summary>
        public int? IdSentinel { get; set; }
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string? TipoDocumento { get; set; }
        /// <summary>
        /// Numero de documento
        /// </summary>
        public string? NumeroDocumento { get; set; }
        /// <summary>
        /// razon Social de la entidad
        /// </summary>
        public string? CnsEntNomRazLn { get; set; }
        /// <summary>
        /// Tipo de cuenta
        /// </summary>
        public string? TipoCuenta { get; set; }
        /// <summary>
        /// Linea de credito
        /// </summary>
        public decimal? LineaCredito { get; set; }
        /// <summary>
        /// Linea de credito no utilizada
        /// </summary>
        public decimal? LineaCreditoNoUtil { get; set; }
        /// <summary>
        /// Linea util
        /// </summary>
        public decimal? LineaUtil { get; set; }
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
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
