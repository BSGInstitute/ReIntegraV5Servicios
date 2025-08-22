using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOtroMovimientoCaja
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_SubTipoMovimientoCaja
        /// </summary>
        public int IdSubTipoMovimientoCaja { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Precio
        /// </summary>
        public decimal Precio { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Fecha de Pago
        /// </summary>
        public DateTime FechaPago { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CentroCosto
        /// </summary>
        public int? IdCentroCosto { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_PlanContable
        /// </summary>
        public int? IdPlanContable { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_CuentaCorriente
        /// </summary>
        public int IdCuentaCorriente { get; set; }
        /// <summary>
        /// Llave foranea con la tabla T_FormaPago
        /// </summary>
        public int? IdFormaPago { get; set; }
        /// <summary>
        /// Observaciones
        /// </summary>
        public string? Observaciones { get; set; }
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
        public Guid? IdMigracion { get; set; }
    }
}
