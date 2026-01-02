using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Catálogo de subestados de cabecera de gestión de contacto
    /// </summary>
    public partial class TSubEstadoCabeceraGestionContacto
    {
        public TSubEstadoCabeceraGestionContacto()
        {
            TCabeceraGestionContactos = new HashSet<TCabeceraGestionContacto>();
        }

        /// <summary>
        /// Identificador único del subestado (Llave primaria)
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del subestado
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Foreign Key que referencia al estado de cabecera
        /// </summary>
        public int IdEstadoCabeceraGestionContacto { get; set; }
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

        public virtual TEstadoCabeceraGestionContacto IdEstadoCabeceraGestionContactoNavigation { get; set; } = null!;
        public virtual ICollection<TCabeceraGestionContacto> TCabeceraGestionContactos { get; set; }
    }
}
