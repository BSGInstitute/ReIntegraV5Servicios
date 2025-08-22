using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionEnvioMailingDetalle
    {
        /// <summary>
        /// Clave Primaria de la Tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConfiguracionEnvioMailing
        /// </summary>
        public int IdConfiguracionEnvioMailing { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConjuntoListaResultado
        /// </summary>
        public int IdConjuntoListaResultado { get; set; }
        /// <summary>
        /// Almacena el asunto con el que se enviara el correo
        /// </summary>
        public string Asunto { get; set; } = null!;
        /// <summary>
        /// Almacena el cuerpo en formato html con el que se enviara el correo
        /// </summary>
        public string CuerpoHtml { get; set; } = null!;
        /// <summary>
        /// Indica si el correo se envio correctamente
        /// </summary>
        public bool EnviadoCorrectamente { get; set; }
        /// <summary>
        /// En caso exista un error, el mensaje sera almacenado
        /// </summary>
        public string MensajeError { get; set; } = null!;
        /// <summary>
        /// Estado del registro (creado o eliminado)
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Sistema Automatico Fecha de modificacion
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Usuario de modificacion
        /// </summary>
        public string UsuarioModificacion { get; set; } = null!;
        /// <summary>
        /// Sistema Automatico Fecha creacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        /// <summary>
        /// Sistema Automatico Usuario de creacion
        /// </summary>
        public DateTime FechaModificacion { get; set; }
        /// <summary>
        /// Campo de sistema automatico que guarda la version del registro
        /// </summary>
        public byte[] RowVersion { get; set; } = null!;
        /// <summary>
        /// Id de migracion de la tabla
        /// </summary>
        public int? IdMigracion { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_MandrilEnvioCorreo
        /// </summary>
        public int IdMandrilEnvioCorreo { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        public int? IdPlantilla { get; set; }
        /// <summary>
        /// Llave foranea a la tabla mkt.T_ConfiguracionEnvioMailingDetalle
        /// </summary>
        public int? IdOportunidad { get; set; }

        public virtual TConfiguracionEnvioMailing IdConfiguracionEnvioMailingNavigation { get; set; } = null!;
    }
}
