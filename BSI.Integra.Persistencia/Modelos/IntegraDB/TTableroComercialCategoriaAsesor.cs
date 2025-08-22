using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TTableroComercialCategoriaAsesor
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        ///  Nombre de la categoria del asesor
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Monto de ventas
        /// </summary>
        public decimal MontoVenta { get; set; }
        /// <summary>
        /// Foreign Key de TMoneda para el monto de venta
        /// </summary>
        public int IdMonedaVenta { get; set; }
        /// <summary>
        /// Foreign key de TTableroComercialUnidad para el monto de venta
        /// </summary>
        public int IdTableroComercialUnidadVenta { get; set; }
        /// <summary>
        /// Monto de premio
        /// </summary>
        public decimal MontoPremio { get; set; }
        /// <summary>
        /// Foreign key de TMoneda para premio
        /// </summary>
        public int IdMonedaPremio { get; set; }
        /// <summary>
        /// Indica si el premio se mostrara en moneda local o no
        /// </summary>
        public bool VisualizarMonedaLocal { get; set; }
        /// <summary>
        /// Estado del registro
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
        /// RowVersion del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion
        /// </summary>
        public int? IdMigracion { get; set; }
    }
}
