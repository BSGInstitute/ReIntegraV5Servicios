using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaOriginalesCongeladoPorDium
    {
        /// <summary>
        /// Id de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Estado de la matricula, matriculado, pormatricular, etc
        /// </summary>
        public string? EstadoMatricula { get; set; }
        /// <summary>
        /// Nombre del alumno
        /// </summary>
        public string? Alumno { get; set; }
        /// <summary>
        /// Id matricula cabecera, no se vincula porque se desea mantener asi los eliminen
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Monto de la cuota
        /// </summary>
        public double? Cuota { get; set; }
        /// <summary>
        /// Nro de cuota
        /// </summary>
        public int? NroCuota { get; set; }
        /// <summary>
        /// Nro Sub Cuota
        /// </summary>
        public int? NroSubCuota { get; set; }
        /// <summary>
        /// Moneda en la que esta el cronograma
        /// </summary>
        public string? Moneda { get; set; }
        /// <summary>
        /// Monto de cuota en dolares
        /// </summary>
        public double? CuotaDolares { get; set; }
        /// <summary>
        /// Codigo de la matricula 
        /// </summary>
        public string? CodigoMatricula { get; set; }
        /// <summary>
        /// Periodo de vencimiento de la cuota
        /// </summary>
        public string? PeriodoPorFechaVencimiento { get; set; }
        /// <summary>
        /// Coordinadora academica asignada
        /// </summary>
        public string? CoordinadoraAcademica { get; set; }
        /// <summary>
        /// Coordinadora de cobranza asignada
        /// </summary>
        public string? CoordinadoraCobranza { get; set; }
        /// <summary>
        /// Fecha vencimiento de la cuota
        /// </summary>
        public DateTime? FechaVencimiento { get; set; }
        /// <summary>
        /// Fecha de congelamiento 
        /// </summary>
        public DateTime? FechaCongelamiento { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        public bool? Estado { get; set; }
        /// <summary>
        /// Fecha de creacion
        /// </summary>
        public DateTime? FechaCreacion { get; set; }
        /// <summary>
        /// Fecha modificacion
        /// </summary>
        public DateTime? FechaModificacion { get; set; }
        /// <summary>
        /// Usuario creacion
        /// </summary>
        public string? UsuarioCreacion { get; set; }
        /// <summary>
        /// Usuario modificacion
        /// </summary>
        public string? UsuarioModificacion { get; set; }
        public int? IdMigracion { get; set; }
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Monto pagado por el alumno
        /// </summary>
        public double? MontoPagado { get; set; }
    }
}
