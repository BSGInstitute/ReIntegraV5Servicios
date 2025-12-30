using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla cabecera de gestión de contacto
    /// </summary>
    public partial class TCabeceraGestionContacto
    {
        /// <summary>
        /// Identificador único del registro (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Foreign Key que referencia al proveedor
        /// </summary>
        public int? IdProveedor { get; set; }
        /// <summary>
        /// Foreign Key que referencia al programa específico
        /// </summary>
        public int IdPespecifico { get; set; }
        /// <summary>
        /// Foreign Key que referencia al subestado de cabecera
        /// </summary>
        public int IdSubEstadoCabeceraGestionContacto { get; set; }
        /// <summary>
        /// Estado del registro (1: Activo, 0: Eliminado/Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que realizó la última modificación del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha y hora de la última modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automático que guarda la versión del registro para control de concurrencia optimista
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TPespecifico IdPespecificoNavigation { get; set; } = null!;
        public virtual TProveedor? IdProveedorNavigation { get; set; }
        public virtual TSubEstadoCabeceraGestionContacto IdSubEstadoCabeceraGestionContactoNavigation { get; set; } = null!;
    }
}
