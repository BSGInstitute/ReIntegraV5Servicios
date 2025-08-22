using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TPersonalEncargadoDeEnvioDeConsultum
    {
        /// <summary>
        /// identificador unico de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Identificador unico de personal Referencia a [gp].[T_Personal]
        /// </summary>
        public int IdPersonal { get; set; }
        /// <summary>
        /// Cantidad de whatsapp enviados por dia 1
        /// </summary>
        public int Dia1 { get; set; }
        /// <summary>
        /// Cantidad de whatsapp enviados por dia 2
        /// </summary>
        public int Dia2 { get; set; }
        /// <summary>
        /// Cantidad de whatsapp enviados por dia 3
        /// </summary>
        public int Dia3 { get; set; }
        /// <summary>
        /// Cantidad de whatsapp enviados por dia 4
        /// </summary>
        public int Dia4 { get; set; }
        /// <summary>
        /// Cantidad de whatsapp enviados por dia 5
        /// </summary>
        public int Dia5 { get; set; }
        /// <summary>
        /// Fecha de envio dia 1
        /// </summary>
        public DateTime? FechaDia1 { get; set; }
        /// <summary>
        /// Fecha de envio dia 2
        /// </summary>
        public DateTime? FechaDia2 { get; set; }
        /// <summary>
        /// Fecha de envio dia 3
        /// </summary>
        public DateTime? FechaDia3 { get; set; }
        /// <summary>
        /// Fecha de envio dia 4
        /// </summary>
        public DateTime? FechaDia4 { get; set; }
        /// <summary>
        /// Fecha de envio dia 5
        /// </summary>
        public DateTime? FechaDia5 { get; set; }
        /// <summary>
        /// indentificador unico referente a la tabla  whp.T_ConfiguracionDeEnvioParaWhatsApp
        /// </summary>
        public int IdConfiguracionDeEnvioParaWhatsApp { get; set; }
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

        public virtual TConfiguracionDeEnvioParaWhatsApp IdConfiguracionDeEnvioParaWhatsAppNavigation { get; set; } = null!;
        public virtual TPersonal IdPersonalNavigation { get; set; } = null!;
    }
}
