using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TGmailCliente
    {
        /// <summary>
        /// es Primary Key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// foreign key de la tabla tpersonal
        /// </summary>
        public int? IdAsesor { get; set; }
        /// <summary>
        /// email del asesor
        /// </summary>
        public string EmailAsesor { get; set; } = null!;
        /// <summary>
        /// password
        /// </summary>
        public string PasswordCorreo { get; set; } = null!;
        /// <summary>
        /// nombre del asesor
        /// </summary>
        public string NombreAsesor { get; set; } = null!;
        public string IdClient { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        /// <summary>
        /// alias del email asesor
        /// </summary>
        public string? AliasEmailAsesor { get; set; }
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
        public Guid? IdMigracion { get; set; }
    }
}
