using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TGastoFinancieroCronogramaDetalle
    {
        /// <summary>
        /// Llave primaria de la tabla 
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_GastoFinancieroCronograma
        /// </summary>
        public int IdGastoFinancieroCronograma { get; set; }
        /// <summary>
        /// Numero de la cuota del registro
        /// </summary>
        public int NumeroCuota { get; set; }
        /// <summary>
        /// Cuota capital del registro
        /// </summary>
        public decimal CapitalCuota { get; set; }
        /// <summary>
        /// Cuota interes del registro
        /// </summary>
        public decimal InteresCuota { get; set; }
        /// <summary>
        /// Fecha de vencimiento de la cuota del registro
        /// </summary>
        public DateTime FechaVencimientoCuota { get; set; }
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
