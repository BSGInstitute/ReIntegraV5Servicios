using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaPagoDetalleOriginal
    {
        /// <summary>
        /// Es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foregin key de la tabla  T_MatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// numero de cuotas
        /// </summary>
        public int NroCuota { get; set; }
        /// <summary>
        /// numero de subcuotas
        /// </summary>
        public int NroSubCuota { get; set; }
        /// <summary>
        /// Fecha de Vencimiento
        /// </summary>
        public DateTime FechaVencimiento { get; set; }
        /// <summary>
        /// Total a pagar
        /// </summary>
        public decimal TotalPagar { get; set; }
        /// <summary>
        /// cuota
        /// </summary>
        public decimal Cuota { get; set; }
        /// <summary>
        /// saldo
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// cancelado
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// monto
        /// </summary>
        public double? Monto { get; set; }
        /// <summary>
        /// tipo de cuota
        /// </summary>
        public string? TipoCuota { get; set; }
        /// <summary>
        /// moneda
        /// </summary>
        public string? Moneda { get; set; }
        /// <summary>
        /// tipo de cambio
        /// </summary>
        public decimal? TipocCambio { get; set; }
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
