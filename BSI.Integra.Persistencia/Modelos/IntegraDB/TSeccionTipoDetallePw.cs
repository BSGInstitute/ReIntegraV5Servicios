using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSeccionTipoDetallePw
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea de la tabla Seccion_PW
        /// </summary>
        public int IdSeccionPw { get; set; }
        /// <summary>
        /// Indica el nombre del campo del registro
        /// </summary>
        public string NombreTitulo { get; set; } = null!;
        /// <summary>
        /// Indica estado eliminado o creado
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Indica el usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Indica el usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Indica la fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Indica la fecha de modificacion del registro
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Indica la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Indica el id original de la tabla migrada
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea de la tabla T_SeccionTipoContenido_PW
        /// </summary>
        public int? IdSeccionTipoContenido { get; set; }

        public virtual TSeccionPw IdSeccionPwNavigation { get; set; } = null!;
    }
}
