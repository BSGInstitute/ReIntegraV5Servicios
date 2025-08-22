using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCorreoGmail
    {
        public TCorreoGmail()
        {
            TCorreoGmailArchivoAdjuntos = new HashSet<TCorreoGmailArchivoAdjunto>();
        }

        /// <summary>
        /// Llave primaria de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave UID devuelto por mailbee Gets UID (Unique-ID), id asignado al mensaje en servidor de correos
        /// </summary>
        public long GmailCorreoId { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_GmailFolder
        /// </summary>
        public int IdGmailFolder { get; set; }
        /// <summary>
        /// Almacena el asunto del correo
        /// </summary>
        public string? Asunto { get; set; }
        /// <summary>
        /// Almacena la fecha recibida
        /// </summary>
        public DateTime Fecha { get; set; }
        /// <summary>
        /// Almacena el cuerpo (body) del correo
        /// </summary>
        public string CuerpoHtml { get; set; } = null!;
        /// <summary>
        /// Indica si el correo fue visto
        /// </summary>
        public bool EsLeido { get; set; }
        /// <summary>
        /// Almacena el remitente del correo
        /// </summary>
        public string? NombreRemitente { get; set; }
        /// <summary>
        /// Almacena el remitente del correo
        /// </summary>
        public string? EmailRemitente { get; set; }
        /// <summary>
        /// Almacena los destinatarios del correo
        /// </summary>
        public string Destinatarios { get; set; } = null!;
        /// <summary>
        /// Almacena los correos a quienes les llega un correo de copia oculta
        /// </summary>
        public string? EmailConCopiaOculta { get; set; }
        /// <summary>
        /// Almacena los correos a quienes les llega un correo de copia
        /// </summary>
        public string? EmailConCopia { get; set; }
        /// <summary>
        /// Indica si aplica para crear una oportunidad
        /// </summary>
        public bool AplicaCrearOportunidad { get; set; }
        /// <summary>
        /// Indica si cumple los criterios minimos para crear una oportunidad
        /// </summary>
        public bool CumpleCriterioCrearOportunidad { get; set; }
        /// <summary>
        /// Indica si se ha creado una oportunidad para ese correo
        /// </summary>
        public bool SeCreoOportunidad { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_PrioridadMailChimpListaCorreo, este se calcula leyendo el cuerpo del correo
        /// </summary>
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
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
        /// Id original de la tabla en integraV3
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Indica si el remitente de este correo fue desuscrito correctamente
        /// </summary>
        public bool? EsDesuscritoCorrectamente { get; set; }
        /// <summary>
        /// Indica si el remitente de este correo debe ser desuscrito
        /// </summary>
        public bool? EsMarcadoDesuscrito { get; set; }
        /// <summary>
        /// Indica si el registro ha sido descartado para crear oportunidad
        /// </summary>
        public bool? EsDescartado { get; set; }
        /// <summary>
        /// Foreign key de Personal
        /// </summary>
        public int? IdPersonal { get; set; }

        public virtual ICollection<TCorreoGmailArchivoAdjunto> TCorreoGmailArchivoAdjuntos { get; set; }
    }
}
