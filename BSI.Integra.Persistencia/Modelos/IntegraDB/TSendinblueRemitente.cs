using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TSendinblueRemitente
    {
        /// <summary>
        /// Identificador unico de tabla
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre de remitente
        /// </summary>
        public string Nombre { get; set; } = null!;
        /// <summary>
        /// Email de remitente
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Id de remitente en sendingblue
        /// </summary>
        public int IdRemitenteSendingblue { get; set; }
        /// <summary>
        /// Indica el estado del remitente en sending blue
        /// </summary>
        public bool Activo { get; set; }
        /// <summary>
        /// Respuesta de Sendinblue
        /// </summary>
        public string Respuesta { get; set; } = null!;
        /// <summary>
        /// Saber si se guardo o no en Sendinblue
        /// </summary>
        public bool EstadoGuardado { get; set; }
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
    }
}
