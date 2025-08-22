using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPagoDetalleModLogFinal
    {
        /// <summary>
        /// es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreign Key de la tabla T_MatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Fecha de la ultima modificacion del registro
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// numero de cuotas
        /// </summary>
        public int? NroCuota { get; set; }
        /// <summary>
        /// numero de subcuotas
        /// </summary>
        public int? NroSubCuota { get; set; }
        /// <summary>
        /// fecha de vencimiento
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }
        /// <summary>
        /// total a pagar
        /// </summary>
        public decimal? TotalPagar { get; set; }
        /// <summary>
        /// cuota
        /// </summary>
        public decimal? Cuota { get; set; }
        /// <summary>
        /// mora
        /// </summary>
        public decimal? Mora { get; set; }
        /// <summary>
        /// monto pagado
        /// </summary>
        public decimal? MontoPagado { get; set; }
        /// <summary>
        /// saldo
        /// </summary>
        public decimal? Saldo { get; set; }
        /// <summary>
        /// Estado de la cuota, cancelado o no (1,0)
        /// </summary>
        public bool? Cancelado { get; set; }
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
        /// Es Foreign Key de T_FormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Fecha de pago en el Banco
        /// </summary>
        public DateTime? FechaPagoBanco { get; set; }
        /// <summary>
        /// Indica si es la ultima cuota o no
        /// </summary>
        public bool? Ultimo { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Es foreing key tDocumentoPago
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
        /// Mensaje del sistema
        /// </summary>
        public string? MensajeSistema { get; set; }
        /// <summary>
        /// Fecha de registro del proceso de pagos
        /// </summary>
        public DateTime? FechaProcesoPago { get; set; }
        /// <summary>
        /// Estado del registro en tCronogramaPagosDetalle_mod
        /// </summary>
        public string? EstadoPrimerLog { get; set; }
        /// <summary>
        /// version
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// Estado de Aprobacion
        /// </summary>
        public bool? Aprobado { get; set; }
        /// <summary>
        /// estado 2
        /// </summary>
        public bool? Estado2 { get; set; }
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
    }
}
