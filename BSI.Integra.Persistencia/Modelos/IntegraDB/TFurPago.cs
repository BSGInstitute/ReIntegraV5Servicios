using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TFurPago
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Fur
        /// </summary>
        public int? IdFur { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_ComprobantePago
        /// </summary>
        public int? IdComprobantePago { get; set; }
        /// <summary>
        /// Numero del pago
        /// </summary>
        public int NumeroPago { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Numero de recibo
        /// </summary>
        public string NumeroRecibo { get; set; } = null!;
        /// <summary>
        /// Llave foranea con la tabla T_FormaPago
        /// </summary>
        public int IdFormaPago { get; set; }
        /// <summary>
        /// Fecha de cobro en el banco
        /// </summary>
        public DateTime FechaCobroBanco { get; set; }
        /// <summary>
        /// Precio total en la moneda original
        /// </summary>
        public decimal PrecioTotalMonedaOrigen { get; set; }
        /// <summary>
        /// Precio total en dolares
        /// </summary>
        public decimal PrecioTotalMonedaDolares { get; set; }
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
        /// <summary>
        /// Llave foranea con la tabla T_CuentaCorriente
        /// </summary>
        public int IdCuentaCorriente { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_
        /// </summary>
        public int? IdComprobantePagoPorFur { get; set; }
    }
}
