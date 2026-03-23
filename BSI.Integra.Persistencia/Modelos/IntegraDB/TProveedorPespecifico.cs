using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Esta tabla almacena la relacion entre proveedores y programas especificos.
    /// </summary>
    public partial class TProveedorPespecifico
    {
        /// <summary>
        /// Identificador unico del registro.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key con la tabla fin.T_Proveedor.
        /// </summary>
        public int IdProveedor { get; set; }
        /// <summary>
        /// Foreign Key con la tabla pla.T_PEspecifico.
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Estado del registro.
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro.
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario de modificacion del registro.
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro.
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro.
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema que almacena automaticamente la version del registro.
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPespecifico IdPespecificoNavigation { get; set; } = null!;
        public virtual TProveedor IdProveedorNavigation { get; set; } = null!;
    }
}
