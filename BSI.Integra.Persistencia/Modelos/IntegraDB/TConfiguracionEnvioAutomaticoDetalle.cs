using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionEnvioAutomaticoDetalle
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// FK T_ConfiguracionEnvioAutomatico
        /// </summary>
        public int? IdConfiguracionEnvioAutomatico { get; set; }
        /// <summary>
        /// FK_TipoEnvioAutomatica
        /// </summary>
        public int? IdTipoEnvioAutomatico { get; set; }
        /// <summary>
        /// FK T_TiempoFrecuencia
        /// </summary>
        public int? IdTiempoFrecuencia { get; set; }
        /// <summary>
        /// FK T_Plantilla
        /// </summary>
        public int? IdPlantilla { get; set; }
        /// <summary>
        /// Valor de anticipación de envio
        /// </summary>
        public int? Valor { get; set; }
        /// <summary>
        /// Estado de envio (Enviado/No Enviado)
        /// </summary>
        public bool? EnvioWhatsApp { get; set; }
        /// <summary>
        /// Estado de envio (Enviado/No Enviado)
        /// </summary>
        public bool? EnvioCorreo { get; set; }
        /// <summary>
        /// Estado de envio (Enviado/No Enviado)
        /// </summary>
        public bool? EnvioMensajeTexto { get; set; }
        /// <summary>
        /// Hora de Envio Automatico
        /// </summary>
        public TimeSpan? HoraEnvioAutomatico { get; set; }
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
    }
}
