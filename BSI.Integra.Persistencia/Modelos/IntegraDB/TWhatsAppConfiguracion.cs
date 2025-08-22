using System;
using System.Collections.Generic;

namespace BSI.Integra.Persistencia.Modelos.IntegraDB
{
    public partial class TWhatsAppConfiguracion
    {
        /// <summary>
        /// Llave primaria
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Direccion web del servidor que debe estar la Ip y el puerto habilitado de la instancia de WhatsApp
        /// </summary>
        public string? UrlWhatsApp { get; set; }
        /// <summary>
        /// Direccion de la Ip publica del servidor de WhatsApp
        /// </summary>
        public string? IpHost { get; set; }
        /// <summary>
        /// Numero celular registrado y vinculado con el servidor en la cuenta de WhatsApp
        /// </summary>
        public string? Numero { get; set; }
        public string? Vname { get; set; }
        /// <summary>
        /// Credencial otorgada por el numeo y asociado a WhatsApp
        /// </summary>
        public string? Certificado { get; set; }
        /// <summary>
        /// Codigo del pais alq ue pertenece elservidor al que debe de responder
        /// </summary>
        public int IdPais { get; set; }
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
