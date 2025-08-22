using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TCampaniaGeneralWhatsApp
    {
        /// <summary>
        /// Identificar de la tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de la Campania General
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Fecha Envio Mailing
        /// </summary>
        public TimeSpan? HoraEnvio { get; set; }
        /// <summary>
        /// Fecha de inicio del envio de whatsapp
        /// </summary>
        public DateTime? FechaInicioEnvioWhatsapp { get; set; }
        /// <summary>
        /// Flag de estado del registro
        /// </summary>
        public bool Estado { get; set; }
        /// <summary>
        /// Usuario que creo el registro
        /// </summary>
        public string UsuarioCreacion { get; set; } = null!;
        /// <summary>
        /// Ultimo usuario que modifico el registro
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
        public int? IdMigracion { get; set; }
    }
}
