using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSentinelSdtEstandarItem
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es Foreing Key T_SENTINEL
        /// </summary>
        public int? IdSentinel { get; set; }
        /// <summary>
        /// Tipo de documento
        /// </summary>
        public string? TipoDocumento { get; set; }
        /// <summary>
        /// Numero de documento
        /// </summary>
        public string? Documento { get; set; }
        /// <summary>
        /// Razon Social 
        /// </summary>
        public string? RazonSocial { get; set; }
        /// <summary>
        /// Fecha de proceso
        /// </summary>
        public DateTime? FechaProceso { get; set; }
        /// <summary>
        /// Semaforos
        /// </summary>
        public string? Semaforos { get; set; }
        /// <summary>
        /// Puntuacion
        /// </summary>
        public string? Score { get; set; }
        /// <summary>
        /// Numero de bancos
        /// </summary>
        public string? NroBancos { get; set; }
        /// <summary>
        /// Deuda total
        /// </summary>
        public string? DeudaTotal { get; set; }
        /// <summary>
        /// Monto vencido banco
        /// </summary>
        public string? VencidoBanco { get; set; }
        /// <summary>
        /// Calificacion
        /// </summary>
        public string? Calificativo { get; set; }
        /// <summary>
        /// veces
        /// </summary>
        public string? Veces24m { get; set; }
        /// <summary>
        /// Numero de semana actual
        /// </summary>
        public string? SemanaActual { get; set; }
        /// <summary>
        /// Numero de semana previa
        /// </summary>
        public string? SemanaPrevio { get; set; }
        /// <summary>
        /// Numero de semana mejor
        /// </summary>
        public string? SemanaPeorMejor { get; set; }
        /// <summary>
        /// Numero de documento alternativo
        /// </summary>
        public string? Documento2 { get; set; }
        /// <summary>
        /// Estado de domicilio
        /// </summary>
        public string? EstadoDomicilio { get; set; }
        /// <summary>
        /// Condicion de domicilio
        /// </summary>
        public string? CondicionDomicilio { get; set; }
        /// <summary>
        /// Monto de deuda tributaria
        /// </summary>
        public string? DeudaTributaria { get; set; }
        /// <summary>
        /// Monto de deuda laboral
        /// </summary>
        public string? DeudaLaboral { get; set; }
        /// <summary>
        /// Monto de deuda impagable
        /// </summary>
        public string? DeudaImpagable { get; set; }
        /// <summary>
        /// Monto de deuda protestos
        /// </summary>
        public string? DeudaProtestos { get; set; }
        /// <summary>
        /// Monto de deuda segun SBS
        /// </summary>
        public string? DeudaSbs { get; set; }
        /// <summary>
        /// Numero de cuentas de tarjetas
        /// </summary>
        public string? CuentasTarjetas { get; set; }
        /// <summary>
        /// Reporte Negativo
        /// </summary>
        public string? ReporteNegativo { get; set; }
        /// <summary>
        /// Tipo de actividad
        /// </summary>
        public string? TipoActividad { get; set; }
        /// <summary>
        /// Fecha de inicio de la actividad
        /// </summary>
        public DateTime? FechaInicioActividad { get; set; }
        /// <summary>
        /// Monto de deuda directa
        /// </summary>
        public string? DeudaDirecta { get; set; }
        /// <summary>
        /// Monto de deuda indirecta
        /// </summary>
        public string? DeudaIndirecta { get; set; }
        /// <summary>
        /// Monto de deuda castigada
        /// </summary>
        public string? DeudaCastigada { get; set; }
        /// <summary>
        /// Linea de credito no utilizada
        /// </summary>
        public string? LineaCreditoNoUtilizada { get; set; }
        /// <summary>
        /// Total del riesgo
        /// </summary>
        public string? TotalRiesgo { get; set; }
        /// <summary>
        /// Peor calificación
        /// </summary>
        public string? PeorCalificacion { get; set; }
        /// <summary>
        /// Porcentaje de calificación normal
        /// </summary>
        public string? PorcentajeCalificacionNormal { get; set; }
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
