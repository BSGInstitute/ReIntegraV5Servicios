using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPagoDetalleFinal
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key T_MatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Numero de la cuota
        /// </summary>
        public int? NroCuota { get; set; }
        /// <summary>
        /// Numero de Sub cuotas
        /// </summary>
        public int? NroSubCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento
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
        /// saldo
        /// </summary>
        public decimal? Saldo { get; set; }
        /// <summary>
        /// mora
        /// </summary>
        public decimal? Mora { get; set; }
        /// <summary>
        /// monto pagado
        /// </summary>
        public decimal? MontoPagado { get; set; }
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
        /// fecha de pago
        /// </summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>
        /// Es Foreign Key de tFormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// es Foreign Key de 
        /// </summary>
        public int? IdCuenta { get; set; }
        /// <summary>
        /// Fecha de pago en el Banco
        /// </summary>
        public DateTime? FechaPagoBanco { get; set; }
        /// <summary>
        /// Envio si o no
        /// </summary>
        public bool? Enviado { get; set; }
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
        /// cuota de dolares
        /// </summary>
        public decimal? CuotaDolares { get; set; }
        /// <summary>
        /// Fecha de registro del proceso de pagos
        /// </summary>
        public DateTime? FechaProcesoPago { get; set; }
        /// <summary>
        /// version
        /// </summary>
        public int? Version { get; set; }
        /// <summary>
        /// esta aproabo o no(0,1)
        /// </summary>
        public bool? Aprobado { get; set; }
        /// <summary>
        /// fecha de deposito
        /// </summary>
        public DateTime? FechaDeposito { get; set; }
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
        /// <summary>
        /// Indica la fecha proceso de pago real modificada
        /// </summary>
        public DateTime? FechaProcesoPagoReal { get; set; }
        /// <summary>
        /// Indica la fecha de pago que ingresa a la cuenta bancaria
        /// </summary>
        public DateTime? FechaIngresoEnCuenta { get; set; }
        /// <summary>
        /// Indica la fecha de disponibilidad del efectivo en las cuentas bancarias
        /// </summary>
        public DateTime? FechaEfectivoDisponible { get; set; }
        /// <summary>
        /// Indica la mora del tarifario
        /// </summary>
        public decimal? MoraTarifario { get; set; }
        /// <summary>
        /// Fecha de Compromiso Pago del Alumno 1
        /// </summary>
        public DateTime? FechaCompromiso1 { get; set; }
        /// <summary>
        /// Fecha de Compromiso Pago del Alumno 2
        /// </summary>
        public DateTime? FechaCompromiso2 { get; set; }
        /// <summary>
        /// Fecha de Compromiso Pago del Alumno 3
        /// </summary>
        public DateTime? FechaCompromiso3 { get; set; }
        /// <summary>
        /// Fecha de Generacion del Compromiso 1
        /// </summary>
        public DateTime? FechaGeneracionCompromiso1 { get; set; }
        /// <summary>
        /// Fecha de Generacion del Compromiso 2
        /// </summary>
        public DateTime? FechaGeneracionCompromiso2 { get; set; }
        /// <summary>
        /// Fecha de Generacion del Compromiso 3
        /// </summary>
        public DateTime? FechaGeneracionCompromiso3 { get; set; }
        /// <summary>
        /// Id del coordinador de cobranza asignado, FK de TPersonal
        /// </summary>
        public int? IdPersonalCoordinadorCobranza { get; set; }
        /// <summary>
        /// Nombre de usuario del coordinador academico asignado
        /// </summary>
        public string? UsuarioCoordinadorAcademico { get; set; }
        /// <summary>
        /// Moneda en la que se realiza el pago de la mora, gestion de cobranza
        /// </summary>
        public string? MonedaMoraTarifario { get; set; }
        /// <summary>
        /// Nro de comprobante
        /// </summary>
        public string? NroDocumentoComprobante { get; set; }
        /// <summary>
        /// Razon social
        /// </summary>
        public string? NombreRazonSocial { get; set; }
        /// <summary>
        /// Detalle de la observacion
        /// </summary>
        public string? Observacion { get; set; }
        /// <summary>
        /// Tipo de Comprobante
        /// </summary>
        public int? IdTipoComprobante { get; set; }
        /// <summary>
        /// Estado de la cuota, enviado a Siigo o no (1,0)
        /// </summary>
        public bool? EnviadoSiigo { get; set; }
        /// <summary>
        /// Estado de la cuota, enviado a Facturama o no (1: Enviado, 0: No enviado)
        /// </summary>
        public bool? FacturamaEnvio { get; set; }
    }
}
