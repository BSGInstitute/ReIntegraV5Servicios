using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TLlamadaWebphoneCruceCentral
    {
        /// <summary>
        /// Llave Primaria
        /// </summary>
        public int Id { get; set; }
        public int IdLlamadaWebphone { get; set; }
        public int IdLlamadaCentral { get; set; }
        public DateTime FechaIncioLlamadaWebphone { get; set; }
        public DateTime FechaFinLlamadaWebphone { get; set; }
        public DateTime FechaIncioLlamadaCentral { get; set; }
        public DateTime FechaFinLlamadaCentral { get; set; }
        public string AnexoWebphone { get; set; } = null!;
        public string AnexoCentral { get; set; } = null!;
        public int DuracionTimbradoWebPhone { get; set; }
        public int DuracionContestoWebPhone { get; set; }
        public int DuracionTimbradoCentral { get; set; }
        public int DuracionContestoCentral { get; set; }
        public int IdAlumno { get; set; }
        public int IdActividadDetalle { get; set; }
        public string TelefonoDestinoWebPhone { get; set; } = null!;
        public string TelefonoDestinoCentral { get; set; } = null!;
        public int IdLlamadaWebPhoneEstado { get; set; }
        public string EstadoLlamadaCentral { get; set; } = null!;
        public string SubEstadoLlamadaCentral { get; set; } = null!;
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
        public int IdMigracion { get; set; }
        /// <summary>
        /// Url de reproduccion del audio
        /// </summary>
        public string? UrlAudio { get; set; }
        public string? Troncal { get; set; }
    }
}
