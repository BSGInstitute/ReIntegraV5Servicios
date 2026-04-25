using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    /// <summary>
    /// Tabla relacional que vincula gestión de contacto con flujos de gestión docente
    /// </summary>
    public partial class TGestionContactoDocenteFlujo
    {
        public TGestionContactoDocenteFlujo()
        {
            TGestionContactoFlujoCongelados = new HashSet<TGestionContactoFlujoCongelado>();
        }

        /// <summary>
        /// Identificador único de la relación
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionContacto
        /// </summary>
        public int IdGestionContacto { get; set; }
        /// <summary>
        /// Llave foránea a la tabla T_GestionDocenteFlujo
        /// </summary>
        public int IdGestionDocenteFlujo { get; set; }
        /// <summary>
        /// Estado del registro (1=Activo, 0=Inactivo)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creó el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Usuario que modificó el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creación del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificación del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Versión de fila para control de concurrencia
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;

        public virtual TGestionContacto IdGestionContactoNavigation { get; set; } = null!;
        public virtual TGestionDocenteFlujo IdGestionDocenteFlujoNavigation { get; set; } = null!;
        public virtual ICollection<TGestionContactoFlujoCongelado> TGestionContactoFlujoCongelados { get; set; }
    }
}
