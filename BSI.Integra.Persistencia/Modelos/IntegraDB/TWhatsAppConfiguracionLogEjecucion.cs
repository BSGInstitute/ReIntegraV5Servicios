using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracionLogEjecucion
    {
        public TWhatsAppConfiguracionLogEjecucion()
        {
            TWhatsAppConfiguracionEnvioDetalles = new HashSet<TWhatsAppConfiguracionEnvioDetalle>();
        }

        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Clave Foranea de la Tabla mkt.T_WhatsAppConfiguracionEnvio
        /// </summary>
        public int IdWhatsAppConfiguracionEnvio { get; set; }
        /// <summary>
        /// Fecha de Inicio de la ejecucion de la configuracion
        /// </summary>
        public DateTime FechaInicio { get; set; }
        /// <summary>
        /// Fecha de Fin de la ejecucion de la configuracion
        /// </summary>
        public DateTime? FechaFin { get; set; }
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

        public virtual ICollection<TWhatsAppConfiguracionEnvioDetalle> TWhatsAppConfiguracionEnvioDetalles { get; set; }
    }
}
