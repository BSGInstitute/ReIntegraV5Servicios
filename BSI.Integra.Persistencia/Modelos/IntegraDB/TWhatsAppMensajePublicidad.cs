using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppMensajePublicidad
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave ForeingKey del personal
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Llave ForeingKey del conjunto lista resultado
        /// </summary>
        public int? IdConjuntoListaResultado { get; set; }
        /// <summary>
        /// Llave ForeingKey del Alumno
        /// </summary>
        public int IdAlumno { get; set; }
        /// <summary>
        /// Numero de celular
        /// </summary>
        public string Celular { get; set; } = null!;
        /// <summary>
        /// Llave ForeingKey del WhatsApp Configuracion Envio
        /// </summary>
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        /// <summary>
        /// Llave ForeingKey del Pais
        /// </summary>
        public int IdPais { get; set; }
        /// <summary>
        /// Booleando 1 o 0
        /// </summary>
        public bool EsValido { get; set; }
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
        /// Identificador de la tabla mkt.T_WhatsAppEstadoValidacion
        /// </summary>
        public int? IdWhatsAppEstadoValidacion { get; set; }
        /// <summary>
        /// Llave ForeingKey de la tabla Prioidad Mail Chimp Lista Correo
        /// </summary>
        public int? IdPrioridadMailChimpListaCorreo { get; set; }

        public virtual TWhatsAppConfiguracionEnvio IdWhatsAppConfiguracionEnvioNavigation { get; set; } = null!;
        public virtual TWhatsAppEstadoValidacion? IdWhatsAppEstadoValidacionNavigation { get; set; }
    }
}
