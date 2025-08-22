using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPagoDetalleMod
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con T_MatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Numero de la cuota
        /// </summary>
        public int? NroCuota { get; set; }
        /// <summary>
        /// Numero de la sub Cuota
        /// </summary>
        public int? NroSubCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento de la cuota
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }
        /// <summary>
        /// Monto total a pagar
        /// </summary>
        public decimal? TotalPagar { get; set; }
        /// <summary>
        /// Monto de la cuota  a pagar
        /// </summary>
        public decimal? Cuota { get; set; }
        /// <summary>
        /// Monto a pagar por mora
        /// </summary>
        public decimal Mora { get; set; }
        /// <summary>
        /// Monto total de la cuota pagado
        /// </summary>
        public decimal MontoPagado { get; set; }
        /// <summary>
        /// saldo restante por pagar del total de la deuda 
        /// </summary>
        public decimal? Saldo { get; set; }
        /// <summary>
        /// Estado de la cuota, cancelado o no (1,0)
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// Tipo de concepto de pago (cuota, matricula)
        /// </summary>
        public string? TipoCuota { get; set; }
        /// <summary>
        /// Tipo de moneda de pago (soles, dolares, etc)
        /// </summary>
        public string? Moneda { get; set; }
        /// <summary>
        /// Fecha de pago de la cuota
        /// </summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>
        /// Llave foranea con T_FormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Llave foranea con T_CuentaCorriente
        /// </summary>
        public int? IdCuentaCorriente { get; set; }
        /// <summary>
        /// Fecha de pago en el Banco
        /// </summary>
        public DateTime? FechaPagoBanco { get; set; }
        /// <summary>
        /// Estado del envio del cronograma al alumno (1, 0)
        /// </summary>
        public bool Enviado { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Llave foranea con T_DocumentoPago
        /// </summary>
        public int? IdDocumentoPago { get; set; }
        /// <summary>
        /// Numero del documento de pago
        /// </summary>
        public string? NroDocumento { get; set; }
        /// <summary>
        /// Tipo de moneda de pago (soles, dolares, etc)
        /// </summary>
        public string? MonedaPago { get; set; }
        /// <summary>
        /// Tipo de cambio
        /// </summary>
        public decimal? TipoCambio { get; set; }
        /// <summary>
        /// Fecha de registro del proceso de pagos
        /// </summary>
        public DateTime? FechaProcesoPago { get; set; }
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
