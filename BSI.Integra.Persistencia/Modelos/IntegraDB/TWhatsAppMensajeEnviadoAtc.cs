using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajeEnviadoAtc
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabala [T_WhatsAppConfiguracionApi]
        /// </summary>
        public int? IdWhatsAppConfiguracionApi { get; set; }
        /// <summary>
        /// Id del numero de celular vinculado
        /// </summary>
        public string? PhoneNumberId { get; set; }
        /// <summary>
        /// Numero de celular de ingreso
        /// </summary>
        public string? WaTo { get; set; }
        /// <summary>
        /// Id del mensaje
        /// </summary>
        public string? WaId { get; set; }
        /// <summary>
        /// tipo de mensaje
        /// </summary>
        public string? WaType { get; set; }
        /// <summary>
        /// tipo del mensaje de whatssap
        /// </summary>
        public string? WaTypeMensaje { get; set; }
        /// <summary>
        /// cuerpo del mensaje
        /// </summary>
        public string? WaBody { get; set; }
        /// <summary>
        /// ruta del archivo
        /// </summary>
        public string? WaFile { get; set; }
        /// <summary>
        /// tipo del arcihvo
        /// </summary>
        public string? WaMimeType { get; set; }
        /// <summary>
        /// Cifrado del WaTypeMensaje
        /// </summary>
        public string? WaSha256 { get; set; }
        /// <summary>
        /// nombre del archivo
        /// </summary>
        public string? WaFileName { get; set; }
        /// <summary>
        /// pie de pagina
        /// </summary>
        public string? WaCaption { get; set; }
        /// <summary>
        /// id del pais del mensaje
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// id del persoanal al que se le asigna el mensaje
        /// </summary>
        public int? IdPersonal { get; set; }
        /// <summary>
        /// id del alumno que envio el mensaje
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// define si el contenido es migrado
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
