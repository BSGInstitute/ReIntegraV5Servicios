using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppUsuarioCredencial
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Id del usuario de la tabla T_WhatsAppUsuario
        /// </summary>
        public int IdWhatsAppUsuario { get; set; }
        /// <summary>
        /// Id de cada configuracion de la tabla T_WhatsAppConfiguracion
        /// </summary>
        public int IdWhatsAppConfiguracion { get; set; }
        /// <summary>
        /// Token de validacion otorgada por WhatsApp para poder consumir sus servicios la token se renueva y crea un nuevo registro si esta expira
        /// </summary>
        public string? UserAuthToken { get; set; }
        /// <summary>
        /// Fecha de expiracion de la token enviada por WhatsApp la fecha se renueva y crea un nuevo registro si esta expira
        /// </summary>
        public DateTime? ExpiresAfter { get; set; }
        public bool? EsMigracion { get; set; }
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
