using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPago
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es primay key (codigo de matricula del alumno)
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Es foreing key tAlumnos
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Es foreing key tPEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Periodo de pago
        /// </summary>
        public string? Periodo { get; set; }
        /// <summary>
        /// Tipo de moneda de pago (soles, dolares, etc)
        /// </summary>
        public string? Moneda { get; set; }
        /// <summary>
        /// Tipo de acuerdo de pago
        /// </summary>
        public string? AcuerdoPago { get; set; }
        /// <summary>
        /// Tipo de cambio
        /// </summary>
        public double? TipoCambio { get; set; }
        /// <summary>
        /// Monto total a pagar
        /// </summary>
        public double? TotalPagar { get; set; }
        /// <summary>
        /// Numero de cuotas 
        /// </summary>
        public int? NroCuotas { get; set; }
        /// <summary>
        /// Fecha de inicio de pago 
        /// </summary>
        public DateTime? FechaIniPago { get; set; }
        /// <summary>
        /// Estado del envio del cronograma al alumno (1, 0)
        /// </summary>
        public bool? Enviado { get; set; }
        /// <summary>
        /// Descripcion de la observaciones
        /// </summary>
        public string? Observaciones { get; set; }
        /// <summary>
        /// Indica si tiene o no cuota inicial
        /// </summary>
        public bool? ConCuotaInicial { get; set; }
        /// <summary>
        /// Monto de la cuota inicial
        /// </summary>
        public decimal? CuotaInicial { get; set; }
        /// <summary>
        /// Indica si aplica o no la columna NDias
        /// </summary>
        public bool? CadaNdias { get; set; }
        /// <summary>
        /// Numero de dias para hacer el pago
        /// </summary>
        public int? Ndias { get; set; }
        /// <summary>
        /// Tipo de moneda de pago (soles, dolares, etc)
        /// </summary>
        public string? WebMoneda { get; set; }
        /// <summary>
        /// Tipo de cambio
        /// </summary>
        public double? WebTipoCambio { get; set; }
        /// <summary>
        /// Monto total a pagar
        /// </summary>
        public double? WebTotalPagar { get; set; }
        /// <summary>
        /// Monto total a pagar redondeado
        /// </summary>
        public double? WebTotalPagarConv { get; set; }
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
        public string? IdMigracion { get; set; }
    }
}
