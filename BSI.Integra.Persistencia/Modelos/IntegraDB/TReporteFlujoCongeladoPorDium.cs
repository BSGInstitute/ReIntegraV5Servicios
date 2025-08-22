using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TReporteFlujoCongeladoPorDium
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Estado de la matricula
        /// </summary>
        public string EstadoMatricula { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key TMatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Nombre completo del coodinador academico
        /// </summary>
        public string CoordinadorAcademico { get; set; } = null!;
        /// <summary>
        /// Nombre completo del coodinador de cobranza
        /// </summary>
        public string CoordinadorCobranza { get; set; } = null!;
        /// <summary>
        /// Es Foreing Key TPEspecifico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Columna nombre del centro de costos
        /// </summary>
        public string NombrePrograma { get; set; } = null!;
        /// <summary>
        /// Almacena el codigo unico de la matricula
        /// </summary>
        public string CodigoMatricula { get; set; } = null!;
        /// <summary>
        /// Nombre completo del alumno
        /// </summary>
        public string NombreAlumno { get; set; } = null!;
        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public DateTime FechaVencimiento { get; set; }
        /// <summary>
        /// Monto de la Cuota
        /// </summary>
        public decimal MontoCuota { get; set; }
        /// <summary>
        /// Total Pagado
        /// </summary>
        public decimal TotalPagado { get; set; }
        /// <summary>
        /// Fecha de Pago
        /// </summary>
        public DateTime? FechaPago { get; set; }
        /// <summary>
        /// Fecha del proceso de Pago
        /// </summary>
        public DateTime? FechaProcesoPago { get; set; }
        /// <summary>
        /// Saldo Pendiente
        /// </summary>
        public decimal SaldoPendiente { get; set; }
        /// <summary>
        /// Saldo Pendiente en dolares
        /// </summary>
        public decimal SaldoPendienteDolar { get; set; }
        /// <summary>
        /// Cuota en dolares
        /// </summary>
        public decimal TotalCuotaDolar { get; set; }
        /// <summary>
        /// El pago en dolares
        /// </summary>
        public decimal RealPagoDolar { get; set; }
        /// <summary>
        /// Numero de documento del alumno
        /// </summary>
        public string NroDocumento { get; set; } = null!;
        /// <summary>
        /// Moneda de Pago
        /// </summary>
        public string MonedaPago { get; set; } = null!;
        /// <summary>
        /// Tipo de Cambio
        /// </summary>
        public decimal? TipoCambio { get; set; }
        /// <summary>
        /// Mora
        /// </summary>
        public decimal Mora { get; set; }
        /// <summary>
        /// Numero de cuota
        /// </summary>
        public int NroCuota { get; set; }
        /// <summary>
        /// Numero de subcuota
        /// </summary>
        public int NroSubCuota { get; set; }
        /// <summary>
        /// Numero de version de congelamiento
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// Estado de la cuota, cancelado o no (1,0)
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// Numero de DNI
        /// </summary>
        public string? Dni { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Fecha de congelamiento
        /// </summary>
        public DateTime FechaCongelamiento { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
