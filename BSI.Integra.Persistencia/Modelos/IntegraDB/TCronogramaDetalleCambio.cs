using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCronogramaDetalleCambio
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// llave forane con la tabla T_MatriculaCabecera
        /// </summary>
        public int? IdMatriculaCabecera { get; set; }
        /// <summary>
        /// llave forane con la tabla T_CronogramaCabeceraCambio
        /// </summary>
        public int IdCronogramaCabeceraCambio { get; set; }
        /// <summary>
        /// Numero de cuota
        /// </summary>
        public int NroCuota { get; set; }
        /// <summary>
        /// Numero de subcuota
        /// </summary>
        public int NroSubcuota { get; set; }
        /// <summary>
        /// Fecha de Vencimiento
        /// </summary>
        public DateTime FechaVencimiento { get; set; }
        /// <summary>
        /// Monto de la cuota
        /// </summary>
        public decimal Cuota { get; set; }
        /// <summary>
        /// Monto de la Mora
        /// </summary>
        public decimal Mora { get; set; }
        /// <summary>
        /// Tipo de Cambio
        /// </summary>
        public decimal TipoCambio { get; set; }
        /// <summary>
        /// Moneda
        /// </summary>
        public string Moneda { get; set; } = null!;
        /// <summary>
        /// Version del Cronograma
        /// </summary>
        public int Version { get; set; }
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
        /// Sistema Automatico Fecha de Modificacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Relacion con el id de la tabla original
        /// </summary>
        public Guid? IdMigracion { get; set; }
    }
}
