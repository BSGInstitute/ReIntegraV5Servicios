using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TOrigenDatoCalidad
    {
        /// <summary>
        /// Clave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del proveedor de origenes a integra
        /// </summary>
        public int IdProveedorCampaniaIntegra { get; set; }
        /// <summary>
        /// Id de las categoria origen asociadas al proveedor
        /// </summary>
        public int IdCategoriaOrigen { get; set; }
        /// <summary>
        /// orden en el que los sectores se van a organizar
        /// </summary>
        public int IdOrigenSector { get; set; }
        /// <summary>
        /// Se va agrupar los proveedores
        /// </summary>
        public bool Agrupar { get; set; }
        /// <summary>
        /// Se va agrupar La categoria origen de los proveedores
        /// </summary>
        public bool AgruparCategoriaOrigen { get; set; }
        /// <summary>
        /// estado del sector
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// usuario creacion del sector
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// usuario modificacion del sector
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// fecha creacion del sector
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// fecha modificacion del sector
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Row version
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
    }
}
