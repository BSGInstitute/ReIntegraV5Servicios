using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPagoFinal
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea que hace referencia a la tabla T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Monto de pago
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Moneda de pago
        /// </summary>
        public string Moneda { get; set; } = null!;
        /// <summary>
        /// Tipo cambio de moneda
        /// </summary>
        public double? TipoCambio { get; set; }
        /// <summary>
        /// Concepto de pago
        /// </summary>
        public string? Concepto { get; set; }
        /// <summary>
        /// Numero de ruc 
        /// </summary>
        public string? Ruc { get; set; }
        /// <summary>
        /// Llave foranea que hace referencia a la tabla T_FormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Numero de serie
        /// </summary>
        public string SerieNumero { get; set; } = null!;
        /// <summary>
        /// Llave foranea que hace referencia a la tabla T_CuentaCorriente
        /// </summary>
        public int? IdCuentaCorriente { get; set; }
        /// <summary>
        /// Numero de referencia cheque
        /// </summary>
        public string? NroRefCheque { get; set; }
        /// <summary>
        /// Fecha de documento
        /// </summary>
        public DateTime FechaDocumento { get; set; }
        /// <summary>
        /// Numero de deposito
        /// </summary>
        public string? NroDeposito { get; set; }
        /// <summary>
        /// Fecha de pago
        /// </summary>
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Estado de pago 
        /// </summary>
        public bool EstadoPago { get; set; }
        /// <summary>
        /// Llave foranea que hace referencia a la tabla T_DocumentoPago
        /// </summary>
        public int? IdDocumentoPago { get; set; }
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
