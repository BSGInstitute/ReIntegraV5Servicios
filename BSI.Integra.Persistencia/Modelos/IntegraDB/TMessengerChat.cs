using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TMessengerChat
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla TFM_MessengerUsuario
        /// </summary>
        public int? IdMeseengerUsuario { get; set; }
        /// <summary>
        /// Llave foranea a la tabla tPersonal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Mensaje del  chat que se envia o recepciona a Messenger de Facebook
        /// </summary>
        public string? Mensaje { get; set; }
        /// <summary>
        /// Tipo de chat, 1 si es del usuario facebook, 0 si es del asesor
        /// </summary>
        public bool? Tipo { get; set; }
        /// <summary>
        /// Estado ,valida si esta activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima Fecha de modificacion del registro
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
        /// <summary>
        /// Id del Mensaje en Facebook
        /// </summary>
        public string? FacebookId { get; set; }
        /// <summary>
        /// Fecha en que se envio/recibio el mensaje
        /// </summary>
        public DateTime? FechaInteraccion { get; set; }
        /// <summary>
        /// es clave foránea de la tabla TipoMensajeMessenger
        /// </summary>
        public int? IdTipoMensajeMessenger { get; set; }
        /// <summary>
        /// Url del Archivo Adjunto
        /// </summary>
        public string? UrlArchivoAdjunto { get; set; }
        /// <summary>
        /// Indica si el mensaje fue leido por el Usuario de Facebook
        /// </summary>
        public bool? Leido { get; set; }
        /// <summary>
        /// Fecha de lectura del mensaje
        /// </summary>
        public DateTime? FechaLectura { get; set; }
        /// <summary>
        /// Es clave foranea de T_FacebookAnuncio
        /// </summary>
        public int? IdFacebookAnuncio { get; set; }
    }
}
