using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMaterialEnvioDetalle
    {
        /// <summary>
        /// Es primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialEnvio
        /// </summary>
        public int IdMaterialEnvio { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialVersion
        /// </summary>
        public int IdMaterialVersion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla ope.T_MaterialEstadoRecepcion
        /// </summary>
        public int IdMaterialEstadoRecepcion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla gp.T_Personal
        /// </summary>
        public int IdPersonalReceptor { get; set; }
        /// <summary>
        /// Indica la cantidad de materiales enviados
        /// </summary>
        public int CantidadEnvio { get; set; }
        /// <summary>
        /// Indica la cantidad de materiales recepcionados
        /// </summary>
        public int CantidadRecepcion { get; set; }
        /// <summary>
        /// Indica el comentario de envio de material
        /// </summary>
        public string ComentarioEnvio { get; set; } = null!;
        /// <summary>
        /// Indica el comentario de recepcion de material
        /// </summary>
        public string ComentarioRecepcion { get; set; } = null!;
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

        public virtual TMaterialEnvio IdMaterialEnvioNavigation { get; set; } = null!;
    }
}
