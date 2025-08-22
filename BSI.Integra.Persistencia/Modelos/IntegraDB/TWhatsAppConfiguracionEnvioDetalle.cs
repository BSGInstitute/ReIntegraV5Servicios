using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracionEnvioDetalle
    {
        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave foranea con la tabla mkt.T_WhatsAppConfiguracionLogEjecucion
        /// </summary>
        public int IdWhatsAppConfiguracionLogEjecucion { get; set; }
        /// <summary>
        /// Indica si el registro se envio de forma correcta
        /// </summary>
        public bool EnviadoCorrectamente { get; set; }
        /// <summary>
        /// Mensaje de error del proceso de envio
        /// </summary>
        public string MensajeError { get; set; } = null!;
        /// <summary>
        /// Clave foranea de la tabla mkt.T_ConjuntoListaResultado
        /// </summary>
        public int IdConjuntoListaResultado { get; set; }
        /// <summary>
        /// Numero Ejecucion de ConjuntoListaResultado
        /// </summary>
        public int ConjuntoListaNroEjecucion { get; set; }
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
        /// Contenido del Mensaje Enviado
        /// </summary>
        public string? Mensaje { get; set; }
        /// <summary>
        /// Identificador del mensaje WhatsApp
        /// </summary>
        public string? WhatsAppId { get; set; }
        /// <summary>
        /// Indica si este mensaje a sido descartado para crearse una oportunidad
        /// </summary>
        public bool? DescartarCrearOportunidad { get; set; }
        /// <summary>
        /// Llave foranea de la tabla mkt.T_PrioridadMailChimpListaCorreo
        /// </summary>
        public int? IdPrioridadMailChimpListaCorreo { get; set; }
        /// <summary>
        /// Fecha de Envio Mensaje de Whatsapp
        /// </summary>
        public DateTime? FechaEnvio { get; set; }

        public virtual TConjuntoListaResultado IdConjuntoListaResultadoNavigation { get; set; } = null!;
        public virtual TPrioridadMailChimpListaCorreo? IdPrioridadMailChimpListaCorreoNavigation { get; set; }
        public virtual TWhatsAppConfiguracionLogEjecucion IdWhatsAppConfiguracionLogEjecucionNavigation { get; set; } = null!;
    }
}
