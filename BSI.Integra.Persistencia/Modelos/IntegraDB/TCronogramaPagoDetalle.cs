using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPagoDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreing key tCronogramaPagos
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Numero de la cuota
        /// </summary>
        public int? NroCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento de la cuota
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }
        /// <summary>
        /// Monto total a pagar 
        /// </summary>
        public decimal? TotalPagar { get; set; }
        /// <summary>
        /// Monto de la cuota a pagar
        /// </summary>
        public decimal? Cuota { get; set; }
        /// <summary>
        /// Saldo restante por pagar del total de la deuda 
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
        /// Fecha de creacion del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de modificacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Usuario de creacion del registro
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
        public int? IdMigracion { get; set; }
    }
}
