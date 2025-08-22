using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPeriodo
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del periodo
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha de inicio
        /// </summary>
        public DateTime FechaInicial { get; set; }
        /// <summary>
        /// Fecha de fin
        /// </summary>
        public DateTime FechaFin { get; set; }
        /// <summary>
        /// Fecha inicial para finanzas
        /// </summary>
        public DateTime FechaInicialFinanzas { get; set; }
        /// <summary>
        /// Fecha de fin para finanzas
        /// </summary>
        public DateTime FechaFinFinanzas { get; set; }
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
        /// <summary>
        /// Fecha de inicio para el reporte de ingresos
        /// </summary>
        public DateTime? FechaInicialRepIngresos { get; set; }
        /// <summary>
        /// Fecha de fin para el reporte de ingresos
        /// </summary>
        public DateTime? FechaFinRepIngresos { get; set; }
    }
}
