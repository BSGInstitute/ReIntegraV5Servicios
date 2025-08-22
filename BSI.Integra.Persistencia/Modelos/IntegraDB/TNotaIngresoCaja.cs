using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TNotaIngresoCaja
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de T_Caja
        /// </summary>
        public int IdCaja { get; set; }
        /// <summary>
        /// Codigo del NIC
        /// </summary>
        public string CodigoNic { get; set; } = null!;
        /// <summary>
        /// Llave foranea de T_OrigenIngresoCaja
        /// </summary>
        public int IdOrigenIngresoCaja { get; set; }
        /// <summary>
        /// Llave foranea de T_Personal, Nombre de quien emite el registro
        /// </summary>
        public int IdPersonalEmitido { get; set; }
        /// <summary>
        /// Monto de emision
        /// </summary>
        public decimal Monto { get; set; }
        /// <summary>
        /// Fecha en la que se hace el giro
        /// </summary>
        public DateTime FechaGiro { get; set; }
        /// <summary>
        /// Descripcion del concepto por el cual se realiza el giro
        /// </summary>
        public string Concepto { get; set; } = null!;
        /// <summary>
        /// Fecha de cobro
        /// </summary>
        public DateTime FechaCobro { get; set; }
        /// <summary>
        /// El anho en que se realiza
        /// </summary>
        public int Anho { get; set; }
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
        public int? IdMigracion { get; set; }
    }
}
