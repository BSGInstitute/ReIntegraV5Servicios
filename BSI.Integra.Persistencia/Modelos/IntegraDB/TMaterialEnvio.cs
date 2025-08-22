using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialEnvio
    {
        public TMaterialEnvio()
        {
            TMaterialEnvioDetalles = new HashSet<TMaterialEnvioDetalle>();
        }

        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla gp.T_SedeTrabajo
        /// </summary>
        public int IdSedeTrabajo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla gp.T_Personal
        /// </summary>
        public int IdPersonalRemitente { get; set; }
        /// <summary>
        /// Llave foranea a la tabla fin.T_Proveedor
        /// </summary>
        public int IdProveedorEnvio { get; set; }
        /// <summary>
        /// Almacena la fecha de envio del material
        /// </summary>
        public DateTime FechaEnvio { get; set; }
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
        /// Usuario de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        public int? IdMigracion { get; set; }

        public virtual ICollection<TMaterialEnvioDetalle> TMaterialEnvioDetalles { get; set; }
    }
}
