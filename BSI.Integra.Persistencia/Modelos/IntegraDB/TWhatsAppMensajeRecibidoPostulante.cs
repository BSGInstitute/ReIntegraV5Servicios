using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajeRecibidoPostulante
    {
        /// <summary>
        /// Pk de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Persona que envia el mensaje
        /// </summary>
        public string? WaFrom { get; set; }
        /// <summary>
        /// Identificador del chat
        /// </summary>
        public string? WaId { get; set; }
        /// <summary>
        /// Fecha de mensaje
        /// </summary>
        public string? WaTimeStamp { get; set; }
        /// <summary>
        /// Tipo del mensaje
        /// </summary>
        public string? WaType { get; set; }
        /// <summary>
        /// Tipo de mensaje
        /// </summary>
        public int? WaTypeMensaje { get; set; }
        /// <summary>
        /// identificador del tipo de mensaje
        /// </summary>
        public string? WaIdTypeMensaje { get; set; }
        /// <summary>
        /// Cuerpo del mensaje
        /// </summary>
        public string? WaBody { get; set; }
        /// <summary>
        /// archvio adjunto del mensaje
        /// </summary>
        public string? WaFile { get; set; }
        /// <summary>
        /// nombre del archivo adjunto
        /// </summary>
        public string? WaFileName { get; set; }
        /// <summary>
        /// tipo de archivo del mensaje
        /// </summary>
        public string? WaMimeType { get; set; }
        /// <summary>
        /// Encriptacion del archivo del mensaje
        /// </summary>
        public string? WaSha256 { get; set; }
        /// <summary>
        /// Descripcion o asunto del mensaje
        /// </summary>
        public string? WaCaption { get; set; }
        /// <summary>
        /// FK T_Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// FK T_Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// FK T_Postulante
        /// </summary>
        public int? IdPostulante { get; set; }
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
    }
}
