using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TReporteTipoDeCambioFinancieroMensual
    {
        /// <summary>
        /// Id del Tipo de cambio financiero mensual para reportes
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Tipo de cambio de la moneda seleccionada a dolares
        /// </summary>
        public string MonedaAdolar { get; set; } = null!;
        /// <summary>
        /// Tipo de cambio de dolares actual a la moneda seleccionada
        /// </summary>
        public string DolarAmoneda { get; set; } = null!;
        /// <summary>
        /// Mes en el que se guarda el tipo de cambio
        /// </summary>
        public int Mes { get; set; }
        /// <summary>
        /// Año en el que se guarda el tipo de cambio
        /// </summary>
        public int Anio { get; set; }
        /// <summary>
        /// Id del tipo de moneda
        /// </summary>
        public int IdMoneda { get; set; }
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
