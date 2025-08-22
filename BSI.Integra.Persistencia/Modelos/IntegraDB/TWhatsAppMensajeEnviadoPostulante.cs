using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajeEnviadoPostulante
    {
        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Numero destinatario
        /// </summary>
        public string? WaTo { get; set; }
        /// <summary>
        /// Identidicador del chat
        /// </summary>
        public string? WaId { get; set; }
        /// <summary>
        /// tipo de mensaje
        /// </summary>
        public string? WaType { get; set; }
        /// <summary>
        /// tipo de mensaje
        /// </summary>
        public int? WaTypeMensaje { get; set; }
        /// <summary>
        /// Verifica si el mensaje es individual o plantilla
        /// </summary>
        public string? WaRecipientType { get; set; }
        /// <summary>
        /// cuerpo del mensaje
        /// </summary>
        public string? WaBody { get; set; }
        /// <summary>
        /// archivos adjuntos
        /// </summary>
        public string? WaFile { get; set; }
        /// <summary>
        /// Nombre archivo adjunto
        /// </summary>
        public string? WaFileName { get; set; }
        /// <summary>
        /// Identificador de tipo de dato
        /// </summary>
        public string? WaMimeType { get; set; }
        /// <summary>
        /// String de encriptacion de archivos adjuntos
        /// </summary>
        public string? WaSha256 { get; set; }
        /// <summary>
        /// Enlaces de mensaje de whatsapp
        /// </summary>
        public string? WaLink { get; set; }
        /// <summary>
        /// Descripcion o asunto del mensaje
        /// </summary>
        public string? WaCaption { get; set; }
        /// <summary>
        /// Fk T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// True si es migracion, false si no es migracion
        /// </summary>
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
        /// <summary>
        /// Fk T_Personal
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// Fk T_Postulante
        /// </summary>
        public int? IdPostulante { get; set; }
    }
}
