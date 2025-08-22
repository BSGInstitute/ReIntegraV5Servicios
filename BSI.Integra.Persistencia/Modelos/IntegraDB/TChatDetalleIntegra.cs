using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TChatDetalleIntegra
    {
        /// <summary>
        /// Clave primaria del registro
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id de la tabla TCRM_InteraccionChatIntegra
        /// </summary>
        public int? IdInteraccionChatIntegra { get; set; }
        /// <summary>
        /// Nombre de quien envia el mensaje
        /// </summary>
        public string? NombreRemitente { get; set; }
        /// <summary>
        /// Id de quien en el mensaje (talumnos, tpersonal)
        /// </summary>
        public string IdRemitente { get; set; } = null!;
        /// <summary>
        /// Mensaje
        /// </summary>
        public string? Mensaje { get; set; }
        /// <summary>
        /// Fecha de envio del mensaje
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Estado del registro activo o no
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario de creacion del registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario de modificacion del registro
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Fecha de creacion del registro
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Ultima fecha de modificacion del registro
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
        /// Validación de Mensaje Ofensivo
        /// </summary>
        public bool? MensajeOfensivo { get; set; }
        /// <summary>
        /// FK de T_ChatDetalleIntegraArchivo
        /// </summary>
        public int? IdChatDetalleIntegraArchivo { get; set; }
    }
}
