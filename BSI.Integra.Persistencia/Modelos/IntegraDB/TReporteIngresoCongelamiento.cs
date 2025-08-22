using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TReporteIngresoCongelamiento
    {
        /// <summary>
        /// Id de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Filtro usado para generar el reporte
        /// </summary>
        public string? NombreFiltro { get; set; }
        /// <summary>
        /// Detalle del reporte
        /// </summary>
        public string? DetalleCongelado { get; set; }
        /// <summary>
        /// Fecha de congelamiento 
        /// </summary>
        public DateTime FechaCongelamiento { get; set; }
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
        /// <summary>
        /// Marca de tiempo
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
