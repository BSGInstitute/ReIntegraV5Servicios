using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSdtRepSbsitem
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
        public string? TipoDoc { get; set; }
        /// <summary>
        /// Nro de documento
        /// </summary>
        public string? NroDoc { get; set; }
        /// <summary>
        /// Nombre de la razon social de la entidad
        /// </summary>
        public string? NombreRazonSocial { get; set; }
        /// <summary>
        /// Calificacion
        /// </summary>
        public string? Calificacion { get; set; }
        /// <summary>
        /// Monto de la deuda
        /// </summary>
        public decimal? MontoDeuda { get; set; }
        /// <summary>
        /// Nro de dias vencidos
        /// </summary>
        public int? DiasVencidos { get; set; }
        /// <summary>
        /// Fecha que se reporto
        /// </summary>
        public DateTime? FechaReporte { get; set; }
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
