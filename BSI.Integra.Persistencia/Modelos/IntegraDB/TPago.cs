using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPago
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_MatriculaCabacera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Monto de pago
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Tipo de moneda en que se hace el pago
        /// </summary>
        public string Moneda { get; set; } = null!;
        /// <summary>
        /// Tipo de cambio para el pago
        /// </summary>
        public double? TipoCambio { get; set; }
        /// <summary>
        /// Concepto
        /// </summary>
        public string? Concepto { get; set; }
        /// <summary>
        /// Numero de RUC
        /// </summary>
        public string? Ruc { get; set; }
        /// <summary>
        /// Llave foranea con la tabl T_FormaPago Forma de cobro
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Numero de serie
        /// </summary>
        public string? SerieNumero { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CuentaCorriente, Numero de cuenta
        /// </summary>
        public int? IdCuentaCorriente { get; set; }
        /// <summary>
        /// Numero de cheque
        /// </summary>
        public string? NroRefCheque { get; set; }
        /// <summary>
        /// Fecha del documento
        /// </summary>
        public DateTime? FechaDocumento { get; set; }
        /// <summary>
        /// Numero de deposito
        /// </summary>
        public string? NroDeposito { get; set; }
        /// <summary>
        /// Fecha de pago
        /// </summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>
        /// Llave forane con l atabla T_DocumentoPago, Numero de documentos
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
        public int? IdMigracion { get; set; }
    }
}
