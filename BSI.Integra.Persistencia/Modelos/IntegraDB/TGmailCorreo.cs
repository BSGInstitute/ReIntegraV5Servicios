using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TGmailCorreo
    {
        public TGmailCorreo()
        {
            TGmailCorreoArchivoAdjuntos = new HashSet<TGmailCorreoArchivoAdjunto>();
        }

        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        public int? IdEtiqueta { get; set; }
        public int? IdGmailCliente { get; set; }
        public string? IdCorreoGmailFormat { get; set; }
        public string? Asunto { get; set; }
        public DateTime? Fecha { get; set; }
        public string? EmailBody { get; set; }
        public bool? Seen { get; set; }
        public string? Remitente { get; set; }
        public string? Destinatarios { get; set; }
        public int? IdPersonal { get; set; }
        public int? Filas { get; set; }
        public int? IdInteraccion { get; set; }
        public string? Cc { get; set; }
        public string? ResumenMensaje { get; set; }
        public string? Bcc { get; set; }
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
        /// <summary>
        /// Llave foranea a la tabla conf.T_ClasificacionPersona, almacena a quien se le envia el mensaje
        /// </summary>
        public int? IdClasificacionPersona { get; set; }

        public virtual ICollection<TGmailCorreoArchivoAdjunto> TGmailCorreoArchivoAdjuntos { get; set; }
    }
}
