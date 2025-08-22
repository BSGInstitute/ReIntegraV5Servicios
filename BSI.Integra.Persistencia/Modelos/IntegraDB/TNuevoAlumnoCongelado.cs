using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TNuevoAlumnoCongelado
    {
        /// <summary>
        /// Es primary key 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Es foreign key de fin.T_MatriculaCabecera
        /// </summary>
        public int IdMatriculaCabecera { get; set; }
        /// <summary>
        /// Numero de cuotas
        /// </summary>
        public int NroCuota { get; set; }
        /// <summary>
        /// Nro de SubCuotas
        /// </summary>
        public int NroSubCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento
        /// </summary>
        public DateTime FechaVencimiento { get; set; }
        /// <summary>
        /// Total a pagar
        /// </summary>
        public decimal? TotalPagar { get; set; }
        /// <summary>
        /// Cuota del alumno
        /// </summary>
        public decimal Cuota { get; set; }
        /// <summary>
        /// Saldo del alumno
        /// </summary>
        public decimal Saldo { get; set; }
        /// <summary>
        /// Mora del alumno
        /// </summary>
        public decimal Mora { get; set; }
        /// <summary>
        /// Monto Pagado
        /// </summary>
        public decimal MontoPagado { get; set; }
        /// <summary>
        /// Indica si esta cancelado 
        /// </summary>
        public bool Cancelado { get; set; }
        /// <summary>
        /// Tipo de cuota que tiene el alumno
        /// </summary>
        public string TipoCuota { get; set; } = null!;
        /// <summary>
        /// Moneda en la que paga el alumno
        /// </summary>
        public string Moneda { get; set; } = null!;
        /// <summary>
        /// Fecha de pago del alumno
        /// </summary>
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Fecha en la que se congela la informacion
        /// </summary>
        public DateTime FechaCongelamiento { get; set; }
        /// <summary>
        /// Es foreign key de mkt.T_Periodo
        /// </summary>
        public int IdPeriodo { get; set; }
        /// <summary>
        /// Para saber si el registro fue eliminado de forma logica
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
        public int? IdMigracion { get; set; }
    }
}
