using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSdtPoshisItem
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_Sentinel
        /// </summary>
        public int? IdSentinel { get; set; }
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string? TipoDocumento { get; set; }
        /// <summary>
        /// Numero de documento
        /// </summary>
        public string? NumeroDocumento { get; set; }
        /// <summary>
        /// Fecha de proceso
        /// </summary>
        public DateTime? FechaProceso { get; set; }
        /// <summary>
        /// Numero de semana actual
        /// </summary>
        public string? SemanaActual { get; set; }
        /// <summary>
        /// Descripción del semaforo
        /// </summary>
        public string? DescripcionSemaforo { get; set; }
        /// <summary>
        /// Score
        /// </summary>
        public decimal? Score { get; set; }
        /// <summary>
        /// Codigo de variación
        /// </summary>
        public int? CodigoVariacion { get; set; }
        /// <summary>
        /// Descripcion de variación
        /// </summary>
        public string? DescripcionVariacion { get; set; }
        /// <summary>
        /// Numero de entidades
        /// </summary>
        public int? NumeroEntidades { get; set; }
        /// <summary>
        /// Monto de deuda total
        /// </summary>
        public decimal? DeudaTotal { get; set; }
        /// <summary>
        /// Porcentaje de calificacion
        /// </summary>
        public decimal? PorcentajeCalificacion { get; set; }
        /// <summary>
        /// Id de peor calificacion
        /// </summary>
        public int? PeorCalificacion { get; set; }
        /// <summary>
        /// Descripcion de peor calificacion
        /// </summary>
        public string? PeroCalificacionDescripcion { get; set; }
        /// <summary>
        /// Monto segun la SBS
        /// </summary>
        public decimal? MontoSbs { get; set; }
        /// <summary>
        /// progreso de registro
        /// </summary>
        public decimal? ProgresoRegistro { get; set; }
        /// <summary>
        /// Impuesto
        /// </summary>
        public decimal? DocImpuesto { get; set; }
        /// <summary>
        /// Deuda tributaria
        /// </summary>
        public decimal? DeudaTributaria { get; set; }
        /// <summary>
        /// AFP
        /// </summary>
        public decimal? Afp { get; set; }
        /// <summary>
        /// Tarjeta de Credito
        /// </summary>
        public int? TarjetaCredito { get; set; }
        /// <summary>
        /// Cuenta Corriente
        /// </summary>
        public int? CuentaCorriente { get; set; }
        /// <summary>
        /// Reporte Negativo
        /// </summary>
        public int? ReporteNegativo { get; set; }
        /// <summary>
        /// Monto de deuda directa
        /// </summary>
        public decimal? DeudaDirecta { get; set; }
        /// <summary>
        /// Monto de deuda indirecta
        /// </summary>
        public decimal? DeudaIndirecta { get; set; }
        /// <summary>
        /// Linea de credito no utilizada
        /// </summary>
        public decimal? LineaCreditoNoUtilizada { get; set; }
        /// <summary>
        /// Deuda castigada
        /// </summary>
        public decimal? DeudaCastigada { get; set; }
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

        public virtual TSentinel? IdSentinelNavigation { get; set; }
    }
}
