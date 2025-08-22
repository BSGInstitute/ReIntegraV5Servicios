using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TGastoFinancieroCronograma
    {
        /// <summary>
        /// Llave primaria de la tabla 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Gasto Financiero
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Llave foranea de la tabla T_EntidadFinanciera
        /// </summary>
        public int IdEntidadFinanciera { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_Moneda
        /// </summary>
        public int IdMoneda { get; set; }
        /// <summary>
        /// Capital total del registro
        /// </summary>
        public decimal CapitalTotal { get; set; }
        /// <summary>
        /// Interes total del registro
        /// </summary>
        public decimal InteresTotal { get; set; }
        /// <summary>
        /// Fecha de inicio del registro
        /// </summary>
        public DateTime FechaInicio { get; set; }
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
