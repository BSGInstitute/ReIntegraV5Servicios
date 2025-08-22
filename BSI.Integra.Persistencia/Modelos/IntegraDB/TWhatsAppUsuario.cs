using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppUsuario
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabla T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Rol de los usuarios no depende de ninguna tabla
        /// </summary>
        public string? RolUser { get; set; }
        /// <summary>
        /// Nombre del usuario que se le envia a WhatsApp
        /// </summary>
        public string? UserUsername { get; set; }
        /// <summary>
        /// Clave del usuario que se le envia a WhatsApp
        /// </summary>
        public string? UserPassword { get; set; }
        public bool? EsMigracion { get; set; }
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

        public virtual TPersonal? IdPersonalNavigation { get; set; }
    }
}
