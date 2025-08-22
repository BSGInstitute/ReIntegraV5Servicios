using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TProducto
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del producto
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Descripcion del producto
        /// </summary>
        public string Descripcion { get; set; } = null!;
        public string CuentaGeneral { get; set; } = null!;
        public string CuentaGeneralCodigo { get; set; } = null!;
        public string CuentaEspecifica { get; set; } = null!;
        public string CuentaEspecificaCodigo { get; set; } = null!;
        /// <summary>
        /// Llave Foranea con la tabla T_ProductoPresentacion
        /// </summary>
        public int IdProductoPresentacion { get; set; }
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
