using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class THistoricoProductoProveedor
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con T_Producto
        /// </summary>
        public int IdProducto { get; set; }
        /// <summary>
        /// Llave foranea con T_Proveedor
        /// </summary>
        public int IdProveedor { get; set; }
        /// <summary>
        /// Costo Moneda Origen
        /// </summary>
        public decimal CostoMonedaOrigen { get; set; }
        /// <summary>
        /// Costo Dolares
        /// </summary>
        public decimal CostoDolares { get; set; }
        /// <summary>
        /// Llave foranea con T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Tipo de Cambio
        /// </summary>
        public decimal TipoCambio { get; set; }
        /// <summary>
        /// Llave foranea con T_CondicionPago
        /// </summary>
        public int? IdCondicionPago { get; set; }
        /// <summary>
        /// Llave foranea con T_CondicionTipoPago
        /// </summary>
        public int IdCondicionTipoPago { get; set; }
        /// <summary>
        /// Version
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string Observaciones { get; set; } = null!;
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
        public Guid? IdMigracion { get; set; }
    }
}
