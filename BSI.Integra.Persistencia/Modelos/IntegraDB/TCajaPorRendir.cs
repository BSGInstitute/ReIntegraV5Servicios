using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCajaPorRendir
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key tCajas
        /// </summary>
        public int? IdCaja { get; set; }
        /// <summary>
        /// Es Foreing Key tCajas_PR_Cabecera
        /// </summary>
        public int? IdCajaPorRendirCabecera { get; set; }
        /// <summary>
        /// Es Foreing Key tFur
        /// </summary>
        public int? IdFur { get; set; }
        /// <summary>
        /// Nombre del usuario que solicita
        /// </summary>
        public int IdPersonalSolicitante { get; set; }
        /// <summary>
        /// Nombre del responsable de caja
        /// </summary>
        public int IdPersonalResponsableCaja { get; set; }
        /// <summary>
        /// Descripcion del pedido
        /// </summary>
        public string Descripcion { get; set; } = null!;
        /// <summary>
        /// Tipo de moneda (No se usa)
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Total del efectivo solicitado
        /// </summary>
        public decimal TotalEfectivo { get; set; }
        /// <summary>
        /// Fecha en la que se entrega el efectivo solicitado
        /// </summary>
        public DateTime FechaEntregaEfectivo { get; set; }
        /// <summary>
        /// El estado del efectivo si fue entregado o no
        /// </summary>
        public bool EsEnviado { get; set; }
        /// <summary>
        /// Fecha en que se envio la solicitud
        /// </summary>
        public DateTime? FechaEnvio { get; set; }
        /// <summary>
        /// Fecha en la que se aprobo la solicitud
        /// </summary>
        public DateTime? FechaAprobacion { get; set; }
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
    }
}
