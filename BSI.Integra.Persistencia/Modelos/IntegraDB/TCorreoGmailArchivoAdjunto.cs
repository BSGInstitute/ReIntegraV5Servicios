using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCorreoGmailArchivoAdjunto
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_CorreoGmail
        /// </summary>
        public int IdCorreoGmail { get; set; }
        /// <summary>
        /// Nombre del archivo adjunto
        /// </summary>
        public string Nombre { get; set; } = null!;
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
        /// Id original de la tabla en integraV3
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Almacena la url del archivo en el repositorio web
        /// </summary>
        public string? UrlArchivoRepositorio { get; set; }

        public virtual TCorreoGmail IdCorreoGmailNavigation { get; set; } = null!;
    }
}
