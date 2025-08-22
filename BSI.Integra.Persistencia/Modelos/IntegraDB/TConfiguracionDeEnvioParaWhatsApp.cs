using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TConfiguracionDeEnvioParaWhatsApp
    {
        public TConfiguracionDeEnvioParaWhatsApp()
        {
            TPersonalEncargadoDeEnvioDeConsulta = new HashSet<TPersonalEncargadoDeEnvioDeConsultum>();
        }

        /// <summary>
        /// identificador unico de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// identificador unico de plantilla referencia a T_Plantilla
        /// </summary>
        public int? IdPlantilla { get; set; }
        /// <summary>
        /// Fecha de envio de mensajes
        /// </summary>
        public DateTime FechaDeEnvio { get; set; }
        /// <summary>
        /// Fecha de finalizacion de envio de mensajes
        /// </summary>
        public DateTime FechaFinDeEnvio { get; set; }
        /// <summary>
        /// Tiempo entre envios
        /// </summary>
        public int TiempoEntreEnvios { get; set; }
        /// <summary>
        /// Referencia a la tabla de filtrado de datos mkt.T_CampaniaGeneralDetalle
        /// </summary>
        public int IdCampaniaGeneralDetalle { get; set; }
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
        /// Hora de envio de plantillas
        /// </summary>
        public int? HoraDeEnvio { get; set; }
        /// <summary>
        /// Indica el nombre d ela campania wpp
        /// </summary>
        public string Nombre { get; set; } = null!;

        public virtual TCampaniaGeneralDetalle IdCampaniaGeneralDetalleNavigation { get; set; } = null!;
        public virtual TPlantilla? IdPlantillaNavigation { get; set; }
        public virtual ICollection<TPersonalEncargadoDeEnvioDeConsultum> TPersonalEncargadoDeEnvioDeConsulta { get; set; }
    }
}
