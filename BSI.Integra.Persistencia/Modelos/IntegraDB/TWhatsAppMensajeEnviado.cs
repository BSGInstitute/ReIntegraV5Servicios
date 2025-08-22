using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajeEnviado
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public string? WaTo { get; set; }
        public string? WaId { get; set; }
        public string? WaType { get; set; }
        public int? WaTypeMensaje { get; set; }
        public string? WaRecipientType { get; set; }
        public string? WaBody { get; set; }
        public string? WaFile { get; set; }
        public string? WaFileName { get; set; }
        public string? WaMimeType { get; set; }
        public string? WaSha256 { get; set; }
        public string? WaLink { get; set; }
        public string? WaCaption { get; set; }
        public int IdPais { get; set; }
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
        public int? IdPersonal { get; set; }
        public int? IdAlumno { get; set; }
    }
}
