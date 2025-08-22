using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajeRecibido
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Campo WaFrom
        /// </summary>
        public string? WaFrom { get; set; }
        /// <summary>
        /// Campo WaId
        /// </summary>
        public string? WaId { get; set; }
        /// <summary>
        /// Campo WaTimeStamp
        /// </summary>
        public string? WaTimeStamp { get; set; }
        /// <summary>
        /// Campo WaType
        /// </summary>
        public string? WaType { get; set; }
        /// <summary>
        /// Campo WaTypeMensaje
        /// </summary>
        public int? WaTypeMensaje { get; set; }
        /// <summary>
        /// Campo WaIdTypeMensaje
        /// </summary>
        public string? WaIdTypeMensaje { get; set; }
        /// <summary>
        /// Campo WaBody
        /// </summary>
        public string? WaBody { get; set; }
        /// <summary>
        /// Campo WaFile
        /// </summary>
        public string? WaFile { get; set; }
        /// <summary>
        /// Campo WaFileName
        /// </summary>
        public string? WaFileName { get; set; }
        /// <summary>
        /// Campo WaMimeType
        /// </summary>
        public string? WaMimeType { get; set; }
        /// <summary>
        /// Campo WaSha256
        /// </summary>
        public string? WaSha256 { get; set; }
        /// <summary>
        /// Campo WaCaption
        /// </summary>
        public string? WaCaption { get; set; }
        /// <summary>
        /// Llave foranea de Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Llave foranea del Personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave foranea del Alumno
        /// </summary>
        public int? IdAlumno { get; set; }
        /// <summary>
        /// Campo true o false de migracion
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
        /// Validación de mensaje ofensivo
        /// </summary>
        public bool? MensajeOfensivo { get; set; }

        public virtual TPai IdPaisNavigation { get; set; } = null!;
    }
}
